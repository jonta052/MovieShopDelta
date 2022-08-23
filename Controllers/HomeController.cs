using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieShopDelta.Models.Database;
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
                (m, o) => new {
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
            var fiveNewestMovies = (from movie in db.Movies
                                    orderby movie.ReleaseYear descending
                                    select movie)
                                   .Take(5)
                                   .ToList();
                                   
            //var fiveNewestMovies = db.Movies
            //    .OrderByDescending(y => y.ReleaseYear)
            //    .Take(5)
            //    .ToList();

            return PartialView("_FiveNewestMovies",fiveNewestMovies);
        }

        public ActionResult FiveOldestMovies()
        {
            var fiveOldestMovies = (from movie in db.Movies
                                    orderby movie.ReleaseYear
                                    select movie)
                                   .Take(5)
                                   .ToList();

            //var fiveOldestMovies = db.Movies
            //    .OrderBy(y => y.ReleaseYear)
            //    .Take(5)
            //    .ToList();

            return PartialView("_FiveOldestMovies",fiveOldestMovies);
        }

        public ActionResult FiveCheapestMovies()
        {
            var fiveCheapestMovies = (from movie in db.Movies
                                    orderby movie.Price
                                    select movie)
                       .Take(5)
                       .ToList();
            //var fiveCheapestMovies = db.Movies
            //    .OrderBy(p => p.Price)
            //    .Take(5)
            //    .ToList();

            return PartialView("_FiveCheapestMovies",fiveCheapestMovies);
        }

        public ActionResult MostExpensiveOrder()
        {
            return PartialView();
        }

    }
}