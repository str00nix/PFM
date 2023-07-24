using CsvHelper.Configuration.Attributes;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using PFM_API.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace PFM_API.Database.Entities
{
    public class CategoryEntity
    {
        //required
        [Key]
        [Required]
        [Name("code")]
        [ReadOnly(true)]
        public string Code { get; set; }

        //required
        [Required]
        [Name("name")]
        public string Name { get; set; }

        [Name("parent-code")]
        public string? ParentCode { get; set; }

        //[Name("parent-code")]
        //[ForeignKey("code")]
        //public virtual Category ParentCode { get; set; }
    }
}
