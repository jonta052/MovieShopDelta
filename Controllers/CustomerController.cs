using MovieShopDelta.Data;
using MovieShopDelta.Models.Database;
using MovieShopDelta.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace MovieShopDelta.Controllers
{
    public class CustomerController : Controller
    {
        AppDbContext db = new AppDbContext();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult CustomerOrder()
        {
            return View();
        }

        [HttpPost]
        public ActionResult CustomerOrder(Customer customer)
        {
            var email = customer.EmailAddress;
            List<Customer> customerList = new List<Customer>();
            customerList = db.Customers.ToList();

            //Get Customer Id
            var customerId = 0;
            customerId = (from c in customerList where c.EmailAddress == email select c.Id).FirstOrDefault();

            //Check if we have a customer id
            if (customerId == 0)
            {
                Session["CustomerId"] = null;
                return View();
            }
            else
            {
                Session["CustomerId"] = customerId;
                return View();
            }
        }
        public ActionResult CustomerOrders()
        {
            int customerId = 0;

            // If session is NOT empty, add customer id variable
            if (Session["CustomerId"] != null)
            {
                customerId = (int)Session["CustomerId"];
            }
            
            List<CustomerOrders> co = new List<CustomerOrders>();

            // Select number of orders for only one customer
            if (customerId != 0)
            {
                var oneCustomer = (from c in db.Customers where c.Id == customerId select c);
                co = oneCustomer.Join(db.Orders,
                                  c => c.Id,
                                  o => o.CustomerId,
                                 (c, o) => new
                                 {
                                     Email = c.EmailAddress,
                                     FirstName = c.FirstName,
                                     LastName = c.LastName,
                                     OrderId = o.Id,
                                     OrderDate = o.OrderDate
                                     

                                 }).GroupBy(x => x.Email)
                                 .Select(r => new CustomerOrders
                                 {
                                     Count = r.Count(),
                                     Email = r.FirstOrDefault().Email,
                                     FirstName = r.FirstOrDefault().FirstName,
                                     LastName = r.FirstOrDefault().LastName,
                                     OrderId = r.FirstOrDefault().OrderId,
                                     OrderDate = r.FirstOrDefault().OrderDate
                                 }).ToList();
                
            }

            // Select number of orders for all customers
            else
            {
                co = db.Customers.Join(db.Orders,
                                  c => c.Id,
                                  o => o.CustomerId,
                                 (c, o) => new
                                 {
                                     Email = c.EmailAddress,
                                     FirstName = c.FirstName,
                                     LastName = c.LastName,
                                     OrderDate = o.OrderDate

                                 })
                                 .GroupBy(x => x.Email)
                                 .Select(r => new CustomerOrders
                                 {
                                     Count = r.Count(),
                                     Email = r.FirstOrDefault().Email,
                                     FirstName = r.FirstOrDefault().FirstName,
                                     LastName = r.FirstOrDefault().LastName,
                                     OrderDate = r.FirstOrDefault().OrderDate
                                 })
                                 .ToList();

            }
            
            return PartialView(co);
        }

        //One customer's individual orders
        public ActionResult CustomerOrdersExpanded()
        {

            List<Order> expandedOrders = new List<Order>();
            var customerId = (int)Session["CustomerId"];

            //Select all orders belonging to a specific customer
            expandedOrders = (from o in db.Orders where o.CustomerId == customerId select o).ToList();
            Session["CustomerId"] = null;

            return PartialView(expandedOrders);  

        }
        
        //Gets one orders orderinfo from orderrows
        public ActionResult CustomerOrderrows(Order item)
        {
            var orderId = item.Id;
            List<CustomerOrderRows> cor = new List<CustomerOrderRows>();
            IQueryable<Order> orderItem = (from o in db.Orders where o.Id == orderId select o);
            
            cor = orderItem
                .Join(db.OrderRows,
                o => o.Id,
                or => or.OrderId,
                (o, or) => new { o, or })
                
                .Join(db.Movies,
                m => m.or.MovieId,
                c => c.Id, (m, c) => new { m, c })
                
                .Select(x => new CustomerOrderRows
                {
                    OrderId = x.m.o.Id,
                    OrderDate = x.m.o.OrderDate,
                    Title = x.c.Title,
                    Price = x.c.Price
                })
                .OrderBy(t => t.Title)
                .ToList();

            return PartialView(cor);
        }

        // Creates form to add customer to database
        public ActionResult AddCustomer()
        {
            return View();
        }

        // Creates form to add customer to database
        [HttpPost]
        public ActionResult AddCustomer([Bind(Include = "Id,FirstName,LastName,BillingAddress,BillingCity,BillingZip,DeliveryAddress,DeliveryCity,DeliveryZip,Phone,EmailAddress")] Customer customer, int? choice)
        {
            //Redirect if user comes from checkout
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();

                // var "choice" = 1 (see order controller) indicates the customer comes form shopping cart
                // then redirect back to shopping cart
                if (choice == 1)
                {
                    return RedirectToAction("ShoppingCart", "Order");
                }
                
                // If customer does no tcome frome shopping cart redirect to add customer page
                return RedirectToAction("AddCustomer");
            }

            return View(customer);
        }
    }
}