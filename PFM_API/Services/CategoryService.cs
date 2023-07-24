using AutoMapper;
using CsvHelper;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using PFM_API.Database.Repositories;
using PFM_API.Mappings;
using PFM_API.Models;

namespace PFM_API.Services
{
    public class CategoryService : ICategoryService
    {

        ICategoryRepository _categoryRepository;
        IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<PagedSortedList<Category>> GetCategories(int page, int pageSize, Models.SortOrder sortOrder, string? sortBy)
        {
            var categories = await _categoryRepository.GetCategories(page, pageSize, sortOrder, sortBy);
            return _mapper.Map<PagedSortedList<Category>>(categories);
        }

        public async Task ImportCategories(IFormFile formFile)
        {
            Console.WriteLine("category service import called");
            _categoryRepository.ImportCategories(formFile);
        }
    }
}
