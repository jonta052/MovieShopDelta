using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieShopDelta.Models.Database;
using MovieShopDelta.Models.ViewModels;
using MovieShopDelta.Models;
using MovieShopDelta.Data;

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
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult MostPopularMovies()
        {
            var mostPopularMovies = db.Movies
                .Join(db.OrderRows,
                m => m.Id,
                o => o.MovieId,
                (m, o) => new 
                {
                    MovieId = o.MovieId,
                    Title = m.Title
                })
                
                .GroupBy(movie => movie.MovieId)
                
                .Select(obj => new MostPopularMoviesVM
                {
                    MovieId = obj.Key,
                    Count = obj.Count(),
                    Title = obj.FirstOrDefault().Title,
                })
                
                .OrderByDescending(count => count.Count)
                
                .Take(5)
                
                .ToList();

            return PartialView(mostPopularMovies);
        }

        public ActionResult FiveNewestMovies()
        {
            //var fiveNewestMovies = (from movie in db.Movies
            //                        orderby movie.ReleaseYear descending
            //                        select movie)
            //                       .Take(5)
            //                       .ToList();

            var fiveNewestMovies = db.Movies
                .OrderByDescending(y => y.ReleaseYear)
                .Take(5)
                .ToList();

            return PartialView("_FiveNewestMovies",fiveNewestMovies);
        }

        public ActionResult FiveOldestMovies()
        {
            //var fiveOldestMovies = (from movie in db.Movies
            //                        orderby movie.ReleaseYear
            //                        select movie)
            //                       .Take(5)
            //                       .ToList();

            var fiveOldestMovies = db.Movies
                .OrderBy(y => y.ReleaseYear)
                .Take(5)
                .ToList();

            return PartialView("_FiveOldestMovies",fiveOldestMovies);
        }

        public ActionResult FiveCheapestMovies()
        {
            //var fiveCheapestMovies = (from movie in db.Movies
            //                        orderby movie.Price
            //                        select movie)
            //           .Take(5)
            //           .ToList();

            var fiveCheapestMovies = db.Movies
                .OrderBy(p => p.Price)
                .Take(5)
                .ToList();

            return PartialView("_FiveCheapestMovies",fiveCheapestMovies);
        }

        public ActionResult MostExpensiveOrder()
        {
            var mostExpensiveOrder = db.Orders
                .Join(db.OrderRows,
                ord => ord.Id, 
                orderrow => orderrow.OrderId,
                (ord1, orderrow) => new 
                {
                    OrderId = ord1.Id,
                    Price = orderrow.Price,
                    CustomerId = ord1.CustomerId
                })
                
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
                
                .GroupBy(custid => custid.CustomerId)
                
                .Select(obj => new MostExpensiveOrderVM
                {
                    FirstName = obj.FirstOrDefault().FirstName,
                    LastName = obj.FirstOrDefault().LastName,
                    SumOrder = obj.Sum(sum => sum.Price)
                })
                
                .OrderByDescending(sum => sum.SumOrder)
                
                .FirstOrDefault();

            return PartialView(mostExpensiveOrder);
        }

    }
}