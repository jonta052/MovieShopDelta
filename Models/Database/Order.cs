using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace MovieShopDelta.Models.Database
{
    public class Order
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy/MM/dd}")]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Required] // added extra from the CodeFirstDemo.
        public int CustomerId { get; set; }
        
        /*[Required]
        public Decimal OrderPrice { get; set; }*/

        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderRow> OrderRows { get; set; }
    }
}