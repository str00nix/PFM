using PFM_API.Models;

namespace PFM_API.Services
{
    public interface ITransactionService
    {
        Task<PagedSortedList<Transaction>> GetTransactions(int page, int pageSize, Models.SortOrder sortOrder, string? sortBy);
        public Task ImportTransactions(IFormFile formFile);
    }
}
