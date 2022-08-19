using System;
using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Web;
using System.Web.Mvc;
using MovieShopDelta.Models.Database;
using MovieShopDelta.Data;

namespace MovieShopDelta.Controllers
{
    public class MovieController : Controller
    {
        AppDbContext db = new AppDbContext();
        // GET: Movie
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AllMovies()
        {
            var movieList = db.Movies.ToList();
            return PartialView("_AllMovies",movieList);
        }
    }
}