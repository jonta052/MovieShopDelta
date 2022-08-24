using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MovieShopDelta.Models.Database
{
    public class Movie
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Director { get; set; }

        [Required]
        [Display(Name = "Release Year")]
        public int ReleaseYear { get; set; }

        [Required]
        public string Genre { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public Decimal Price { get; set; }

        public virtual ICollection<OrderRow> OrderRows { get; set; }
    }
}