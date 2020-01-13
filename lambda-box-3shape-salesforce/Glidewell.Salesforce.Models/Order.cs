using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace lambda_box_3shape_salesforce.Glidewell.Salesforce.Models
{
    public class Attributes
    {
        public string type { get; set; }
    }

    public class Attributes2
    {
        public string type { get; set; }
    }

    public class Record
    {
        public Attributes2 attributes { get; set; }
        public string PricebookEntryId { get; set; }
        public string quantity { get; set; }
        public string UnitPrice { get; set; }
    }

    public class OrderItems
    {
        public List<Record> records { get; set; }
    }

    public class Orders
    {
        public Attributes attributes { get; set; }
        public string EffectiveDate { get; set; }
        public string Status { get; set; }
        public string billingCity { get; set; }
        public string accountId { get; set; }
        public string Pricebook2Id { get; set; }
        //public string External_Reference_ID { get; set; }
        public OrderItems OrderItems { get; set; }
    }

    public class RootObject
    {
        [JsonProperty("order")]
        public List<Orders> order { get; set; }
    }

    /* Model generated from this https://developer.salesforce.com/docs/atlas.en-us.api_placeorder.meta/api_placeorder/sforce_placeorder_rest_api_place_order_account.htm
     * 
     * {
   "order": [
   {
      "attributes": {
      "type": "Order"
      },
      "EffectiveDate": "2013-07-11",
      "Status": "Draft",
      "billingCity": "SFO-Inside-OrderEntity-1",
      "accountId": "001D000000JRDAv",
      "Pricebook2Id": "01sD0000000G2NjIAK",
      "OrderItems": {
         "records": [
            {
            "attributes": {
               "type": "OrderItem"
            },
            "PricebookEntryId": "01uD0000001c5toIAA",
            "quantity": "1",
            "UnitPrice": "15.99"
            }
         ]
      }
   }
   ]
}
     * */
}
