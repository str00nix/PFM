using CsvHelper.Configuration.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Xml.Linq;

namespace PFM_API.Models
{
    public class Transaction
    {
        //required
        [Key]
        [Required]
        [Name("id")]
        [ReadOnly(true)]
        public string Id {get; set;}

        [Name("beneficiary-name")]
        public string? BeneficiaryName {get; set;}

        //required
        [Name("date")]
        [Required]
        public DateTime Date {get; set;}

        //required
        [Name("direction")]
        [Required]
        public DirectionsEnum Direction {get; set;}

        //required
        [Name("amount")]
        [Required]
        public double Amount {get; set;}

        [Name("description")]
        public string? Description {get; set;}

        //required
        [Name("currency")]
        [MaxLength(3)]
        [MinLength(3)]
        [Required]
        public string CurrencyCode {get; set;}

        [Name("mcc")]
        ///[ForeignKey("Mcc")]
        public MCCEnum? Mcc {get; set;}

        //required
        [Name("kind")]
        [Required]
        public TransactionKindEnum Kind {get; set;}

        [Name("catcode")]
        [ReadOnly(true)]
        public string? CatCode { get; set; }
        public ICollection<SplitTransaction> SplitTransactions { get; set; }

        public Transaction(){}

        public Transaction(string id, string? beneficiaryName, DateTime date, DirectionsEnum direction, double amount, string? description, string currencyCode, MCCEnum? mcc, TransactionKindEnum kind, string? catCode, ICollection<SplitTransaction>? splitTransactions)
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
            SplitTransactions = splitTransactions ?? new List<SplitTransaction>();
        }
    }
}
