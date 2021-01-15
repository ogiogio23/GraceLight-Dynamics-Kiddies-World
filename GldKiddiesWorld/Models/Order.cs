using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GldKiddiesWorld.Models
{
    public class Order
    {
        [Key]
        public int OrderId { get; set; }

        public int UserId { get; set; }

        public int ProductId { get; set; }

        [Display(Name = "Invoice No.")]
        [Required]
        public string InvoiceNo { get; set; }

        [Display(Name = "Payment Mode")]
        public string PaymentMode { get; set; } = "Cash";

        [Display(Name = "Delivery Address")]
        [Required]
        public string DeliveryAddress { get; set; }

        [Display(Name = "Payment Reference")]
        public string PaymentReference { get; set; }

        public string Status { get; set; } = "Pending";

        [Display(Name = "Date Created")]
        [SqlDefaultValue(DefaultValue = "getdate()")]
        public DateTime DateCreated { get; set; } = DateTime.Now;

        public List<Product> Products { get; set; }
    }
}