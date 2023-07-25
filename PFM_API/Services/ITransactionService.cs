using PFM_API.Commands;
using PFM_API.Models;

namespace PFM_API.Services
{
    public interface ITransactionService
    {
        Task<PagedSortedList<Transaction>> GetTransactions(List<TransactionKindEnum>? listOfKinds, DateTime? startDate, DateTime? endDate, int page, int pageSize, SortOrder sortOrder, string? sortBy);
        public Task ImportTransactions(IFormFile formFile);
        Task<bool> InsertTransaction(Transaction t);
        Task<bool> CategorizeTransaction(string id, string idCategory);
        Task<bool> SplitTransaction(Splits[] splits, string id);
        Task<List<SpendingByCategory>> GetAnaliytics(string? catcode, DateTime? startDate, DateTime? endDate, DirectionsEnum? directionKind);
    }
}
