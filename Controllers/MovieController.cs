using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieShopDelta.Models.Database;
using MovieShopDelta.Data;
using MovieShopDelta.Models.ViewModels;

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
                return RedirectToAction("AllMovies");
            }
            return View(movie);
        }

        public ActionResult AllTheMovies()
        {

            return View();
        }
        
            public ActionResult AllMovies(Movie chosenMovie)
        {
            List<MovieQuantityVM> movieList = new List<MovieQuantityVM>();
            List<Movie> selectedMovies = new List<Movie>();
            List<Movie> moviesToDisplay = new List<Movie>();

            string movieTitle = "";
            movieTitle = chosenMovie.Title;

            if (movieTitle != null)
            {
                int movieId = db.Movies.Where(m => m.Title == movieTitle).Select(x => x.Id).FirstOrDefault();
                
                moviesToDisplay.Add(db.Movies.Find(movieId));//(from m in db.Movies where m.Title == movieTitle select m);
            }
            else
            {
                moviesToDisplay = db.Movies.ToList();
            }

            if (Session["MovieIds"] != null)
            {
                string listOfMovieIds = (string)Session["MovieIds"];

                List<int> lomi = listOfMovieIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

                foreach (var mid in lomi)
                {
                    selectedMovies.Add(db.Movies.Find(mid));
                }

                //var movies = db.Movies.ToList();

                var result = moviesToDisplay.Concat(selectedMovies);
                //List<Movie> result = selectedMovies.Add(movies);
                // var result = selectedMovies.(movies);

                movieList = result.GroupBy(mid => mid.Id).Select(m => new MovieQuantityVM
                {
                    Id = m.FirstOrDefault().Id,
                    Title = m.FirstOrDefault().Title,
                    Director = m.FirstOrDefault().Director,
                    ReleaseYear = m.FirstOrDefault().ReleaseYear,
                    Genre = m.FirstOrDefault().Genre,
                    Price = m.FirstOrDefault().Price,
                    ImageURL = m.FirstOrDefault().ImageURL,
                    Quantity = m.Count() - 1
                }).ToList();

                /*foreach (var mid in lomi)
                {
                    shoppingList.Add(db.Movies.Find(mid));
                }*/

                // Send the list of movies to _AllMovies partial to be displayed
                // on the ShoppingCart view (which gets this action)
                // return PartialView("~/Views/Movie/_AllMovies.cshtml",shoppingList);

                return PartialView("_AllMovies", movieList);
            }
            else
            {
                movieList = moviesToDisplay.GroupBy(mid => mid.Id).Select(m => new MovieQuantityVM
                {
                    Id = m.FirstOrDefault().Id,
                    Title = m.FirstOrDefault().Title,
                    Director = m.FirstOrDefault().Director,
                    ReleaseYear = m.FirstOrDefault().ReleaseYear,
                    Genre = m.FirstOrDefault().Genre,
                    Price = m.FirstOrDefault().Price,
                    ImageURL = m.FirstOrDefault().ImageURL,
                    Quantity = m.Count() - 1
                }).ToList();

                return PartialView("_AllMovies", movieList);
            }
            //var movieList = db.Movies.ToList();
            //return PartialView("_AllMovies",movieList);
        }
    }
}