using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MovieShopDelta.Models.Database
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Billing Address")]
        public string BillingAddress { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Billing City")]
        public string BillingCity { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Billing Zip")]
        public string BillingZip { get; set; }

        [Required]
        [StringLength(200)]
        [Display(Name = "Delivery Address")]
        public string DeliveryAddress { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Delivery City")]
        public string DeliveryCity { get; set; }

        [Required]
        [StringLength(100)]
        [Display(Name = "Delivery Zip")]
        public string DeliveryZip { get; set; }

        [Required]
        [StringLength(25)]
        [DataType(DataType.PhoneNumber)]
        public string Phone { get; set; }

        [Required]
        [StringLength(255)]
        [Display(Name = "Email")]
        [DataType(DataType.EmailAddress)]
        public string EmailAddress { get; set; }

        public virtual ICollection<Order> Orders { get; set; }
    }
}