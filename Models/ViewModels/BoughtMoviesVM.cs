using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MovieShopDelta.Models.ViewModels
{
    public class BoughtMoviesVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        [DataType(DataType.Currency)]
        public Decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}