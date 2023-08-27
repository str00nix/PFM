using AutoMapper;
using PFM_API.Commands;
using PFM_API.Database.Entities;
using PFM_API.Database.Repositories;
using PFM_API.Models;

namespace PFM_API.Services
{
    public class TransactionService : ITransactionService
    {

        ITransactionRepository _transactionRepository;
        ICategoryRepository _categoryRepository;
        IMapper _mapper;

        public TransactionService(ITransactionRepository transactionRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<PagedSortedList<Transaction>> GetTransactions(List<TransactionKindEnum>? listOfKinds, DateTime? startDate, DateTime? endDate, int page, int pageSize, SortOrder sortOrder, string? sortBy)
        {
            var transaction = await _transactionRepository.GetTransactions(listOfKinds, startDate, endDate, page, pageSize, sortOrder, sortBy);
            return _mapper.Map<PagedSortedList<Transaction>>(transaction);
        }

        static bool RowHasData(List<string> cells)
        {
            return cells.Any(x => x.Length > 0);
        }

        static double parseStringToDouble(string str)
        {
            return double.Parse(str.Trim('"').Replace(",", ""));
        }

        public async Task<bool> ImportTransactions(IFormFile formFile)
        {
            Console.WriteLine("service for transaction import called");
            //var result = await _transactionRepository.ImportTransactions(formFile);

            using var reader = new StreamReader(formFile.OpenReadStream());

            bool firstLine = true;
            while (reader.EndOfStream == false)
            {
                var content = reader.ReadLine();
                try
                {

                    var parts = content.Split(',').ToList();

                    if (RowHasData(parts))
                    {

                        if (!firstLine)
                        {

                            string Id = parts[0];

                            string beneficiaryName = parts[1];

                            string[] dateParts = parts[2].Split('/');
                            DateTime date = new DateTime(int.Parse(dateParts[2]), int.Parse(dateParts[0]), int.Parse(dateParts[1]));

                            DirectionsEnum directions;
                            Enum.TryParse<DirectionsEnum>(parts[3], out directions);

                            double amount = parts.Capacity == 9 ? double.Parse(parts[4]) : parseStringToDouble(string.Join(',', parts.GetRange(4, parts.Capacity - 8)));

                            string description = parts.Capacity == 9 ? parts[5] : parts[parts.Capacity - 4];

                            string currencyCode = parts.Capacity == 9 ? parts[6] : parts[parts.Capacity - 3];

                            TransactionKindEnum kind;
                            Enum.TryParse<TransactionKindEnum>(parts.Capacity == 9 ? parts[8] : parts[parts.Capacity - 1], out kind);

                            var inserted = await InsertTransaction(new Transaction()
                            {
                                Id = Id,
                                BeneficiaryName = beneficiaryName,
                                Date = date,
                                Amount = amount,
                                Direction = directions,
                                Description = description,
                                CurrencyCode = currencyCode,
                                Mcc = parts.Capacity == 9 ? parts[7] : parts[parts.Capacity - 2],
                                Kind = kind
                            });
                            if (inserted == false)
                            {
                                continue;
                            };
                        }
                        firstLine = false;
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(content);
                    Console.WriteLine(ex.Message);
                    //content
                    break;
                }
            }

            return true;
            //return result;
        }

        private async Task<bool> CheckIfTransactionExist(string id)
        {
            var transaction = await _transactionRepository.GetTransactionById(id);
            return transaction == null ? false : true;

        }

        public async Task<bool> InsertTransaction(Transaction transaction)
        {
            var checkIfTransactionExist = await CheckIfTransactionExist(transaction.Id);
            if (!checkIfTransactionExist)
            {
                return await _transactionRepository.CreateTransaction(_mapper.Map<TransactionEntity>(transaction));
            }
            return false;

        }

        public async Task<bool> SplitTransaction(string id, List<Splits> splits)
        {

            var transaction = await _transactionRepository.GetTransactionById(id);
            if (transaction == null) return false;

            double amount = 0.0;
            foreach (Splits split in splits)
            {
                var category = await _transactionRepository.GetCategoryByCodeId(split.catcode);
                if (category == null) return false;
                amount += split.amount;
            }
            if (transaction.Amount != amount) return false;

            return await _transactionRepository.SplitTransaction(transaction, splits);
        }

        public async Task<bool> CategorizeTransaction(string transactionId, string categoryId)
        {
            var transaction = await _transactionRepository.GetTransactionById(transactionId);
            var category = await _categoryRepository.GetCategoryByCodeId(categoryId);

            if (transaction == null || category == null) {
                return false;
            }
            else
            {
                return await _transactionRepository.CategorizeTransaction(transaction, category);
            }

        }
        public async Task<List<SpendingByCategory>> GetAnaliytics(string? catcode, DateTime? startDate, DateTime? endDate, DirectionsEnum? directionKind)
        {
            return await _transactionRepository.GetAnalytics(catcode, startDate, endDate, directionKind);
        }
        public async Task<bool> AutoCategorizeTransactions()
        {
            return await _transactionRepository.AutoCategorizeTransactions();
        }
    }
}
