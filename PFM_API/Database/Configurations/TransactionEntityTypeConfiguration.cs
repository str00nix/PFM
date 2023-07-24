using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PFM_API.Database.Entities;

namespace PFM_API.Database.Configurations
{
    public class TransactionEntityTypeConfiguration : IEntityTypeConfiguration<TransactionEntity>
    {
        public void Configure(EntityTypeBuilder<TransactionEntity> builder)
        {
            builder.ToTable("transaction");
            //builder.ToTable("Transaction");
            //primary key
            builder.HasKey(x => x.Id);

            //column definitions
            builder.Property(x => x.Id).IsRequired();
            builder.Property(x => x.BeneficiaryName);
            builder.Property(x => x.Date).IsRequired();
            builder.Property(x => x.Direction).IsRequired().HasConversion<string>();
            builder.Property(x => x.Amount).IsRequired();
            builder.Property(x => x.Description);
            builder.Property(x => x.CurrencyCode).IsRequired().HasMaxLength(3);
            builder.Property(x => x.Mcc);
            builder.Property(x => x.Kind).IsRequired().HasConversion<string>().HasMaxLength(3);
        }
    }
}
