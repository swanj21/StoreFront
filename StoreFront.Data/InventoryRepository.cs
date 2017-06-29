using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreFront.Data
{
    public class InventoryRepository
    {
        StoreFrontDataModel db = new StoreFrontDataModel();

        /* Returns a list of products that have either a name or description containing the search text */
        public List<product> SearchProducts(string searchText)
        {            
            var searchResult = db.Database.SqlQuery<product>(
                "SELECT * " +
                "FROM product " +
                "WHERE product.ProductName LIKE '%{0}%' or " +
                "product.ProductDescription LIKE '%{0}%'", searchText);
            return searchResult.ToList();
        }
        /* Return an instance of a product with all the fields populated */
        public product GetProduct(int id)
        {
            return db.product.Find(id);
        }
    }
}