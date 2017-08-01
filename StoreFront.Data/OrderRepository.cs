using System;
using System.Collections.Generic;
using System.Linq;

namespace StoreFront.Data
{
    public class OrderRepository
    {
        StoreFrontDataModel db = new StoreFrontDataModel();

        /* 
           Return instance of order object which will return a specified order with all the fields populated 
           @param id - ID(primary key in DB) of an order.
        */
        public orders GetOrder(int id)
        {
            if (id <= 0)
                throw new ArgumentOutOfRangeException("id", id, StoreFrontDataModel.INVALIDID);
            return db.orders.Find(id);
        }

        // This will return all orders placed b/w start and end.
        public List<orders> GetOrders(DateTime start, DateTime end)
        {
            return db.orders.Where(ord => ord.OrderDate > start && ord.OrderDate < end).ToList();
        }

        // This will get an order and change its status to 'Shipped'(3).
        public void MarkOrderShipped(int id)
        {
            db.orders.Find(id).StatusID = 3;
            db.SaveChanges();
        }
    }
}