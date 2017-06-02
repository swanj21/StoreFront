using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ecommerce.Models
{
    public class SearchViewModel : CustomerBaseViewModel
    {
        /* @param string productName
           @return product(object) with the name specified by productName
           */
        public product SearchProducts(string productName)
        {
            // Get the ID corresponding to the productName.
            // Use that ID in spGetProduct to return the result.
            return new product();
        }
    }
}