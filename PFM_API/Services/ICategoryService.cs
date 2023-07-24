using PFM_API.Models;

namespace PFM_API.Services
{
    public interface ICategoryService
    {
        Task<PagedSortedList<Category>> GetCategories(int page, int pageSize, Models.SortOrder sortOrder, string? sortBy);
        public Task ImportCategories(IFormFile formFile);
    }
}
