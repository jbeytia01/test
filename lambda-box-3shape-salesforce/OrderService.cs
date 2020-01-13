using lambda_box_3shape_salesforce.Glidewell.Digital.Models;
using lambda_box_3shape_salesforce.Glidewell.Salesforce.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace lambda_box_3shape_salesforce
{
    public class OrderService
    {
        public async Task Process(EvenlyOrders order)
        {

            var sf = new Salesforce();

            // Authenticate and load lookup data.
            await sf.Load();

            // Find the accound id associated with Evenly
            var account = sf.Accounts.Find((a) => a.Name.Contains("Evenly Technologiesm Inc."));            

            // Find the Pricebook id associated with Evenly
            var pricebook = sf.Pricebooks.Find((p) => p.Name.Contains("Evenly"));            

            // Find the product id assocated with the product specified in the order received.
            var product = sf.Products.Find((p) => p.Name.ToUpper().Contains(order.Product.Trim().ToUpper()));

            // Find the pricebook entry id that matched the product id and pricebook id.
            var pricebookentry = sf.PriceBookEntries.Find((e) => e.Product2Id.Equals(product.Id) && e.Pricebook2Id.Equals(pricebook.Id));

            //
            // TODO: Need to populate salesforce custom fields (Order Type, Customer Email, External Reference ID.
            //       Order type = New Digital
            //       Customer Email = order.EmailAddress
            //       External Reference ID = order.OrderId
            //
            // TODO: Need to populate comments into the saleforce Notes 
            //       Include order.Comments into the Notes section with title of 'RX Notes'
            //

            // Create an instance of the order request
            var ro = new RootObject();

            // Create a new instance of the salesforce order object.
            var o = new Orders
            {
                attributes = new Attributes { type = "Order" },
                Status = "Created",
                accountId = account.Id, //Evenly Technologiesm Inc.
                billingCity = "Irvine",
                EffectiveDate = DateTime.Today.ToString("yyyy-MM-dd"),
                Pricebook2Id = pricebook.Id, // Evenly Pricebook
                OrderItems = new OrderItems { records = new List<Record>() }
            };

            var record = new Record
            {
                attributes = new Attributes2 { type = "OrderItem" },
                PricebookEntryId = pricebookentry.Id, 
                quantity = "1",
                UnitPrice = pricebookentry.UnitPrice.ToString()
            };

            o.OrderItems.records.Add(record);

            ro.order = new List<Orders>
            {
                o
            };

            // Create the order in salesforce.
            sf.CreateOrder(ro);

        }
    }
}
