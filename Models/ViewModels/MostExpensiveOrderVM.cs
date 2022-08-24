using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieShopDelta.Models.ViewModels
{
    public class MostExpensiveOrderVM
    {
        [Required]
        [Display (Name ="First name")]
        public string FirstName { get; set; }

        [Required]
        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        [Display(Name = "Sum of order")]
        public Decimal SumOrder { get; set; }
    }
}