using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GldKiddiesWorld.Models
{
    public class Feedback
    {
        [Key]
        public int FeedbackId { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is Required")]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Invalid Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression(@"([\w\.\-_]+)?\w+@[\w-_]+(\.\w+){1,}", ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required]
        public string Subject { get; set; }

        [Display(Name = "Date Created")]
        [SqlDefaultValue(DefaultValue = "getdate()")]
        public DateTime DateCreated { get; set; }
    }
}