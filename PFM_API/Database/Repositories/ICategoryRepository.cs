using PFM_API.Database.Entities;
using PFM_API.Models;

namespace PFM_API.Database.Repositories
{
    public interface ICategoryRepository
    {
        Task<PagedSortedList<CategoryEntity>> GetCategories(int page, int pageSize, Models.SortOrder sortOrder, string? sortBy, string? parentCode);
        public Task ImportCategories(IFormFile formFile);
        Task<CategoryEntity> GetCategoryByCodeId(string codeId);
        Task<bool> CreateCategory(CategoryEntity categoryEntity);
        Task<bool> UpdateCategory(Category category);
        Task<List<CategoryEntity>> GetChildCategories(string parentCode);
        Task<List<CategoryEntity>> GetParentCategories();
        Task<List<SpendingByCategory>> GetAnalytics(string? catcode, DateTime? startDate, DateTime? endDate, DirectionsEnum? directionKind);
    }
}
