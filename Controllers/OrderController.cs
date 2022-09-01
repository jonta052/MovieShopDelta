using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MovieShopDelta.Models.Database;
using MovieShopDelta.Data;
using MovieShopDelta.Models.ViewModels;

namespace MovieShopDelta.Controllers
{
    public class OrderController : Controller
    {
        AppDbContext db = new AppDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddToCart(int? id, string thisAction, string thisController)
        {
            Session["MovieIds"] = Session["MovieIds"] +","+ id.ToString();
            return RedirectToAction(thisAction, thisController);
        }

        public ActionResult RemFromCart(int? id, string thisAction, string thisController)
        {
            string listOfMovieIds = (string)Session["MovieIds"];

            // If trying to remove from an empty shopping cart, stay on the all movies list
            if (listOfMovieIds == null)
            {
                return RedirectToAction(thisAction, thisController);
            }

            // Else, remove the item from the shopping cart
            else
            {
                List<string> temp = listOfMovieIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                temp.Remove(id.ToString());
                listOfMovieIds = String.Join(",", temp.ToArray());

                Session["MovieIds"] = listOfMovieIds;

                return RedirectToAction(thisAction, thisController);
            }

        }

        public ActionResult BoughtMovies()
        {
            List<BoughtMoviesVM> shoppingList = new List<BoughtMoviesVM>();
            List<Movie> selectedMovies = new List<Movie>();

            if (Session["MovieIds"] != null)
            {
                string listOfMovieIds = (string)Session["MovieIds"];

                List<int> lomi = listOfMovieIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

                foreach (var mid in lomi)
                {
                    selectedMovies.Add(db.Movies.Find(mid));
                }

                shoppingList = selectedMovies.GroupBy(mmid => mmid.Id).Select(m => new BoughtMoviesVM
                {
                    Id = m.FirstOrDefault().Id,
                    Price = m.Sum(x => x.Price),
                    Title = m.FirstOrDefault().Title,
                    Quantity = m.Count()
                }).ToList();

                /*foreach (var mid in lomi)
                {
                    shoppingList.Add(db.Movies.Find(mid));
                }*/

                // Send the list of movies to _AllMovies partial to be displayed
                // on the ShoppingCart view (which gets this action)
                // return PartialView("~/Views/Movie/_AllMovies.cshtml",shoppingList);

                return PartialView(shoppingList);

            }
            else
            {
                // Send the list of movies to _AllMovies partial to be displayed
                // on the ShoppingCart view (which gets this action)
                // return PartialView("~/Views/Movie/_AllMovies.cshtml",shoppingList);

                return PartialView(shoppingList);
            }
        }

        //Only has textbox + Customer model that is sent to another view
        public ActionResult ShoppingCart()
        {
            ViewBag.HasMovieId = Session["MovieIds"] != null;
            return View();
        }

        public ActionResult CheckOut(Customer customer)
        {
            var email = customer.EmailAddress;
            List<Customer> customerList = new List<Customer>();
            customerList = db.Customers.ToList();
            
            //Get Customer Id
            var customerId = 0;
            customerId = (from c in customerList where c.EmailAddress == email select c.Id).FirstOrDefault(); // Changed to lambda expression

            //Check if we have a customer id
            if (customerId == 0)
            {
                return RedirectToAction("AddCustomer", "Customer");
            }

            //Get movies customer wants to buy
            if (Session["MovieIds"] != null)
            {
                string listOfMovieIds = (string)Session["MovieIds"];

                List<int> lomi = listOfMovieIds.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(int.Parse).ToList();

                //Save some stuff
                Order order = new Order();
                order.CustomerId = customerId;
                foreach (var mid in lomi)
                {
                    order.OrderRows.Add(new OrderRow()
                    {
                        MovieId = mid,
                        Price = db.Movies.Where(m => m.Id == mid).FirstOrDefault().Price
                    });
                }
                
                // Add and save order to the database, then clear the session
                db.Orders.Add(order);
                db.SaveChanges();
                Session.Clear();

                return View("ConfirmationPage");
            }
            else
            {
                return View("ShoppingCart");
            }

        }

    }
}