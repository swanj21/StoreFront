using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Web;
using StoreFront.InventoryService;
using System.ServiceModel.Description;

namespace Ecommerce
{
    public class InvSvcTest
    {
        static void Main(string[] args)
        {
            // Step 1
            // Create a URI to serve as the base address
            Uri baseAddress = new Uri("http://localhost:8000/InvSvcTest");

            // Step 2
            // Create the ServiceHost
            ServiceHost host = new ServiceHost(typeof(InventoryService), baseAddress);

            // Step 3
            // Add a service endpoint(not needed)
            host.AddServiceEndpoint(typeof(IInventoryService), new WSHttpBinding(), "InventoryService");

            // Step 4
            // Enable metadata exchange
            ServiceMetadataBehavior smb = new ServiceMetadataBehavior();
            smb.HttpGetEnabled = true;
            host.Description.Behaviors.Add(smb);

            // Step 5
            // Start, and stop, the service
            host.Open();
            Console.WriteLine("Service is open.");
            Console.ReadLine();
            // Shutdown the service
            host.Close();
        }
    }
}