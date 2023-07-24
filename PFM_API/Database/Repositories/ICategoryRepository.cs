using PFM_API.Database.Entities;
using PFM_API.Models;

namespace PFM_API.Database.Repositories
{
    public interface ICategoryRepository
    {
        Task<PagedSortedList<CategoryEntity>> GetCategories(int page, int pageSize, Models.SortOrder sortOrder, string? sortBy);
        public Task ImportCategories(IFormFile formFile);
    }
}
