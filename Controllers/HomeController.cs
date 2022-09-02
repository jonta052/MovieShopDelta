using MovieShopDelta.Data;
using MovieShopDelta.Models;
using MovieShopDelta.Models.ViewModels;
using System.Linq;
using System.Web.Mvc;

namespace MovieShopDelta.Controllers
{
    public class HomeController : Controller
    {
        AppDbContext db = new AppDbContext();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Requesting Movies and Gift Cards";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult MostPopularMovies()
        {
            // Joining tables
            var mostPopularMovies = db.Movies

                // Joining Movies with OrderRows
                .Join(db.OrderRows,
                m => m.Id,
                o => o.MovieId,
                (m, o) => new
                {
                    MovieId = o.MovieId,
                    Title = m.Title
                })

                // Group data
                .GroupBy(movie => movie.MovieId)

                // Select what data to use
                .Select(obj => new MostPopularMoviesVM
                {
                    MovieId = obj.Key,
                    Count = obj.Count(),
                    Title = obj.FirstOrDefault().Title,
                })

                // Structuring
                .OrderByDescending(count => count.Count)
                .ThenBy(title => title.Title)
                .Take(5)
                .ToList();

            return PartialView(mostPopularMovies);
        }

        public ActionResult FiveNewestMovies()
        {

            var fiveNewestMovies = db.Movies
                .GroupBy(t => t.Title)
                .Select(t => t.FirstOrDefault())
                .OrderByDescending(y => y.ReleaseYear)
                .ThenBy(title => title.Title)
                .Take(5)
                .ToList();

            return PartialView("_FiveNewestMovies", fiveNewestMovies);
        }

        public ActionResult FiveOldestMovies()
        {

            var fiveOldestMovies = db.Movies
                .GroupBy(t => t.Title)
                .Select(t => t.FirstOrDefault())
                .OrderBy(y => y.ReleaseYear)
                .ThenBy(title => title.Title)
                .Take(5)
                .ToList();

            return PartialView("_FiveOldestMovies", fiveOldestMovies);
        }

        public ActionResult FiveCheapestMovies()
        {

            var fiveCheapestMovies = db.Movies
                .GroupBy(t => t.Title)
                .Select(t => t.FirstOrDefault())
                .OrderBy(p => p.Price)
                .ThenBy(title => title.Title)
                .Take(5)
                .ToList();

            return PartialView("_FiveCheapestMovies", fiveCheapestMovies);
        }

        public ActionResult MostExpensiveOrder()
        {
            var mostExpensiveOrder = db.Orders

                // Joining Orders with OrderRows
                .Join(db.OrderRows,
                ord => ord.Id,
                orderrow => orderrow.OrderId,
                (ord1, orderrow) => new
                {
                    OrderId = ord1.Id,
                    Price = orderrow.Price,
                    CustomerId = ord1.CustomerId
                })

                // Joining Orders with Customers
                .Join(db.Customers,
                ord2 => ord2.CustomerId,
                cust => cust.Id,
                (ord2, cust) => new
                {
                    OrderId = ord2.OrderId,
                    Price = ord2.Price,
                    CustomerId = ord2.CustomerId,
                    FirstName = cust.FirstName,
                    LastName = cust.LastName
                })

                // Group data
                .GroupBy(ordid => ordid.OrderId)

                // Select what data to use
                .Select(obj => new MostExpensiveOrderVM
                {
                    FirstName = obj.FirstOrDefault().FirstName,
                    LastName = obj.FirstOrDefault().LastName,
                    SumOrder = obj.Sum(sum => sum.Price)
                })

                // Structuring
                .OrderByDescending(sum => sum.SumOrder)
                .FirstOrDefault();

            return PartialView(mostExpensiveOrder);
        }
    }
}