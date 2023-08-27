using PFM_API.Commands;
using PFM_API.Database.Entities;
using PFM_API.Models;

namespace PFM_API.Database.Repositories
{
    public interface ITransactionRepository
    {
        Task<PagedSortedList<TransactionEntity>> GetTransactions(List<TransactionKindEnum>? listOfKinds, DateTime? startDate, DateTime? endDate, int page = 1, int pageSize = 10, SortOrder sortOrder = SortOrder.Asc, string? sortBy = null);
        public Task<bool> ImportTransactions(IFormFile formFile);
        Task<TransactionEntity> GetTransactionById(string id);
        Task<bool> CreateTransaction(TransactionEntity transactionEntity);
        Task<bool> CategorizeTransaction(TransactionEntity transaction, CategoryEntity category);
        Task<bool> SplitTransaction(TransactionEntity transaction, List<Splits> splits);
        Task<CategoryEntity> GetCategoryByCodeId(string codeId);
        Task<List<SpendingByCategory>> GetAnalytics(string? catcode, DateTime? startDate, DateTime? endDate, DirectionsEnum? directionKind);
        Task<bool> AutoCategorizeTransactions();
    }
}
