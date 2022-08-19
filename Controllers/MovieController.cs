using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
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

        public ActionResult AddMovie()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddMovie([Bind(Include = "Id,Title,Director,ReleaseYear,Genre,Price")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("AddMovie");
            }

            return View(movie);
        }
    }
}