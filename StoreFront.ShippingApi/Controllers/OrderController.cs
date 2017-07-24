using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StoreFront.ShippingApi.Controllers
{
    //
    // What else do I have to do?
    //
    public class OrderController : ApiController
    {
        // GET: api/Order
        public IEnumerable<string> Get()
        {
            return new string[] {};
        }

        // GET: api/Order/5
        public IEnumerable<string> Get(int id)
        {
            return new string[] { id.ToString() };
        }

        // Return all order objects b/w startDate and endDate
        public List<Data.orders> GetOrders(DateTime startDate, DateTime endDate)
        {
            Data.OrderRepository ordRepo = new Data.OrderRepository();
            return ordRepo.GetOrders(startDate, endDate);
        }

        // Mark the order as shipped
        public void MarkOrderShipped(int id)
        {
            Data.OrderRepository ordRepo = new Data.OrderRepository();
            ordRepo.MarkOrderShipped(id);
        }

        // POST: api/Order
        public void Post([FromBody]string value)
        {

        }

        // PUT(UPDATE): api/Order/5
        public void Put(int id, [FromBody]string value)
        {

        }

        // DELETE: api/Order/5
        public void Delete(int id)
        {

        }
    }
}
