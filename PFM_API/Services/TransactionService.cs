using AutoMapper;
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

        public async Task<PagedSortedList<Transaction>> GetTransactions(int page, int pageSize, SortOrder sortOrder, string? sortBy)
        {
            var transaction = await _transactionRepository.GetTransactions(page, pageSize, sortOrder, sortBy);
            return _mapper.Map<PagedSortedList<Transaction>>(transaction);
        }

        public async Task ImportTransactions(IFormFile formFile)
        {
            Console.WriteLine("service for transaction import called");
            _transactionRepository.ImportTransactions(formFile);
        }
    }
}
