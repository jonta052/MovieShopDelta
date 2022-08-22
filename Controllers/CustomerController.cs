using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieShopDelta.Models.Database;
using MovieShopDelta.Data;

namespace MovieShopDelta.Controllers
{
    public class CustomerController : Controller
    {
        AppDbContext db = new AppDbContext();
        public ActionResult Index()
        {
            return View();
        }
        
        public ActionResult AddToCart(int? id)
        {
            Session["MovieIds"] = Session["MovieIds"] + id.ToString() + ",";

            return RedirectToAction("AddMovie");
        }

        public ActionResult RemFromCart(int? id)
        {
            string listOfMovieIds = (string)Session["MovieIds"];

            List<string> temp = listOfMovieIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
            temp.Remove(id.ToString());

            listOfMovieIds = String.Join(",", temp.ToArray());
            Session["MovieIds"] = listOfMovieIds;

            return RedirectToAction("AddMovie");
        }
        //This page and view to be repaced
        public ActionResult AddMovie(/*Movie movie*/)
        {
            
            return View(db.Movies.ToList());
        }
        /*public ActionResult ShoppingCart()
        {
            return View();
        }
        [HttpPost]*/
        //Needs to be partial
        public ActionResult ShoppingCart()
        {
            List<Movie> shoppingList = new List<Movie>();
            if (Session["MovieIds"] != null)
            {
                string listOfMovieIds = (string)Session["MovieIds"];
                
                List<int> lomi = listOfMovieIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

                foreach (var mid in lomi)
                {
                    shoppingList.Add(db.Movies.Find(mid));
                }
                return View(shoppingList);
            }
            else
            {
                return View(shoppingList);
            }
            
        }

    }
}