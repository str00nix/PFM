using PFM_API.Commands;
using PFM_API.Models;

namespace PFM_API.Services
{
    public interface ITransactionService
    {
        Task<PagedSortedList<Transaction>> GetTransactions(List<TransactionKindEnum>? listOfKinds, DateTime? startDate, DateTime? endDate, int page, int pageSize, SortOrder sortOrder, string? sortBy);
        public Task<bool> ImportTransactions(IFormFile formFile);
        Task<bool> InsertTransaction(Transaction transaction);
        Task<bool> CategorizeTransaction(string transactionId, string categoryId);
        Task<bool> SplitTransaction(string id, List<Splits> splits);
        Task<List<SpendingByCategory>> GetAnaliytics(string? catcode, DateTime? startDate, DateTime? endDate, DirectionsEnum? directionKind);
        Task<bool> AutoCategorizeTransactions();
    }
}
