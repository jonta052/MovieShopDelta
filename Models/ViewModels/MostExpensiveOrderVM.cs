using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieShopDelta.Models.ViewModels
{
    public class MostExpensiveOrderVM
    {
        [Display (Name ="First name")]
        public string FirstName { get; set; }

        [Display(Name = "Last name")]
        public string LastName { get; set; }

        [Display(Name = "Sum of order")]
        public decimal SumOrder { get; set; }
    }
}