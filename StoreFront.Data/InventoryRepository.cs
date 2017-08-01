using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreFront.Data
{
    public class InventoryRepository
    {
        StoreFrontDataModel db = new StoreFrontDataModel();
        public List<product> prodList = new List<product>();

        /* Returns a list of products that have either a name or description containing the search text */
        public List<product> SearchProducts(string searchText)
        {
            prodList = db.product.Where(prod => prod.ProductName.Contains(searchText)).ToList<product>();
            
            return prodList;
        }

        /* Return an instance of a product with all the fields populated */
        public product GetProduct(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException("id", id, StoreFrontDataModel.INVALIDID);
            return db.product.Find(id);
        }

        /* Return true/false depending on if the product is in the cart already or if it needs to be put in */
        public bool AddShoppingCartItem(int productID, int shoppingCartID, users user)
        {
            Object queryResult = null;
            queryResult = db.Database.SqlQuery<Object>(
                "select * " +
                "from shoppingCartProduct " +
                "where NOT EXISTS (select * " +
                                  "from shoppingCartProduct " +
                                  "where ShoppingCartID = {0} AND ProductID = {1})", shoppingCartID, productID).Count();
            if (queryResult.Equals(0) && db.shoppingCartProduct.Count() > 0)
            {
                db.Database.ExecuteSqlCommand("Update shoppingCartProduct " +
                                           "set Quantity = (Quantity + 1), DateModified = SYSDATETIME(), ModifiedBy = {0} " +
                                           "where ProductID = {1} AND ShoppingCartID = {2}", user.UserName, productID, shoppingCartID);
                return true;
            }
            else
            {
                db.Database.ExecuteSqlCommand("insert into shoppingCartProduct values ({0}, {1}, 1, SYSDATETIME(), {2}, null, null);",
                    shoppingCartID, productID, user.UserName);
                return false;
            }
        }
    }
}