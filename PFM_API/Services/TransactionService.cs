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

        public async Task ImportTransactions(IFormFile formFile)
        {
            Console.WriteLine("service for transaction import called");
            _transactionRepository.ImportTransactions(formFile);
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
