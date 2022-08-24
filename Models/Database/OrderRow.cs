using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MovieShopDelta.Models.Database
{
    public class OrderRow
    {
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        public int MovieId { get; set; }

        [Required]
        [DataType(DataType.Currency)]
        public Decimal Price { get; set; }

        public virtual Order Order { get; set; }

        public virtual Movie Movie { get; set; }
    }
}