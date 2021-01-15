using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GldKiddiesWorld.Models
{
    public class Product
    {
        [Key]
        public int ProductId { get; set; }

        [Display(Name = "Product Name")]
        [Required]
        public string ProductName { get; set; }

        [Display(Name = "Product Image1")]
        [Required]
        public string ProductImageUrl1 { get; set; }

        [Display(Name = "Product Image2")]
        [Required]
        public string ProductImageUrl2 { get; set; }

        [Display(Name = "Product Image3")]
        [Required]
        public string ProductImageUrl3 { get; set; }

        [Range(1, 1000000)]
        [DataType(DataType.Currency)]
        [Required]
        public decimal Price { get; set; }

        public string Description { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Display(Name = "Date Created")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [SqlDefaultValue(DefaultValue = "getdate()")]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public int CatId { get; set; }

        public Category Category { get; set; }
    }
}