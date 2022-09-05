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
        public ActionResult Index()
        {
            return View();
        }

        // List of all movies and its info from the database
        public ActionResult MovieCatalogue()
        {
            return View(db.Movies.ToList().OrderBy(m => m.Title));
        }

        // Creates form to add movie to database
        public ActionResult AddMovie()
        {
            return View();
        }

        // Creates form to add movie to database
        [HttpPost]
        public ActionResult AddMovie([Bind(Include = "Id,Title,Director,ReleaseYear,Genre,Price,ImageURL")] Movie movie)
        {
            if (ModelState.IsValid)
            {
                db.Movies.Add(movie);
                db.SaveChanges();
                return RedirectToAction("AllTheMovies");
            }
            return View(movie);
        }

        // Connected to return button in all movies view
        // When pressing reset it shows all the movies instead of one selected
        public ActionResult Nollare()
        {
            Session["movieTitle"] = null;
            return RedirectToAction("AllTheMovies");
        }

        // Creates page to buy movies from
        public ActionResult AllTheMovies() 
        { 
            return View(); 
        }

        // Creates page to buy movies from
        [HttpPost]
        public ActionResult AllTheMovies(Movie chosenMovie)
        {
            ///// Search box /////

            // If no title is entered in the search box,
            // then display all movies
            if (chosenMovie.Title == null)
            {
                return View();
            }

            // If a title IS entered in search box send that one to the view
            else
            {
                return View(chosenMovie);
            }
        }

        // Displays the list of movies on the page where one can buy them
        // and includes add to cart buttons
        public ActionResult AllMovies(Movie chosenMovie)
        {
            //List to send to view
            List<MovieQuantityVM> movieList = new List<MovieQuantityVM>();


            //Movies user has selected to buy
            List<Movie> selectedMovies = new List<Movie>();


            //List gets one or all movies
            List<Movie> moviesToDisplay = new List<Movie>();


            //User has chosen movie title via textbox
            if (chosenMovie.Title != null)
            {
                Session["movieTitle"] = chosenMovie.Title;
            }

            // Declaration of empty variable
            string movieTitle = "";


            //User has chosen movie title via textbox
            if (Session["movieTitle"] != null)
            {
                movieTitle = (string)Session["movieTitle"];
            }

            //User has chosen movie title via textbox
            int movieId = 0;

            // If a title has been entered, get its id
            if (movieTitle != "")
            {
                movieId = db.Movies.Where(m => m.Title == movieTitle)
                    .Select(x => x.Id)
                    .FirstOrDefault();

                // If a valid id then add it to moviesToDisplay
                if (movieId != 0)
                {
                    moviesToDisplay.Add(db.Movies.Find(movieId));
                }
                else
                {
                    moviesToDisplay = db.Movies.ToList();
                }
            }

            // If no title has been entered display all movies
            else
            {
                moviesToDisplay = db.Movies.ToList();
            }


            //User has pressed plus button (add movie to cart)
            if (Session["MovieIds"] != null)
            {
                // Add ids to list
                string listOfMovieIds = (string)Session["MovieIds"];

                List<int> lomi = listOfMovieIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

                foreach (var mid in lomi)
                {
                    selectedMovies.Add(db.Movies.Find(mid));
                }

                // Creates a list combining movies
                List<Movie> result = new List<Movie>();

                //If moviesToDisplay only consists of one movie, selectedMovies have to only contain
                //copies of that movie or else the result variable will display other selected movies too
                if (moviesToDisplay.Count() == 1)
                {
                    selectedMovies = selectedMovies.Where(m => m.Id == movieId)
                        .Select(x => x)
                        .ToList();
                }

                result = moviesToDisplay.Concat(selectedMovies).ToList();

                movieList = result.GroupBy(mid => mid.Id)
                    .Select(m => new MovieQuantityVM
                        {
                            Id = m.FirstOrDefault().Id,
                            Title = m.FirstOrDefault().Title,
                            Director = m.FirstOrDefault().Director,
                            ReleaseYear = m.FirstOrDefault().ReleaseYear,
                            Genre = m.FirstOrDefault().Genre,
                            Price = m.FirstOrDefault().Price,
                            ImageURL = m.FirstOrDefault().ImageURL,
                            Quantity = m.Count() - 1
                        })
                    .OrderBy(m => m.Title)
                    .ToList();

                return PartialView("_AllMovies", movieList);
            }
            else
            {
                movieList = moviesToDisplay.GroupBy(mid => mid.Id)
                    .Select(m => new MovieQuantityVM
                        {
                            Id = m.FirstOrDefault().Id,
                            Title = m.FirstOrDefault().Title,
                            Director = m.FirstOrDefault().Director,
                            ReleaseYear = m.FirstOrDefault().ReleaseYear,
                            Genre = m.FirstOrDefault().Genre,
                            Price = m.FirstOrDefault().Price,
                            ImageURL = m.FirstOrDefault().ImageURL,
                            Quantity = m.Count() - 1
                        })
                    .OrderBy(m => m.Title)
                    .ToList();
                
                return PartialView("_AllMovies", movieList);
                
            }
        }
    }
}