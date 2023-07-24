using PFM_API.Database.Entities;
using PFM_API.Models;

namespace PFM_API.Database.Repositories
{
    public interface ITransactionRepository
    {
        Task<PagedSortedList<TransactionEntity>> GetTransactions(int page, int pageSize, Models.SortOrder sortOrder, string? sortBy);
        public Task ImportTransactions(IFormFile formFile);
    }
}
