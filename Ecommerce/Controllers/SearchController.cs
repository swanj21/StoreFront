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
        CustomerBaseViewModel eCommerceDB = new CustomerBaseViewModel();

        // GET: Search(Search/Index) This gets called when the page is first queried.
        public ActionResult Index()
        {
            ViewBag.Title = "Search";
            return View();
        }

        // POST: Search/Index
        // Gets called when sending information from the Index page by using a submit button(or others)
        [HttpPost]
        public ActionResult Index(string one)
        {
            return View("Search");
        }

        // GET
        public ActionResult Search(IEnumerable<product> prod)
        {
            ViewBag.Title = "Products";
            return View(prod); // This needs an IEnumerable product object passed to it.
        }
    }
}
