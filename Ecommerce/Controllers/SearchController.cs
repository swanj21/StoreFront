using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class SearchController : Controller
    {
        SearchViewModel SVdb = new SearchViewModel();

        // GET: Search(Search/Index) This gets called when the page is first queried.
        public ActionResult Index()
        {
            ViewBag.Title = "Search";
            return View();
        }

        // POST: Search/Index
        [HttpPost]
        public ActionResult Index(string searchItem)
        {// searchItem works, holds the value of the string to search.
            // List<Ecommerce.Models.product> prodList = SVdb.SearchProducts(searchItem);
            SVdb.SearchProducts(searchItem);

            return View("Search", SVdb);
        }

        // GET
        public ActionResult Search(SearchViewModel svm)
        {
            ViewBag.Title = "Products";
            return View(svm); // This needs an IEnumerable product object passed to it.
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
            users curUser = new users();
            curUser = curUser.FindUser(Session["Username"].ToString());
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