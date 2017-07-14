using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class SearchViewModel : CustomerBaseViewModel
    {
        public List<product> currentProdList { get; set; }

        /* @param string searchString
           @return IEnumerable<product> with the name like searchString.
        */
        public void SearchProducts(string searchString)
        { // -- REMOVE AND MAKE PARAMETERIZED QUERY -- 
            var dbProductList = Database.SqlQuery<product>(
                "SELECT * " +
                "FROM product " +
                "WHERE product.ProductName LIKE '%" + searchString + "%';");

            currentProdList = dbProductList.ToList();
        }

        public bool AddShoppingCartItem(int productID, int shoppingCartID, StoreFront.Data.users user)
        { // -- REMOVE AND MAKE PARAMETERIZED QUERY --  
            Object queryResult = null;
            queryResult = Database.SqlQuery<Object>(
                "select * " +
                "from shoppingCartProduct " +
                "where NOT EXISTS (select * " +
                                  "from shoppingCartProduct " +
                                  "where ShoppingCartID = {0} AND ProductID = {1})", shoppingCartID, productID).Count();
            if (queryResult.Equals(0) && shoppingCartProduct.Count() > 0)
            {
                Database.ExecuteSqlCommand("Update shoppingCartProduct " +
                                           "set Quantity = (Quantity + 1), DateModified = SYSDATETIME(), ModifiedBy = {0} " +
                                           "where ProductID = {1} AND ShoppingCartID = {2}", user.UserName, productID, shoppingCartID);
                return true;
            }
            else
            {
                Database.ExecuteSqlCommand("insert into shoppingCartProduct values ({0}, {1}, 1, SYSDATETIME(), {2}, null, null);",
                    shoppingCartID, productID, user.UserName);
                return false;
            }
        }
    }
}