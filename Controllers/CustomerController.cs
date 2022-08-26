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
    public class CustomerController : Controller
    {
        AppDbContext db = new AppDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CustomerOrders()
        {
            List<CustomerOrders> co = new List<CustomerOrders>();

            co = db.Customers.Join(db.Orders,
                                      c => c.Id,
                                      o => o.CustomerId,
                                     (c, o) => new 
                                     {
                                         Email = c.EmailAddress,
                                         FirstName = c.FirstName,
                                         LastName = c.LastName,
                                         OrderDate = o.OrderDate

                                     }).GroupBy(x => x.Email)
                                     .Select(r => new CustomerOrders
                                     { 
                                         Count = r.Count(),
                                         Email = r.FirstOrDefault().Email,
                                         FirstName = r.FirstOrDefault().FirstName,
                                         LastName = r.FirstOrDefault().LastName,
                                         OrderDate = r.FirstOrDefault().OrderDate
                                     }).ToList();/*.OrderByDescending(c => c.Email).ToList();*/
           
            return View(co);
        }

        public ActionResult CustomerOrderrows()
        {

            return View();
        }
        public ActionResult AddCustomer()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddCustomer([Bind(Include = "Id,FirstName,LastName,BillingAddress,BillingCity,BillingZip,DeliveryAddress,DeliveryCity,DeliveryZip,Phone,EmailAddress")] Customer customer)
        {
            //Redirect if user from checkout
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("ShoppingCart","Order");
                //return RedirectToAction("AddCustomer");
            }
            return View(customer);
        }
    }
}