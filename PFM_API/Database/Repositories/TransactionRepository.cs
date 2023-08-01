using CsvHelper;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using PFM_API.Commands;
using PFM_API.Database.Entities;
using PFM_API.Mappings;
using PFM_API.Models;

namespace PFM_API.Database.Repositories
{
    public class TransactionRepository : ITransactionRepository
    {

        PFMDbContext _dbContext;

        public TransactionRepository(PFMDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PagedSortedList<TransactionEntity>> GetTransactions(List<TransactionKindEnum>? listOfKinds, DateTime? startDate, DateTime? endDate, int page, int pageSize, SortOrder sortOrder, string? sortBy)
        {
            var query = _dbContext.Transactions.AsQueryable();


            if (listOfKinds != null && listOfKinds.Count != 0) query = query.Where(x => listOfKinds.Contains(x.Kind));
            
            if (startDate != null) query = query.Where(x => x.Date >= startDate);
            
            if (endDate != null) query = query.Where(x => x.Date <= endDate);


            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((totalCount * 1.0) / pageSize);

            if (!String.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "beneficiary-name":
                        query = sortOrder == Models.SortOrder.Asc ? query.OrderBy(x => x.BeneficiaryName) : query.OrderByDescending(x => x.BeneficiaryName);
                        break;
                    case "date":
                        query = sortOrder == Models.SortOrder.Asc ? query.OrderBy(x => x.Date) : query.OrderByDescending(x => x.Date);
                        break;
                    case "direction":
                        query = sortOrder == Models.SortOrder.Asc ? query.OrderBy(x => x.Direction) : query.OrderByDescending(x => x.Direction);
                        break;
                    case "amount":
                        query = sortOrder == Models.SortOrder.Asc ? query.OrderBy(x => x.Amount) : query.OrderByDescending(x => x.Amount);
                        break;
                    case "description":
                        query = sortOrder == Models.SortOrder.Asc ? query.OrderBy(x => x.Description) : query.OrderByDescending(x => x.Description);
                        break;
                    case "currency":
                        query = sortOrder == Models.SortOrder.Asc ? query.OrderBy(x => x.CurrencyCode) : query.OrderByDescending(x => x.CurrencyCode);
                        break;
                    case "MCC":
                        query = sortOrder == Models.SortOrder.Asc ? query.OrderBy(x => x.Mcc) : query.OrderByDescending(x => x.Mcc);
                        break;
                    case "kind":
                        query = sortOrder == Models.SortOrder.Asc ? query.OrderBy(x => x.Kind) : query.OrderByDescending(x => x.Kind);
                        break;
                    case "CatCode":
                    query = sortOrder == Models.SortOrder.Asc ? query.OrderBy(x => x.category) : query.OrderByDescending(x => x.category);
                    break;
                }
            }
            else
            {
                query = query.OrderBy(x => x.BeneficiaryName);
            }

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            var items = await query.ToListAsync();

            return new PagedSortedList<TransactionEntity>
            {
                TotalPages = totalPages,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                SortBy = sortBy ?? "name",
                SortOrder = sortOrder,
                Items = items
            };
        }

        public async Task ImportTransactions(IFormFile formFile)
        {
            Console.WriteLine("transaction repository import called");

            formFile = formFile ?? throw new ArgumentNullException(nameof(formFile));

            using var memoryStream = new MemoryStream(new byte[formFile.Length]);
            await formFile.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            using var reader = new StreamReader(memoryStream);

            using var csv = new CsvReader(reader, System.Globalization.CultureInfo.CurrentCulture);


            csv.Context.RegisterClassMap<TransactionsMap>();


            var records = csv.GetRecords<TransactionEntity>().ToList();


            Console.WriteLine($"{records.Count} transaction records in csv file");

            List<TransactionEntity> items = new List<TransactionEntity>();


            foreach (var record in records) {

                TransactionEntity transaction = _dbContext.Transactions.Where(s => s.Id == record.Id).FirstOrDefault();

                if (transaction == null)
                {
                    transaction = new TransactionEntity();
                }

                transaction.Id = record.Id;
                transaction.BeneficiaryName = record.BeneficiaryName;
                transaction.Date = record.Date;
                transaction.Direction = record.Direction;
                transaction.Amount = record.Amount;
                transaction.Description = record.Description;
                transaction.CurrencyCode = record.CurrencyCode;
                transaction.Mcc = record.Mcc;
                transaction.Kind = record.Kind;

                items.Add(transaction);

            }

            foreach (TransactionEntity item in items)
            {
                Console.WriteLine(item.BeneficiaryName);
                if (!_dbContext.Transactions.Any(c => c.Id.Equals(item.Id)))
                    _dbContext.Transactions.Add(item);
                else
                    _dbContext.Transactions.Update(item);

            }
            _dbContext.SaveChanges();
        }

        public async Task<bool> CreateTransaction(TransactionEntity transactionEntity)
        {
            _dbContext.Transactions.Add(transactionEntity);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<TransactionEntity> GetTransactionById(string id)
        {
            return await _dbContext.Transactions.Include(x => x.SplitTransactions).FirstOrDefaultAsync(t => t.Id.Equals(id));
        }

        public async Task<bool> CategorizeTransaction(TransactionEntity transaction, CategoryEntity category)
        {
            transaction.CatCode = category.Code;
            transaction.category = category;
            _dbContext.Update(transaction);
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<bool> SplitTransaction(TransactionEntity transaction, List<Splits> splits)
        {
            var existingSplitsList = await _dbContext.TransactionSplits.AsQueryable().Where(x => x.TransactionId.Equals(transaction.Id)).ToListAsync();

            foreach (var existingTransaction in existingSplitsList)
            {
                _dbContext.TransactionSplits.Remove(existingTransaction);

            }

            await _dbContext.SaveChangesAsync();

            foreach (Splits split in splits)
            {
                SplitTransactionEntity splitTransactionEntity = new SplitTransactionEntity() 
                {   Amount = split.amount,
                    Catcode = split.catcode,
                    TransactionId = transaction.Id
                };

                transaction.SplitTransactions.Add(splitTransactionEntity);
            }
            await _dbContext.SaveChangesAsync();
            return true;
        }

        public async Task<CategoryEntity> GetCategoryByCodeId(string codeId)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(t => t.Code.Equals(codeId));
        }

        public async Task<List<SpendingByCategory>> GetAnalytics(string? catcode, DateTime? startDate, DateTime? endDate, DirectionsEnum? directionKind) {

            var query = _dbContext.Transactions.Where(t => t.CatCode != null).AsQueryable();

            if (!string.IsNullOrEmpty(catcode)) query = query.Where(t => t.CatCode == catcode);

            if (startDate != null) query = query.Where(t => t.Date >= startDate);

            if (endDate != null) query = query.Where(t => t.Date <= endDate);

            if (directionKind != null) query = query.Where(t => t.Direction == directionKind);

            var spendingAnalytics = await query
                .GroupBy(t => new { t.CatCode, t.category.Name })
                .Select(g => new SpendingByCategory
                {
                    catcode = g.Key.CatCode,
                    amount = Math.Round(g.Sum(t => t.Amount), 2),
                    count = g.Count()
                })
                .ToListAsync();

            return spendingAnalytics;
        }
        public async Task<bool> AutoCategorizeTransactions()
        {
            var transactions = _dbContext.Transactions.ToList();

            List<CategorizationRuleFromJson> rulesList = new List<CategorizationRuleFromJson>();

            using (StreamReader streamReader = new("rulesJsonFile.json"))
            {
                string jsonObj = await streamReader.ReadToEndAsync();
                rulesList = JsonConvert.DeserializeObject<List<CategorizationRuleFromJson>>(jsonObj).ToList();
            }

            int counter = 0;
            bool wasCategorized = false;

            foreach (TransactionEntity transaction in transactions)
            {
                if (!string.IsNullOrEmpty(transaction.CatCode))
                {
                    continue;
                }

                wasCategorized = false;

                foreach (CategorizationRuleFromJson rule in rulesList)
                {
                    if (rule.MCC.Any(x => x == (int)transaction.Mcc) || rule.Keywords.Any(x => transaction.Description.ToLower().Contains(x.ToLower()) || transaction.BeneficiaryName.ToLower().Contains(x.ToLower())))
                    {
                        transaction.CatCode = rule.CatCode;
                        counter++;
                        //Console.WriteLine($"Auto-Categorized {transaction.Id}");
                        wasCategorized = true;
                        break;
                    }
                }

                if (!wasCategorized)
                {
                    Console.WriteLine($"{transaction.Id} ({transaction.BeneficiaryName}) - ({transaction.Description}) (MCC: {transaction.Mcc}) was not Auto-Categorized");
                }
            }

            await _dbContext.SaveChangesAsync();

            Console.WriteLine($"Auto-Categorized {counter} transactions.");

            return true;
        }

    }
}
