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
        IMapper _mapper;

        public TransactionService(ITransactionRepository transactionRepository, IMapper mapper)
        {
            _transactionRepository = transactionRepository;
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

        private async Task<bool> CheckIfTransactionExist(string Id)
        {
            var transaction = await _transactionRepository.GetTransactionById(Id);
            if (transaction == null) return false;
            else return true;

        }

        public async Task<bool> InsertTransaction(Transaction t)
        {
            var checkIfTransactionExist = await CheckIfTransactionExist(t.Id);
            if (!checkIfTransactionExist)
            {
                return await _transactionRepository.CreateTransaction(_mapper.Map<TransactionEntity>(t));
            }
            return false;

        }

        public async Task<bool> SplitTransaction(Splits[] splits, string id)
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

            return await _transactionRepository.SplitTheTransaction(transaction, splits);
        }

        public async Task<bool> CategorizeTransaction(string id, string idCategory)
        {
            var transaction = await _transactionRepository.GetTransactionById(id);
            var category = await _transactionRepository.GetCategoryByCodeId(idCategory);
            if (transaction == null || category == null) return false;
            else
            {
                return await _transactionRepository.CategorizeTransaction(transaction, category);
            }

        }
        public async Task<List<SpendingByCategory>> GetAnaliytics(string? catcode, DateTime? startDate, DateTime? endDate, DirectionsEnum? directionKind)
        {
            return await _transactionRepository.GetAnalytics(catcode, startDate, endDate, directionKind);
        }
    }
}
