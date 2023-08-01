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

        public async Task<PagedSortedList<CategoryEntity>> GetCategories(int page, int pageSize, Models.SortOrder sortOrder, string? sortBy, string? parentCode)
        {
            var query = _dbContext.Categories.AsQueryable();
            var totalCount = query.Count();
            var totalPages = (int)Math.Ceiling((totalCount * 1.0) / pageSize);

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

            using var reader = new StreamReader(memoryStream);


            using var csv = new CsvReader(reader, System.Globalization.CultureInfo.CurrentCulture);

            csv.Context.RegisterClassMap<CategoriesMap>();


            var records = csv.GetRecords<CategoryEntity>().ToList();

            Console.WriteLine($"{records.Count} records");

            List<CategoryEntity> items = new List<CategoryEntity>();
            foreach (var record in records)
            {

                if (record.ParentCode == "")
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
        public async Task<CategoryEntity> GetCategoryByCodeId(string? codeId)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(t => t.Code.Equals(codeId));
        }
        public async Task<bool> CreateCategory(CategoryEntity categoryEntity)
        {
            _dbContext.Categories.Add(categoryEntity);
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateCategory(Category c)
        {
            var category = await _dbContext.Categories.SingleAsync(x => x.Code.Equals(c.Code));
            category.Name = c.Name;
            category.ParentCode = c.ParentCode;
            await _dbContext.SaveChangesAsync();
            return true;
        }
        public async Task<List<CategoryEntity>> GetChildCategories(string parentCode)
        {
            return await _dbContext.Categories.AsQueryable().Where(x => x.ParentCode.Equals(parentCode)).ToListAsync();
        }

        public async Task<List<CategoryEntity>> GetParentCategories()
        {
            return await _dbContext.Categories.AsQueryable().Where(x => x.ParentCode.Equals("")).ToListAsync();
        }

        public async Task<List<SpendingByCategory>> GetAnalytics(string? catcode, DateTime? startDate, DateTime? endDate, DirectionsEnum? directionKind)
        {
            if (catcode != null)
            {
                var queryC = _dbContext.Categories.AsQueryable();
                var queryT = _dbContext.Transactions.AsQueryable();

                var finalList = await queryT.Join(queryC,
                            queryT => queryT.CatCode,
                            queryC => queryC.Code,
                            (queryT, queryC) => new {
                                Id = queryT.Id,
                                Amount = queryT.Amount,
                                CatCode = queryT.CatCode,
                                ParentCode = queryC.ParentCode,
                                Direction = queryT.Direction,
                                Date = queryT.Date
                            }).Where(x => x.ParentCode.Equals(catcode) || x.CatCode.Equals(catcode))
                            .Where(x => directionKind == null || (x.Direction == directionKind))
                            .Where(x => (startDate == null || (x.Date >= startDate)) && (endDate == null || (x.Date <= endDate)))
                            .GroupBy(x => x.CatCode)
                            .Select(x => new SpendingByCategory
                            {
                                catcode = x.First().CatCode,
                                count = x.Count(),
                                amount = x.Sum(c => c.Amount)
                            }).ToListAsync();

                return finalList;
            }

            else
            {
                List<SpendingByCategory> listOfSpendings = new List<SpendingByCategory>();

                var query = _dbContext.Categories.AsQueryable();
                query = query.Where(x => x.ParentCode.Equals(""));
                List<CategoryEntity> listOfRoots = await query.ToListAsync();

                foreach (CategoryEntity categoryEntity in listOfRoots)
                {
                    string rootCode = categoryEntity.Code;
                    List<CategoryEntity> listOfChildrenAndRoot = await _dbContext.Categories.AsQueryable().Where(x => x.ParentCode.Equals(rootCode) || x.Code.Equals(rootCode)).ToListAsync();

                    List<TransactionEntity> listOfTransactions = await _dbContext.Transactions.AsQueryable()
                        .Where(x => listOfChildrenAndRoot.Contains(x.category))
                        .Where(x => directionKind == null || x.Direction == directionKind)
                        .Where(x => (startDate == null || x.Date >= startDate) && (endDate == null || x.Date <= endDate)).ToListAsync();


                    SpendingByCategory s = new SpendingByCategory(); s.amount = 0.0; s.count = 0; s.catcode = rootCode;
                    foreach(TransactionEntity transactionEntity in listOfTransactions)
                    {
                        s.amount += transactionEntity.Amount;
                        s.count++;
                    }

                    if (s.count > 0) listOfSpendings.Add(s);
                }
                return listOfSpendings;
            }
        }
    }
}
