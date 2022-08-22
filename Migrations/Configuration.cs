namespace MovieShopDelta.Migrations
{
    using System.Data.Entity.Migrations;

    internal sealed class Configuration : DbMigrationsConfiguration<MovieShopDelta.Data.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(MovieShopDelta.Data.AppDbContext context)
        {
            context.Customers.AddOrUpdate(c => c.EmailAddress, new Models.Database.Customer
            {
                FirstName = "Bengt",
                LastName = "Bengtsson",
                BillingAddress = "Lektorsgatan 34",
                BillingCity = "Linkoping",
                BillingZip = "123456",
                DeliveryAddress = "Lektorsgatan 34",
                DeliveryCity = "Linkoping",
                DeliveryZip = "123456",
                EmailAddress = "hej@da.com",
                Phone = "123-234234234"

            }, new Models.Database.Customer
            {
                FirstName = "Adam",
                LastName = "Jones",
                BillingAddress = "Norrgatan 15",
                BillingCity = "Sala",
                BillingZip = "56783",
                DeliveryAddress = "Norrgatan 15",
                DeliveryCity = "Sala",
                DeliveryZip = "56783",
                EmailAddress = "adam@jones.com",
                Phone = "567-145637156"
            },new Models.Database.Customer
            { 
                FirstName = "Brett",
                LastName = "Vasquez",
                BillingAddress = "Lantgatan 84",
                BillingCity = "Katrineholm",
                BillingZip = "79652",
                DeliveryAddress = "Jakobsgatan 17",
                DeliveryCity = "Ryd",
                DeliveryZip = "73956",
                EmailAddress = "brett@vasquez.com",
                Phone = "985-123789654" 
            },new Models.Database.Customer
            { 
                FirstName = "Anna",
                LastName = "Li",
                BillingAddress = "Linggatan 5",
                BillingCity = "Storlien",
                BillingZip = "31231",
                DeliveryAddress = "Stora gatan 38",
                DeliveryCity = "Stockholm",
                DeliveryZip = "65324",
                EmailAddress = "anna@li.com",
                Phone = "652-321678952" 
            },new Models.Database.Customer
            { 
                FirstName = "David", LastName = "Eriksson",
                BillingAddress = "Vasagatan 33",
                BillingCity = "Helsingborg",
                BillingZip = "95236",
                DeliveryAddress = "Vasagatan 33",
                DeliveryCity = "Helsingborg",
                DeliveryZip = "95236",
                EmailAddress = "david@eriksson.com",
                Phone = "502-103204506" 
            },new Models.Database.Customer
            { 
                FirstName = "Paula",
                LastName = "Lima",
                BillingAddress = "Strutsgatan 99",
                BillingCity = "Uppsala",
                BillingZip = "72424",
                DeliveryAddress = "Strutsgatan 99",
                DeliveryCity = "Uppsala",
                DeliveryZip = "72424",
                EmailAddress = "paula@lima.com",
                Phone = "021-584343926" 
            });
            context.Movies.AddOrUpdate(m => m.Title, new Models.Database.Movie
            {
                Title = "Sallskapsresan",
                Director = "Lasse Aberg",
                ReleaseYear = 1980,
                Genre = "Comedy",
                Price = 130

            }, new Models.Database.Movie
            {
                Title = "Mortal Engines",
                Director = "Christian Rivers",
                ReleaseYear = 2018,
                Genre = "Action",
                Price = 175
            }, new Models.Database.Movie
            {
                Title = "The Godfather",
                Director = "Francis Ford Coppola",
                ReleaseYear = 1972,
                Genre = "Drama",
                Price = 187
            }, new Models.Database.Movie
            {
                Title = "The Shawshank Redemption",
                Director = "Frank Darabont",
                ReleaseYear = 1994,
                Genre = "Drama",
                Price = 163
            }, new Models.Database.Movie
            {
                Title = "The Godfather: Part II",
                Director = "Francis Ford Coppola",
                ReleaseYear = 1974,
                Genre = "Drama",
                Price = 180
            }, new Models.Database.Movie
            {
                Title = "12 Angry Men",
                Director = "Sidney Lumet",
                ReleaseYear = 1957,
                Genre = "Crime",
                Price = 130
            }, new Models.Database.Movie
            {
                Title = "The Lord of the Rings",
                Director = "Peter Jackson",
                ReleaseYear = 2003,
                Genre = "Adventure",
                Price = 98
            }, new Models.Database.Movie
            {
                Title = "Forest Gump",
                Director = "Sidney Lumet",
                ReleaseYear = 1994,
                Genre = "Romance",
                Price = 164
            }, new Models.Database.Movie
            {
                Title = "Inception",
                Director = "Christopher Nolan",
                ReleaseYear = 2010,
                Genre = "Sci-fi",
                Price = 167
            }, new Models.Database.Movie
            {
                Title = "The Matrix",
                Director = "Wachowski Sisters",
                ReleaseYear = 1999,
                Genre = "Action",
                Price = 144
            }
            );
            context.Orders.AddOrUpdate(c => c.Id, new Models.Database.Order
            {
                //Change OrderDate in Order model to only set
                CustomerId = 1
                
            }, new Models.Database.Order
            {
                CustomerId = 2
            }, new Models.Database.Order
            {
                CustomerId = 3
            }
            );
            context.OrderRows.AddOrUpdate(c => c.Id, new Models.Database.OrderRow
            {
                //Change OrderDate in Order model to only set
                OrderId = 1,
                MovieId = 3,
                Price = 187

            }, new Models.Database.OrderRow
            {
                OrderId = 1,
                MovieId = 5,
                Price = 180

            }, new Models.Database.OrderRow
            {
                OrderId = 2,
                MovieId = 4,
                Price = 163
            }, new Models.Database.OrderRow
            {
                OrderId = 3,
                MovieId = 7,
                Price = 98
            },
             new Models.Database.OrderRow
             {
                 OrderId = 4,
                 MovieId = 7,
                 Price = 98
             }
            );

            context.SaveChanges();
        }
    }
}
