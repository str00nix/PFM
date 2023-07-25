using CsvHelper;
using Microsoft.EntityFrameworkCore;
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
                    //"beneficiary-name"
                    //"date"
                    //"direction"
                    //"amount"
                    //"description"
                    //"currency"
                    //"mcc"
                    //"kind"
                    //[ForeignKey("CatCode")]
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

        public async Task<TransactionEntity> GetTransactionById(string Id)
        {

            return await _dbContext.Transactions.Include(x => x.SplitTransactions).FirstOrDefaultAsync(t => t.Id.Equals(Id));
        }
        public async Task<bool> CategorizeTransaction(TransactionEntity transaction, CategoryEntity category)
        {
            transaction.category = category;
            transaction.CatCode = category.Code;
            _dbContext.Update(transaction);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> SplitTheTransaction(TransactionEntity transaction, Splits[] splits)
        {
            var listOfAlreadyExistingSplits = await _dbContext.TransactionSplits.AsQueryable().Where(x => x.TransactionId.Equals(transaction.Id)).ToListAsync();

            for (int i = 0; i < listOfAlreadyExistingSplits.Count; i++)
            {
                _dbContext.TransactionSplits.Remove(listOfAlreadyExistingSplits[i]);

            }
            await _dbContext.SaveChangesAsync();

            foreach (Splits split in splits)
            {
                SplitTransactionEntity splitTransactionEntity = new SplitTransactionEntity() { Amount = split.amount, Catcode = split.catcode, TransactionId = transaction.Id };
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

            if (catcode != null) {
                
                var queryC = _dbContext.Categories.AsQueryable();
                var queryT = _dbContext.Transactions.AsQueryable();

                var finalList = await queryT.Join(queryC,
                            queryT => queryT.CatCode,
                            queryC => queryC.Code,
                            (queryT, queryC) => new {
                                Id = queryT.Id,                             
                                Amount = queryT.Amount,
                                CatCode = queryT.CatCode,
                                ParentCode = queryC.ParentCode,
                                Direction = queryT.Direction,
                                Date = queryT.Date
                            }).Where(x => x.ParentCode.Equals(catcode) || x.CatCode.Equals(catcode))
                            .Where(x => directionKind == null || (x.Direction == directionKind))
                            .Where(x => (startDate == null || (x.Date >= startDate)) && (endDate == null || (x.Date <= endDate)))
                            .GroupBy(x => x.CatCode)
                            .Select(x => new SpendingByCategory {
                                catcode = x.First().CatCode,
                                count = x.Count(),
                                amount = x.Sum(c => c.Amount)
                            }).ToListAsync();

                return finalList;
            }

            else
            {
                List<SpendingByCategory> listOfSpendings = new List<SpendingByCategory>();

                var query = _dbContext.Categories.AsQueryable();
                query = query.Where(x => x.ParentCode.Equals(""));
                List<CategoryEntity> listOfRoots = await query.ToListAsync();

                foreach (CategoryEntity categoryEntity in listOfRoots) {
                    string rootCode = categoryEntity.Code;
                    List<CategoryEntity> listOfChildrenAndRoot = await _dbContext.Categories.AsQueryable().Where(x => x.ParentCode.Equals(rootCode) || x.Code.Equals(rootCode)).ToListAsync();

                    List<TransactionEntity> listOfTransactions = await _dbContext.Transactions.AsQueryable()
                        .Where(x => listOfChildrenAndRoot.Contains(x.category))
                        .Where(x => directionKind == null || x.Direction == directionKind)
                        .Where(x => (startDate == null || x.Date >= startDate) && (endDate == null || x.Date <= endDate)).ToListAsync();


                    SpendingByCategory s = new SpendingByCategory(); s.amount = 0.0; s.count = 0; s.catcode = rootCode;
                    foreach(TransactionEntity transactionEntity in listOfTransactions) {
                        s.amount += transactionEntity.Amount;
                        s.count++;
                    }

                    if(s.count > 0) listOfSpendings.Add(s);
                }
                return listOfSpendings;
            }
        }
    }
}
