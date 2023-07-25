using PFM_API.Models;

namespace PFM_API.Services
{
    public interface ICategoryService
    {
        Task<PagedSortedList<Category>> GetCategories(int page, int pageSize, Models.SortOrder sortOrder, string? sortBy, string? parentCode);
        //Task<PagedSortedList<Category>> GetCategoriesByParentCode(string? parentCode);
        public Task ImportCategories(IFormFile formFile);
        //Task<List<SpendingByCategory>> GetAnaliytics(string? catcode, DateTime? startDate, DateTime? endDate, DirectionsEnum? directionKind);
    }
}
