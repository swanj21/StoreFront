using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using StoreFront.Data;

namespace Ecommerce.Controllers
{
    public class OrderController : Controller
    {
        StoreFrontDataModel db = new StoreFrontDataModel();
        SqlSecurityManager sqlSM = new SqlSecurityManager();

        public ActionResult OrdersAdmin()
        {
            if (sqlSM.IsAdmin()) // IsAdmin() also checks if the Session["UserName"] == null
                return View();

            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public ActionResult OrdersAdmin(string orderID)
        {
            return RedirectToAction("OrderAdminDetails", new { orderID = orderID });
        }

        public ActionResult OrderAdminDetails(int orderID)
        {
            if (sqlSM.IsAdmin())
                return View(orderID);

            return RedirectToAction("Index", "Home");
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
            add.StateID = addressState;
            add.ZipCode = addressZip;
            add.IsBilling = addressBilling;
            add.IsShipping = addressShipping;
            add.CreatedBy = Session["UserName"].ToString();
            add.DateCreated = DateTime.Now;
            add.UserID = db.users.Where(usr => usr.UserName.Equals(add.CreatedBy)).Single().UserID;
            
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

            db.orderProduct.Where(op => op.OrderProductID == prodID).Single().Quantity = quantity;
            db.SaveChanges();

            return PartialView("productsTab", ord);
        }

        public PartialViewResult RemoveFromOrder(int orderProdID, int orderID)
        {
            orders order = db.orders.Find(orderID);
            db.orderProduct.Remove((from op in db.orderProduct
                                    where op.OrderProductID == orderProdID
                                    select op).Single());
            db.SaveChanges();
            /*
            db.Database.ExecuteSqlCommand("DELETE FROM orderProduct " +
                                          "WHERE OrderProductID = {0}", orderProdID);
            */
            return PartialView("productsTab", order);
        }
    }
}
