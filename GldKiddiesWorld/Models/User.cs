using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GldKiddiesWorld.Models
{
    public class User
    {
        [Key]
        public int UserId { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "First Name is Required")]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Invalid Name")]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "Last Name is Required")]
        [RegularExpression(@"^(([A-za-z]+[\s]{1}[A-za-z]+)|([A-Za-z]+))$", ErrorMessage = "Invalid Name")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Email is Required")]
        [RegularExpression(@"([\w\.\-_]+)?\w+@[\w-_]+(\.\w+){1,}", ErrorMessage = "Invalid Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Username is Required")]
        [RegularExpression(@"(?!.*[\.\-\_]{2,})^[a-zA-Z0-9\.\-\_]{3,24}$", ErrorMessage = "Invalid Username")]
        public string Username { get; set; }

        [Required]
        [RegularExpression(@"^(?=.*\d)(?=.*[a-z])(?=.*[A-Z])(?=.*[a-zA-Z]).{8,}$", ErrorMessage = "Password must contain atleast one uppercase, lowercase, number, special character & eight characters!")]
        public string Password { get; set; }

        [Display(Name = "Comfirm Password")]
        [Required]
        [Compare("Password", ErrorMessage = "The Password and Confirmation Password do not match.")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "Phone Number is Required")]
        [RegularExpression(@"([+(\d]{1})(([\d+() -.]){5,16})([+(\d]{1})", ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; }

        [Display(Name = "Date Created")]
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [SqlDefaultValue(DefaultValue = "getdate()")]
        public DateTime DateCreated { get; set; }

        public List<Order> Orders { get; set; }
    }
}