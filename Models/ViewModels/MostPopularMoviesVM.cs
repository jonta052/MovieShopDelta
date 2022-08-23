using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MovieShopDelta.Models
{
    public class MostPopularMoviesVM
    {
        public int MovieId { get; set; }
        public string Title { get; set; }

        [Display(Name = "Number of purchases")]
        public int Count { get; set; }
    }
}