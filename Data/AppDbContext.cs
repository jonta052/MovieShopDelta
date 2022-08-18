using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MovieShopDelta.Models.Database;
using System.Data.Entity;

namespace MovieShopDelta.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext() : base("DefaultConnection")
        {

        }

        public DbSet<Customer> Customers { get; set; }
        public DbSet<Movie> Movies { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderRow> OrderRows { get; set; }
    }
}