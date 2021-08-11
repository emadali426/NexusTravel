using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System.Collections.Generic;
using System.Configuration;

namespace TravelNexus.Services
{
    public partial class AmadeusService
    {
        public string BaseUrl { get; set; }
        public string ClientId { get; set; }
        public string ClientSecret { get; set; }
        public string AccessToken { get; set; }

        public AmadeusService()
        {
            BaseUrl = ConfigurationManager.AppSettings["AmadeusBaseUrl"];
            ClientId = ConfigurationManager.AppSettings["AmadeusApiKey"];
            ClientSecret = ConfigurationManager.AppSettings["AmadeusApiSecret"];
            AccessToken = GetAccessToken();
        }

        protected string GetAccessToken()
        {
            var client = new RestClient($"{BaseUrl}/v1/security/oauth2/token");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/x-www-form-urlencoded");
            request.AddParameter("client_id", ClientId);
            request.AddParameter("client_secret", ClientSecret);
            request.AddParameter("grant_type", "client_credentials");
            IRestResponse response = client.Execute(request);
            var res = JObject.Parse(response.Content);
            return res["access_token"].ToString();

        }

        //public Dictionary<string,string> GetCityAirportByKeyword(string keyword)
        //{
        //    var client = new RestClient($"{BaseUrl}/v1/reference-data/locations?subType=CITY,AIRPORT&keyword={keyword}");
        //    client.Timeout = -1;
        //    var request = new RestRequest(Method.GET);
        //    request.AddHeader("Authorization", $"Bearer {AccessToken}");
        //    IRestResponse response = client.Execute(request);
        //    var res = JsonConvert.DeserializeObject<LocationSearchResult>(response.Content);
        //    var result= new Dictionary<string, string>();
        //    foreach(var item in res.data)
        //    {
        //        result.Add(item.iataCode, item.detailedName);
        //    }
        //    return result;
        //}

    }
}
