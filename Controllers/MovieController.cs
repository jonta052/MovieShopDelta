using MovieShopDelta.Data;
using MovieShopDelta.Models.Database;
using MovieShopDelta.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

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
        public ActionResult AddMovie([Bind(Include = "Id,Title,Director,ReleaseYear,Genre,Price,ImageURL")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("AllMovies");
            }
            return View(movie);
        }

        //If user want's to return from one selected movie to the list of all movies
        public ActionResult Nollare()
        {
            Session["movieTitle"] = null;
            return RedirectToAction("AllTheMovies");
        }

        public ActionResult AllTheMovies() { return View(); }
        [HttpPost]
        public ActionResult AllTheMovies(Movie chosenMovie)
        {
            //From Search box
            if (chosenMovie.Title == null)
            {
                return View();
            }
            else
            {
                return View(chosenMovie);
            }
        }

        public ActionResult AllMovies(Movie chosenMovie)
        {
            //List to send to view
            List<MovieQuantityVM> movieList = new List<MovieQuantityVM>();
            //Movies user has selected to buy
            List<Movie> selectedMovies = new List<Movie>();
            //List basicly gets one or all movies
            List<Movie> moviesToDisplay = new List<Movie>();

            //User has chosen movie title via textbox
            if (chosenMovie.Title != null)
            {
                Session["movieTitle"] = chosenMovie.Title;
            }

            string movieTitle = "";
            //User has chosen movie title via textbox
            if (Session["movieTitle"] != null)
            {
                movieTitle = (string)Session["movieTitle"];
            }
            //User has chosen movie title via textbox
            int movieId = 0;
            if (movieTitle != "")
            {
                movieId = db.Movies.Where(m => m.Title == movieTitle).Select(x => x.Id).FirstOrDefault();

                moviesToDisplay.Add(db.Movies.Find(movieId));
            }
            else
            {
                moviesToDisplay = db.Movies.ToList();
            }
            //User has selected movies to buy
            //Fel?
            if (Session["MovieIds"] != null)
            {
                string listOfMovieIds = (string)Session["MovieIds"];

                List<int> lomi = listOfMovieIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

                foreach (var mid in lomi)
                {
                    //Selected movies gets added to list
                    selectedMovies.Add(db.Movies.Find(mid));
                }
                List<Movie> result = new List<Movie>();
                //Har skrivit in film i textbox
                //THIS ONE MIGHT BE WRONG
                /*if (Session["MovieTitle"] == null)
                {
                    result = moviesToDisplay.Concat(selectedMovies).ToList();
                }
                else
                {
                    result = selectedMovies;
                }*/
                //result = moviesToDisplay.Concat(selectedMovies).ToList();
                //List<Movie> result = selectedMovies.Add(movies);
                // var result = selectedMovies.(movies);

                if (moviesToDisplay.Count() == 1)
                {
                    selectedMovies = selectedMovies.Where(m => m.Id == movieId).Select(x => x).ToList();
                }
                result = moviesToDisplay.Concat(selectedMovies).ToList();
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

                //return View("AllTheMovies", movieList);
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

                //return View("AllTheMovies", movieList);
                return PartialView("_AllMovies", movieList);

            }
            //var movieList = db.Movies.ToList();
            //return PartialView("_AllMovies",movieList);
        }
    }
}