using System;
using System.IO;
using System.Web.Mvc;
using StoreFront.Data;
using System.Linq;

namespace Ecommerce.Controllers
{
    public class ShoppingCartController : Controller
    {
        StoreFrontDataModel db = new StoreFrontDataModel();
        users user = new users();

        // GET: ShoppingCart
        public ActionResult Index()
        {
            GetShoppingCart();
            return View(db);
        }

        public void GetShoppingCart()
        {
            db.getShoppingCartProducts(user.GetShoppingCartID(user.FindUser(Session["UserName"].ToString())));
        }

        [HttpPost]
        public ActionResult RemoveFromCart(int productID)
        {
            int cartID = user.GetShoppingCartID(user.FindUser(Session["UserName"].ToString()));

            db.shoppingCartProduct.Remove((from item in db.shoppingCartProduct
                                             where item.ShoppingCartID == cartID && item.ProductID == productID
                                             select item).Single());
            Session["ItemsInCart"] = Convert.ToInt32(Session["ItemsInCart"]) - 1;
            GetShoppingCart();

            db.SaveChanges();

            var _displayCartPartialView = RazorToString(this.ControllerContext, "_DisplayCart", db);
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
            if (quantity < 1)
                return;
            
            user = user.FindUser(Session["UserName"].ToString());

            shoppingCartProduct prod = db.shoppingCartProduct.Where(pr => pr.ProductID == prodID).Single();
            prod.ModifiedBy = user.UserName;
            prod.Quantity = quantity;
            prod.DateModified = DateTime.Now;
            db.SaveChanges();
        }
    }
}