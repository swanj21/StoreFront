using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class ShoppingCartViewModel : CustomerBaseViewModel
    {
        public List<product> ShoppingCartItems = new List<product>();
        public CustomerBaseViewModel db = new CustomerBaseViewModel();

        public void PopulateCart(int cartID)
        {// Populate the ShoppingCartItems list
            var products = Database.SqlQuery<product>(
                "SELECT product.ProductID, product.ProductDescription, product.IsPublished, " +
                "product.ProductName, product.ImageFile, shoppingCartProduct.Quantity, product.Price, " +
                "product.DateCreated, product.CreatedBy, product.DateModified, product.ModifiedBy " +
                "FROM product inner join shoppingCartProduct on product.ProductID = shoppingCartProduct.ProductID " +
                "WHERE ShoppingCartID = {0}", cartID);
            ShoppingCartItems = products.ToList();
        }
    }
}