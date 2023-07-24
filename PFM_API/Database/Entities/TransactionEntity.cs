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
        //case of "2,376.50", normal case is 27.10
        [Name("amount")]
        [Required]
        public double Amount { get; set; }
        //public double Amount {
        //    get { return Amount; }
        //    set {
        //        if (!(value is string)) {
        //            Amount = value;
        //        }
        //        else {
        //            Amount = double.Parse(value, System.Globalization.NumberStyles.AllowDecimalPoint, System.Globalization.NumberFormatInfo.InvariantInfo);
        //        }
        //    }
        //}

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
        public virtual CategoryEntity category { get; set; }
    }
}
