using CsvHelper.Configuration;
using PFM_API.Database.Entities;

namespace PFM_API.Mappings
{
    public class CategoriesMap : ClassMap<CategoryEntity>
    {
        public CategoriesMap() {

            Map(m => m.Code).Name("code").Index(0);
            Map(m => m.ParentCode).Name("parent-code").Index(1);
            Map(m => m.Name).Name("name").Index(2);

        }
    }
}
