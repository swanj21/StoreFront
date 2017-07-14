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

        /* Returns a list of products that have either a name or description containing the search text */
        public List<product> SearchProducts(string searchText)
        {
            return db.product.Where(prod => prod.ProductName.Contains(searchText)).ToList<product>();
        }

        /* Return an instance of a product with all the fields populated */
        public product GetProduct(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException("id", id, StoreFrontDataModel.INVALIDID);
            return db.product.Find(id);
        }
    }
}