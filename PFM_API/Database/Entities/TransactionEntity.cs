using CsvHelper.Configuration.Attributes;
using PFM_API.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace PFM_API.Database.Entities
{
    public class TransactionEntity
    {
        //required
        [Key]
        [Required]
        [Name("id")]
        [ReadOnly(true)]
        public string Id { get; set; }

        [Name("beneficiary-name")]
        public string? BeneficiaryName { get; set; }

        //required
        [Name("date")]
        [Required]
        public DateTime Date { get; set; }

        //required
        [Name("direction")]
        [Required]
        public DirectionsEnum Direction { get; set; }

        //required
        [Name("amount")]
        [Required]
        public double Amount { get; set; }

        [Name("description")]
        public string? Description { get; set; }

        //required
        [Name("currency")]
        [MaxLength(3)]
        [MinLength(3)]
        [Required]
        public string CurrencyCode { get; set; }

        [Name("mcc")]
        public MCCEnum? Mcc { get; set; }

        //required
        [Name("kind")]
        [Required]
        public TransactionKindEnum Kind { get; set; }

        public string? CatCode { get; set; }

        [ForeignKey("catcode")]
        //public virtual CategoryEntity category { get; set; }
        public CategoryEntity? category { get; set; }
        public ICollection<SplitTransactionEntity> SplitTransactions { get; set; }
        public TransactionEntity() { }

        public TransactionEntity(string id, string? beneficiaryName, DateTime date, DirectionsEnum direction, double amount, string? description, string currencyCode, MCCEnum? mcc, TransactionKindEnum kind, string? catCode, ICollection<SplitTransactionEntity>? splitTransactions)
        {
            Id = id;
            BeneficiaryName = beneficiaryName;
            Date = date;
            Direction = direction;
            Amount = amount;
            Description = description;
            CurrencyCode = currencyCode;
            Mcc = mcc;
            Kind = kind;
            CatCode = catCode;
            SplitTransactions = splitTransactions ?? new List<SplitTransactionEntity>();
        }
    }
}
