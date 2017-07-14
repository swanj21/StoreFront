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
            // Mark an order as shipped using the function in OrderRepository.
            StoreFront.Data.OrderRepository ordRepo = new StoreFront.Data.OrderRepository();
            List<StoreFront.Data.orders> ordList = new List<StoreFront.Data.orders>();

            DateTime startTime = new DateTime();
            DateTime endTime = new DateTime();
            int id = 1;

            ordList = ordRepo.GetOrders(startTime, endTime);
            // ordRepo.MarkOrderShipped(id);
            return new string[] { startTime.ToString(), endTime.ToString(), id.ToString() };
        }

        // GET: api/Order/5
        public string Get(int id)
        {
            return id.ToString();
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
