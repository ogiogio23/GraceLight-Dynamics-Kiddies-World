using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GldKiddiesWorld.Models
{
    public class Category
    {
        [Key]
        public int CatId { get; set; }

        [Display(Name = "Category Name")]
        [Required]
        public string CategoryName { get; set; }

        [Display(Name = "Date Created")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [SqlDefaultValue(DefaultValue = "getdate()")]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public List<Product> Products { get; set; }
    }
}