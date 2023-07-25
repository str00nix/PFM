using Microsoft.EntityFrameworkCore;
using PFM_API.Database.Configurations;
using PFM_API.Database.Entities;
using System.Reflection;

namespace PFM_API.Database
{
    public class PFMDbContext : DbContext
    {
        public DbSet<CategoryEntity> Categories { get; set; }
        public DbSet<TransactionEntity> Transactions { get; set; }
        public DbSet<SplitTransactionEntity> TransactionSplits { get; set; }

        public PFMDbContext() {  }

        public PFMDbContext(DbContextOptions<PFMDbContext> options) : base(options) {  }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
            //modelBuilder.ApplyConfiguration(
            //    new CategoryEntityTypeConfiguration(),
            //    new TransactionEntityTypeConfiguration()
            //    );
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            //modelBuilder.ApplyConfiguration(
            //    new CategoryEntityTypeConfiguration()
            //    );
            //modelBuilder.ApplyConfiguration(
            //    new TransactionEntityTypeConfiguration()
            //    );
            ////modelBuilder.Entity<TransactionEntity>().HasKey(t => t.Id);
            base.OnModelCreating(modelBuilder);
        }

    }
}
