using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreFront.Data
{
    class OrderRepository
    {
        /* 
           Return instance of order object which will return a specified order with all the fields populated 
           @param id - ID(primary key in DB) of an order.
        */
        public orders GetOrder(int id)
        {
            StoreFrontDataModel db = new StoreFrontDataModel();
            return db.orders.Find(id);
        }
    }
}