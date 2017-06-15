using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Ecommerce.Controllers
{
    public class ShoppingCartController : Controller
    {
        ShoppingCartViewModel cart = new ShoppingCartViewModel();
        users user = new users();

        // GET: ShoppingCart
        public ActionResult Index()
        {
            GetShoppingCart();
            return View(cart);
        }

        public void GetShoppingCart()
        {
            cart.PopulateCart(user.GetShoppingCartID(user.FindUser(Session["Username"].ToString())));
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int productID)
        {
            int cartID = user.GetShoppingCartID(user.FindUser(Session["Username"].ToString()));
            cart.Database.ExecuteSqlCommand("delete from shoppingCartProduct where ShoppingCartID = {0} AND ProductID = {1}",
                cartID, productID);
            Session["ItemsInCart"] = Convert.ToInt32(Session["ItemsInCart"]) - 1;
            GetShoppingCart();

            var _displayCartPartialView = RazorToString(this.ControllerContext, "_DisplayCart", cart);
            var _cartPartialView = RazorToString(this.ControllerContext, "_Cart", Session["ItemsInCart"]);
            var json = Json(new { _displayCartPartialView, _cartPartialView });

            return json;
        }

        public string RazorToString(ControllerContext controllerContext, string viewName, object model)
        {
            controllerContext.Controller.ViewData.Model = model;
            using (var writer = new StringWriter())
            {
                var ViewResult = ViewEngines.Engines.FindPartialView(controllerContext, viewName);
                var ViewContext = new ViewContext(controllerContext, ViewResult.View, controllerContext.Controller.ViewData, 
                                                  controllerContext.Controller.TempData, writer);
                ViewResult.View.Render(ViewContext, writer);
                ViewResult.ViewEngine.ReleaseView(controllerContext, ViewResult.View);
                return writer.GetStringBuilder().ToString();
            }
        }

        public void UpdateItems(int quantity, int prodID)
        {
            user = user.FindUser(Session["Username"].ToString());
            cart.Database.ExecuteSqlCommand(
                "UPDATE shoppingCartProduct " +
                "SET Quantity = {0}, DateModified = SYSDATETIME(), ModifiedBy = {1} " +
                "WHERE ShoppingCartID = {2} AND ProductID = {3};", 
                quantity, user.UserName, user.GetShoppingCartID(user), prodID);
        }
    }
}