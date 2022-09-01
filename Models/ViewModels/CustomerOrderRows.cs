using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MovieShopDelta.Models.ViewModels
{
    public class CustomerOrderRows
    {
        [Display(Name = "Order Id")]
        public int OrderId { get; set; }

        [Display(Name = "Order Date")]
        public DateTime OrderDate { get; set; }

        [Display(Name = "Movie Title")]
        public string Title { get; set; }

        [Display(Name = "Price")]
        [DataType(DataType.Currency)]
        public Decimal Price { get; set; }

    }
}