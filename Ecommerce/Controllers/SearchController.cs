using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using StoreFront.Data;

namespace Ecommerce.Controllers
{
    public class SearchController : Controller
    {
         InventoryRepository SVdb = new InventoryRepository();

        // GET: Search(Search/Index)
        public ActionResult Index()
        {
            ViewBag.Title = "Search";
            return View();
        }

        // POST: Search/Index
        [HttpPost]
        public ActionResult Index(string searchItem)
        {
            SVdb.SearchProducts(searchItem);

            return View("Search", SVdb);
        }

        // GET
        public ActionResult Search(InventoryRepository svm)
        {
            ViewBag.Title = "Products";
            return View(svm);
        }

        [HttpPost]
        public ActionResult Search(string output)
        {
            return View();
        }

        /* This will add the specified item to the specified shopping cart, 
         * and increase the session[ItemsInCart] by 1.  */
        public PartialViewResult AddShoppingCartItem(int itemID, int shoppingCartID, string searchStr)
        {
            StoreFront.Data.users curUser = new StoreFront.Data.users();
            curUser = curUser.FindUser(Session["UserName"].ToString());
            // True if the quantity of an item in the cart was updated, false if a new item was added.
            bool quantityUpdated = SVdb.AddShoppingCartItem(itemID, shoppingCartID, curUser);

            int result = Convert.ToInt32(Session["ItemsInCart"]);

            if (!quantityUpdated)
            {// If a new item was added to the cart.
                int itemsInCart = Convert.ToInt32(Session["ItemsInCart"]);
                itemsInCart++;
                Session["ItemsInCart"] = itemsInCart;
                result = itemsInCart;
            }
            SVdb.SearchProducts(searchStr);
            return PartialView("_Cart", result);
        }
    }
}