using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.Lambda.Core;
using Amazon.Lambda.SNSEvents;
using lambda_box_3shape_salesforce.Glidewell.Digital.Models;
using Newtonsoft.Json;
using System;

// Assembly attribute to enable the Lambda function's JSON input to be converted into a .NET class.
[assembly: LambdaSerializer(typeof(Amazon.Lambda.Serialization.Json.JsonSerializer))]

namespace lambda_box_3shape_salesforce
{
    public class Function
    {
        IDynamoDBContext Dynamo { get; set; }
        ILambdaLogger Logger { get; set; }

        public Function()
        {
            Dynamo = new DynamoDBContext(new AmazonDynamoDBClient());
        }

        /// <summary>
        /// Process SNS message and create Evenly order into our SalesForce instance.
        /// </summary>
        /// <param name="input"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public void FunctionHandler(SNSEvent message, ILambdaContext context)
        {
            Logger = context.Logger;

            try
            {
                var body = message.Records[0].Sns.Message;

                var order = JsonConvert.DeserializeObject<EvenlyOrders>(body);

                var orderService = new OrderService();
                orderService.Process(order).Wait();

                Logger.LogLine($"Received the following order {body}");
            }
            catch (Exception)
            {

                throw;
            }
        }     
    }
}
