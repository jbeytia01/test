using lambda_box_3shape_salesforce.Glidewell.Salesforce.Models;
using NetCoreForce.Client;
using NetCoreForce.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace lambda_box_3shape_salesforce
{
    public class Salesforce
    {
        public List<SfPricebook2> Pricebooks { get; private set; }
        public List<SfProduct2> Products { get; private set; }
        public List<SfPricebookEntry> PriceBookEntries { get; private set; }
        public List<SfAccount> Accounts { get; private set; }
        public string AccessToken { get; private set; }

        public async Task Load()
        {
            var auth = new AuthenticationClient();

            //
            //TODO: Move token information to Lambda Environment variables.
            //       
            //

            //Pass in the login information
            await auth.UsernamePasswordAsync("3MVG9LBJLApeX_PBn4SXK4vWIZgUwaByh5aAkEKtY_N8.E7cwD1.bEKSetVd5EjOnuie6kB1gmDridf8KPpSz",
                "439C541B6523A7F8C5F8BD6CCFEFC6F184C0FEE89FF2D3C95F58156A3C060923",
                "kartikl@augustahitech.com",
                "Gl1d3w3ll!2019", "https://login.salesforce.com/services/oauth2/token");

            //the AuthenticationClient object will then contain the instance URL and access token to be used in each of the API calls
            ForceClient client = new ForceClient(auth.AccessInfo.InstanceUrl, auth.ApiVersion, auth.AccessInfo.AccessToken);

            Pricebooks = client.Query<SfPricebook2>("SELECT Id, Name FROM Pricebook2").Result;
            Products = client.Query<SfProduct2>("SELECT Id, Name FROM Product2").Result;
            PriceBookEntries = client.Query<SfPricebookEntry>("SELECT Id, Name, Product2Id, Pricebook2Id, UnitPrice FROM PricebookEntry").Result;
            Accounts = client.Query<SfAccount>("SELECT Id, Name FROM Account").Result;
            AccessToken = auth.AccessInfo.AccessToken;

        }

        public void CreateOrder(RootObject order)
        {
            //TODO: Move URL to lambda environment varialble
            var client = new RestClient
            {
                BaseUrl = new Uri("https://na174.salesforce.com/services/data/v47.0/commerce/sale/order")
            };

            var request = new RestRequest(Method.POST)
            {
                RequestFormat = DataFormat.Json
            };

            request.AddHeader("Authorization", "Bearer " + AccessToken);
            request.AddParameter("application/json; charset=utf-8", JsonConvert.SerializeObject(order), ParameterType.RequestBody);
            var response = client.Execute(request);

            if(!response.IsSuccessful)
            {
                throw response.ErrorException;
            }
        }

    }
}
