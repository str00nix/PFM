using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PFM_API.Database.Entities;

namespace PFM_API.Database.Configurations
{
    public class SplitTransactionEntityTypeConfiguration : IEntityTypeConfiguration<SplitTransactionEntity>
    {
        public void Configure(EntityTypeBuilder<SplitTransactionEntity> builder)
        {
            builder.ToTable("transactionSplits");
            //primary key
            builder.HasKey(x => x.Id);

            //column definitions
            builder.Property(x => x.Id).IsRequired().ValueGeneratedOnAdd();
            builder.Property(x => x.Catcode);
            builder.Property(x => x.Amount);
        }
    }
}
