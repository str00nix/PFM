using CsvHelper;
using Microsoft.EntityFrameworkCore;
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

        public async Task<PagedSortedList<TransactionEntity>> GetTransactions(int page, int pageSize, SortOrder sortOrder, string? sortBy)
        {
            var query = _dbContext.Transactions.AsQueryable();
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
            //var records = csv.GetRecords<Transaction>().ToList();


            Console.WriteLine($"{records.Count} transaction records in csv file");

            List<TransactionEntity> items = new List<TransactionEntity>();
            //List<Transaction> items = new List<Transaction>();


            //foreach (TransactionEntity record in records) {
            foreach (var record in records) {

                //Transaction transaction = _dbContext.Transactions.Where(s => s.Id == record.Id).FirstOrDefault();
                TransactionEntity transaction = _dbContext.Transactions.Where(s => s.Id == record.Id).FirstOrDefault();

                if (transaction == null)
                {
                    //transaction = new Transaction();
                    transaction = new TransactionEntity();
                }

                transaction.Id = record.Id;
                transaction.BeneficiaryName = record.BeneficiaryName;
                transaction.Date = record.Date;
                transaction.Direction = record.Direction;

                //case of "2,376.50", normal case is 27.10
                //if (record.Amount is string)
                //{
                //    transaction.Amount = record.Amount;
                //}
                //else
                //{
                //    transaction.Amount = double.Parse(record.Amount, System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.NumberFormatInfo.InvariantInfo);
                //}

                transaction.Amount = record.Amount;


                transaction.Description = record.Description;
                transaction.CurrencyCode = record.CurrencyCode;
                transaction.Mcc = record.Mcc;
                transaction.Kind = record.Kind;

                items.Add(transaction);

            }


            //Adding in database
            foreach (TransactionEntity item in items)
            //foreach (Transaction item in items)
            {
                Console.WriteLine(item.BeneficiaryName);
                if (!_dbContext.Transactions.Any(c => c.Id.Equals(item.Id)))
                    _dbContext.Transactions.Add(item);
                else
                    _dbContext.Transactions.Update(item);

            }
            _dbContext.SaveChanges();
        }
    }
}
