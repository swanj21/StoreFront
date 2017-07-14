using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;
using StoreFront.Data;

namespace StoreFront.InventoryService
{
    // USE THIS: https://msdn.microsoft.com/en-us/magazine/dd943053.aspx
    public class InventoryService : IInventoryService
    {
        InventoryRepository invRepo = new InventoryRepository();

        // Contains default implementation of the service contract
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }

        public product GetProductDetails(int id)
        {
            return invRepo.GetProduct(id);
        }

        public List<product> SearchProducts(string text)
        {
            return invRepo.SearchProducts(text);
        }
    }
}
