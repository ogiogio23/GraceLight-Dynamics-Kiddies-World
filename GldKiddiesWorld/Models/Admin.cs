using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GldKiddiesWorld.Models
{
    public class Admin
    {
        [Key]
        public int AdminId { get; set; }

        [Display(Name = "First Name")]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Invalid Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Invalid Name")]
        public string LastName { get; set; }

        [Required]
        [RegularExpression(@"([\w\.\-_]+)?\w+@[\w-_]+(\.\w+){1,}", ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$", ErrorMessage = "Password must contain atleast one uppercase, lowercase, number, special character & eight characters!")]
        public string Password { get; set; }

        [Display(Name = "Comfirm Password")]
        [Required]
        [Compare("Password", ErrorMessage = "The Password and Confirmation Password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Date Created")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [SqlDefaultValue(DefaultValue = "getdate()")]
        public DateTime DateCreated { get; set; } = DateTime.Now;
    }
}