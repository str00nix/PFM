using CsvHelper;
using Microsoft.EntityFrameworkCore;
using PFM_API.Database.Entities;
using PFM_API.Mappings;
using PFM_API.Models;

namespace PFM_API.Database.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        PFMDbContext _dbContext;

        public CategoryRepository(PFMDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<PagedSortedList<CategoryEntity>> GetCategories(int page, int pageSize, Models.SortOrder sortOrder, string? sortBy)
        {
            var query = _dbContext.Categories.AsQueryable();
            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((totalCount * 1.0) / pageSize);
            //totalPages = totalPages > 0 ? totalPages : 1;

            if (!String.IsNullOrEmpty(sortBy))
            {
                switch (sortBy)
                {
                    case "code":
                        query = sortOrder == Models.SortOrder.Asc ? query.OrderBy(x => x.Code) : query.OrderByDescending(x => x.Code);
                        break;
                    case "name":
                        query = sortOrder == Models.SortOrder.Asc ? query.OrderBy(x => x.Name) : query.OrderByDescending(x => x.Name);
                        break;
                    case "parent-code":
                        query = sortOrder == Models.SortOrder.Asc ? query.OrderBy(x => x.ParentCode) : query.OrderByDescending(x => x.ParentCode);
                        break;
                }
            }
            else
            {
                query = query.OrderBy(x => x.Name);
            }

            query = query.Skip((page - 1) * pageSize).Take(pageSize);

            var items = await query.ToListAsync();

            return new PagedSortedList<CategoryEntity>
            {
                TotalPages = totalPages,
                TotalCount = totalCount,
                Page = page,
                PageSize = pageSize,
                SortBy = sortBy ?? "name",
                SortOrder = sortOrder,
                Items = items
            };
        }

        public async Task ImportCategories(IFormFile formFile)
        {

            Console.WriteLine("category repository import called");

            formFile = formFile ?? throw new ArgumentNullException(nameof(formFile));

            using var memoryStream = new MemoryStream(new byte[formFile.Length]);
            await formFile.CopyToAsync(memoryStream);
            memoryStream.Position = 0;

            //using var reader = new StreamReader((System.IO.Stream)formFile);
            using var reader = new StreamReader(memoryStream);


            //using var csv = new CsvReader(reader, System.Globalization.CultureInfo.CreateSpecificCulture("enUS"));
            using var csv = new CsvReader(reader, System.Globalization.CultureInfo.CurrentCulture);

            //var records = csv.GetRecords<Category>();

            //...

            csv.Context.RegisterClassMap<CategoriesMap>();


            var records = csv.GetRecords<CategoryEntity>().ToList();

            Console.WriteLine($"{records.Count} records");

            List<CategoryEntity> items = new List<CategoryEntity>();
            foreach (var record in records)
            {

                /* if (string.IsNullOrWhiteSpace(record.id))
                 {
                     break;
                 }*/
                if (record.ParentCode == "")
                //if (record.ParentCode == null)
                {
                    CategoryEntity category;
                    category = _dbContext.Categories.Where(s => s.Code == record.Code).FirstOrDefault();
                    if (category == null)
                    {
                        category = new CategoryEntity();
                    }

                    category.Code = record.Code;
                    category.Name = record.Name;


                    items.Add(category);

                    var index = items.FindIndex(x => x.Code == record.Code);
                    items.RemoveAt(index);
                    if (items.Count == index)
                    {
                        items.Add(category);
                    }
                }
                else
                {
                    CategoryEntity subCategory;
                    subCategory = _dbContext.Categories.Where(s => s.Code == record.Code).FirstOrDefault();

                    if (subCategory == null)
                    {
                        subCategory = new CategoryEntity();
                    }

                    subCategory.Code = record.Code;
                    subCategory.Name = record.Name;
                    subCategory.ParentCode = record.ParentCode;

                    if (!_dbContext.Categories.Any(x => x.Code.Equals(record.Code)))
                        _dbContext.Categories.Add(subCategory);
                    else
                        _dbContext.Categories.Update(subCategory);
                }

            }

            //Adding in database
            foreach (CategoryEntity item in items)
            {
                Console.WriteLine(item.Name);
                if (!_dbContext.Categories.Any(c => c.Code.Equals(item.Code)))
                    _dbContext.Categories.Add(item);
                else
                    _dbContext.Categories.Update(item);

            }
            _dbContext.SaveChanges();

        }
    }
}
