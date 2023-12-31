﻿using CsvHelper.Configuration.Attributes;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using System.Xml.Linq;

namespace PFM_API.Models
{
    public class Category
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
        [JsonPropertyName("parent-code")]
        public string? ParentCode { get; set; }
    }
}
