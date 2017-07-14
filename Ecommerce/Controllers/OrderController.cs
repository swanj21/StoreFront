using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreFront.Data;

namespace Ecommerce.Controllers
{
    public class OrderController : Controller
    {
        StoreFrontDataModel db = new StoreFrontDataModel();

        public ActionResult OrdersAdmin()
        {
            return View();
        }

        [HttpPost]
        public ActionResult OrdersAdmin(string orderID)
        {
            return RedirectToAction("OrderAdminDetails", new { orderID = orderID });
        }
        public ActionResult OrderAdminDetails(int orderID)
        {
            return View(orderID);
        }

        public ActionResult PlaceOrder()
        {
            return View();
        }

        [HttpPost]
        public ActionResult PlaceOrder(string address1, string address2, string address3, 
                                       string addressCity, int addressState, string addressZip, 
                                       bool addressBilling, bool addressShipping)
        {
            address add = new address();
            add.Address1 = address1;
            add.Address2 = address2;
            add.Address3 = address3;
            add.City = addressCity;
            add.StateID = addressState; // Need to make a dropdown or something to choose the stateID from previous page.
            add.ZipCode = addressZip;
            add.IsBilling = addressBilling;
            add.IsShipping = addressShipping;
            add.CreatedBy = Session["UserName"].ToString();
            add.DateCreated = DateTime.Now;
            string tempName = Session["UserName"].ToString();
            add.UserID = db.users.Where(usr => usr.UserName.Equals(tempName)).SingleOrDefault().UserID;

            db.address.Add(add);
            db.SaveChanges();

            return View();
        }

        public ActionResult ConfirmOrder(int addressID)
        {
            // Get the address from the addressID in the data model db.
            address add = db.address.Find(addressID); // Full address.

            string tempName = Session["UserName"].ToString();
            users user = db.users.Where(uName => uName.UserName.Equals(tempName)).SingleOrDefault();
            List<shoppingCartProduct> products = db.getShoppingCartProducts(db.getShoppingCartID(user.UserID));

            ViewBag.address = add;
            ViewBag.products = products;
            return View();
        }

        public ActionResult OrderSubmitted(int cartID, int userID, int addressID, float total)
        {
            db.removeFromCart(cartID, userID, addressID, total);
            Session["ItemsInCart"] = 0;
            return View();
        }

        public ActionResult IncreaseStatus(int orderID)
        {
            int curStatus = db.orders.Find(orderID).StatusID.Value;
            if (curStatus >= 4)
            {
                return RedirectToAction("OrderAdminDetails", new { orderID = orderID });
            }

            db.orders.Where(ord => ord.OrderID == orderID).SingleOrDefault().StatusID += 1;
            db.SaveChanges();
            return RedirectToAction("OrderAdminDetails", new { orderID = orderID });
        }

        public PartialViewResult UpdateQuantity(int prodID, int quantity, int ordID)
        {
            orders ord = db.orders.Find(ordID);
            if (quantity < 1)
                return PartialView("productsTab", ord);

            // MAKE A PARAMETERIZED QUERY FOR THIS STATEMENT
            db.Database.ExecuteSqlCommand("UPDATE orderProduct " +
                                          "SET Quantity = {0} " +
                                          "WHERE OrderProductID = {1}", quantity, prodID);
            return PartialView("productsTab", ord);
        }

        public PartialViewResult RemoveFromOrder(int orderProdID, int orderID)
        {
            orders order = db.orders.Find(orderID);
            // MAKE A PARAMETERIZED QUERY FOR THIS STATEMENT
            db.Database.ExecuteSqlCommand("DELETE FROM orderProduct " +
                                          "WHERE OrderProductID = {0}", orderProdID);
            return PartialView("productsTab", order);
        }
    }
}
