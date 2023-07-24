using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PFM_API.Database.Entities;

namespace PFM_API.Database.Configurations
{
    public class CategoryEntityTypeConfiguration : IEntityTypeConfiguration<CategoryEntity>
    {
        public void Configure(EntityTypeBuilder<CategoryEntity> builder)
        {
            builder.ToTable("category");
            //builder.ToTable("Category");

            //primary key
            builder.HasKey(x => x.Code);

            //column definitions
            builder.Property(x => x.Code).IsRequired().HasMaxLength(16);
            builder.Property(x => x.Name).IsRequired().HasMaxLength(128);
            builder.Property(x => x.ParentCode).HasMaxLength(16);
        }
    }
}
