using System;

namespace lambda_box_3shape_salesforce.Glidewell.Digital.Models
{
    public class EvenlyOrders
    {
        /// <summary>
        /// Should be entered as the external reference id in salesforce
        /// </summary>
        public string OrderId { get; set; }

        /// <summary>
        /// The type of scanner that generated this order.  Can we include this in a custom saleforce order field?
        /// </summary>
        public string SourceSystem { get; set; }

        /// <summary>
        /// Should be entered in the email address field in salesforce
        /// </summary>
        public string EmailAddress { get; set; }

        /// <summary>
        /// The patient information for the digital order.  Do Not include in salesforce order.
        /// </summary>
        public string PatientName { get; set; }

        /// <summary>
        /// This should select the corresponding product in salesforce.
        /// </summary>
        public string Product { get; set; }

        /// <summary>
        /// The RX comments for the digital order.
        /// </summary>
        public string Comments { get; set; }

        /// <summary>
        /// The date this digital order was generated.
        /// </summary>
        public DateTime DateCreated { get; set; }        
    }
}
