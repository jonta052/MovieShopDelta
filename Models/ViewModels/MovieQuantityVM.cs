using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MovieShopDelta.Models.ViewModels
{
    public class MovieQuantityVM
    {
        public int Id { get; set; }

     
        public string Title { get; set; }

     
        public string Director { get; set; }

  
        [Display(Name = "Release Year")]
        public int ReleaseYear { get; set; }


        public string Genre { get; set; }

    
        [DataType(DataType.Currency)]
        public Decimal Price { get; set; }

      
        [Display]
        public string ImageURL { get; set; }

        public int Quantity { get; set; }
    }
}