using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelNexus.Services
{

    public partial class AmadeusService
    {

        //public List<Flight> SearchFlight(string origin, string Destination, DateTime? departuredate, DateTime? returndate)
        //{if (departuredate.HasValue && departuredate < DateTime.Today)
        //        departuredate = null;
        //    var resultList = new List<Flight>();
        //    var client = new RestClient($"{BaseUrl}/v1/shopping/flight-dates?origin={origin}&destination={Destination}{(departuredate.HasValue? "&departureDate="+ departuredate.Value.ToString("yyyy-MM-dd") : "")}");
        //    client.Timeout = -1;
        //    var request = new RestRequest(Method.GET);
        //    request.AddHeader("Authorization", $"Bearer {AccessToken}");
        //    IRestResponse response = client.Execute(request);
        //    var res = JObject.Parse(response.Content);
        //    foreach(var item in res["data"])
        //    {
        //        Flight f = new Flight()
        //        {
        //            DepartureDate = item["departureDate"].Value<DateTime>(),
        //            ArrivalDate = item["returnDate"].Value<DateTime>(),
        //            Origin = item["origin"].Value<string>(),
        //            Destination = item["destination"].Value<string>(),
        //            Price = item["price"]["total"].Value<decimal>(),
        //            flightDestinations = item["links"]["flightDestinations"].Value<string>(),
        //            flightOffers = item["links"]["flightOffers"].Value<string>(),
        //        };
        //        resultList.Add(f);
        //    }
        //    return resultList;
        //}
        public List<Flight> SearchFlightOffers(string origin, string Destination, DateTime departuredate, DateTime returndate, int adults, bool nonstop)
        {
            if (string.IsNullOrEmpty(origin) || string.IsNullOrEmpty(Destination))
                return new List<Flight>();
            if (origin.Length > 3)
            {
                string code = Airports.GetCodeByName(origin);
                origin = string.IsNullOrEmpty(code) ? origin : code;
            }
            if (Destination.Length > 3)
            {
                string code = Airports.GetCodeByName(Destination);
                Destination = string.IsNullOrEmpty(code) ? Destination : code;
            }
            var resultList = new List<Flight>();
            var client = new RestClient($"{BaseUrl}/v2/shopping/flight-offers?originLocationCode={origin}&destinationLocationCode={Destination}&departureDate={departuredate.ToString("yyyy-MM-dd")}&returnDate={returndate.ToString("yyyy-MM-dd")}&adults={adults}");
            client.Timeout = -1;
            var request = new RestRequest(Method.GET);
            request.AddHeader("Authorization", $"Bearer {AccessToken}");
            IRestResponse response = client.Execute(request);
            var responseContent = response.Content;
            var res = JObject.Parse(responseContent);
            if (res["data"] == null)
                return resultList;

            //Dictionary<string, string> airlines = new Dictionary<string, string>();

            if (res["dictionaries"] != null)
            {
                var carriers = res["dictionaries"]["carriers"];
                if (carriers != null)
                {
                    foreach (var item in carriers)
                    {
                        if (Airlines.AirlineCodes == null)
                            Airlines.AirlineCodes = new Dictionary<string, string>();
                        if (!Airlines.AirlineCodes.Any(x=>x.Key==((Newtonsoft.Json.Linq.JProperty)item).Name))
                            Airlines.AirlineCodes.Add(((Newtonsoft.Json.Linq.JProperty)item).Name, ((Newtonsoft.Json.Linq.JProperty)item).Value.ToString());
                    }
                }
            }

            // resultList = res["data"].ToObject<List<Flight>>();

            foreach (var item in res["data"])
            {
                Flight f = MapFlightDetails(item);
                resultList.Add(f);
            }
            return resultList;
        }

        public  Flight MapFlightDetails(JToken item)
        {
            var seg = item["itineraries"][0]["segments"][0];
         //   var al = airlines[seg["carrierCode"].Value<string>()];
            Flight f = JsonConvert.DeserializeObject<Flight>(item.ToString());
            foreach(var itinerary in f.itineraries)
            {
                foreach (var segment in itinerary.segments)
                {
                    var al = Airlines.AirlineCodes[segment.carrierCode];
                    segment.Airline = string.IsNullOrEmpty(al) ? segment.carrierCode : al;
                }
            }
            if(f.itineraries.Count > 0 && f.itineraries[0].segments.Count > 0)
            {
                var departure = f.itineraries[0].segments[0].departure;
                var origin = departure.iataCode;
                string orig = Airports.GetNameByCode(origin);
                f.Origin = string.IsNullOrEmpty(orig) ? origin : orig;
                f.DepartureDate = departure.at;

                var segments = f.itineraries[0].segments;
                var arrive = segments[segments.Count-1].arrival;
                var destination = arrive.iataCode;
                string dest = Airports.GetNameByCode(destination);
                f.Destination = string.IsNullOrEmpty(dest) ? destination : dest;
                f.ArrivalDate = arrive.at;

            }

            f.NoOfStops = f.itineraries.Count > 0 ? f.itineraries[0].segments.Count - 1 : 0;
            f.FlightOffer = item.ToString();
            decimal price;
            decimal.TryParse(f.price.total, out price);
            f.AvgPrice = price;
            f.Currency = f.price.currency;
            //Flight f = new Flight()
            //{
            //    FlightOffer = item.ToString(),
            //    DepartureDate = seg["departure"]["at"].Value<DateTime>(),
            //    ArrivalDate = seg["arrival"]["at"].Value<DateTime>(),
            //    NoOfStops = seg["numberOfStops"].Value<int>(),
            //    NoOfBookableSeats = item["numberOfBookableSeats"].Value<int>(),
            //    IsOneWay = item["oneWay"].Value<bool>(),
            //    Origin = Airports.GetNameByCode(seg["departure"]?["iataCode"].Value<string>()),
            //    Destination = Airports.GetNameByCode(seg["arrival"]?["iataCode"].Value<string>()),
            //    Price = item["price"]["total"].Value<decimal>(),
            //    Currency = item["price"]["currency"].Value<string>(),
            //    Airline = string.IsNullOrEmpty(al) ? item["carrierCode"].Value<string>() : al,
            //    //flightDestinations = item["links"]?["flightDestinations"].Value<string>(),
            //    //flightOffers = item["links"]?["flightOffers"].Value<string>(),
            //};
            return f;
        }

        //public List<Flight> SearchCheapestFlightDates(string origin, string Destination)
        //{
        //    if (string.IsNullOrEmpty(origin) || string.IsNullOrEmpty(Destination))
        //        return new List<Flight>();
        //    if (origin.Length > 3)
        //    {
        //        string code = Cities.GetCodeByName(origin);
        //        origin = string.IsNullOrEmpty(code) ? origin : code;
        //    }
        //    if (Destination.Length > 3)
        //    {
        //        string code = Cities.GetCodeByName(Destination);
        //        Destination = string.IsNullOrEmpty(code) ? Destination : code;
        //    }
        //    var resultList = new List<Flight>();
        //    var client = new RestClient($"{BaseUrl}/v1/shopping/flight-dates?origin={origin}&destination={Destination}");
        //    client.Timeout = -1;
        //    var request = new RestRequest(Method.GET);
        //    request.AddHeader("Authorization", $"Bearer {AccessToken}");
        //    IRestResponse response = client.Execute(request);
        //    var res = JObject.Parse(response.Content);
        //    foreach (var item in res["data"])
        //    {
        //        Flight f = new Flight()
        //        {
        //            DepartureDate = item["departureDate"].Value<DateTime>(),
        //            ArrivalDate = item["returnDate"].Value<DateTime>(),
        //            Origin = Cities.GetNameByCode(item["origin"].Value<string>()),
        //            Destination = Cities.GetNameByCode(item["destination"].Value<string>()),
        //            Price = item["price"]["total"].Value<decimal>(),
        //            flightDestinations = item["links"]["flightDestinations"].Value<string>(),
        //            flightOffers = item["links"]["flightOffers"].Value<string>(),
        //        };
        //        resultList.Add(f);
        //    }
        //    return resultList;

        //}
        //public List<Flight> SearchFlightInspiration(string origin)
        //{
        //    if (string.IsNullOrEmpty(origin) )
        //        return new List<Flight>();
        //    if (origin.Length > 3)
        //    {
        //        string code = Cities.GetCodeByName(origin);
        //        origin = string.IsNullOrEmpty(code) ? origin : code;
        //    }
        //    var resultList = new List<Flight>();
        //    var client = new RestClient($"{BaseUrl}/v1/shopping/flight-destinations?origin={origin}");
        //    client.Timeout = -1;
        //    var request = new RestRequest(Method.GET);
        //    request.AddHeader("Authorization", $"Bearer {AccessToken}");
        //    IRestResponse response = client.Execute(request);
        //    var res = JObject.Parse(response.Content);
        //    if (res["data"] == null)
        //        return resultList;

        //    foreach (var item in res["data"])
        //    {
        //        var seg = item["itineraries"][0]["segments"][0];
        //        Flight f = new Flight()
        //        {

        //            DepartureDate = seg["departure"]["at"].Value<DateTime>(),
        //            ArrivalDate = seg["arrival"]["at"].Value<DateTime>(),
        //            NoOfStops = seg["numberOfStops"].Value<int>(),
        //            NoOfBookableSeats = item["numberOfBookableSeats"].Value<int>(),
        //            IsOneWay = item["oneWay"].Value<bool>(),
        //            Origin = Cities.GetNameByCode(seg["departure"]["iataCode"].Value<string>()),
        //            Destination = Cities.GetNameByCode(seg["arrival"]["iataCode"].Value<string>()),
        //            Price = item["price"]["total"].Value<decimal>(),
        //            Currency = item["price"]["currency"].Value<string>(),
        //            //flightDestinations = item["links"]?["flightDestinations"].Value<string>(),
        //            //flightOffers = item["links"]?["flightOffers"].Value<string>(),
        //        };
        //        resultList.Add(f);
        //    }
        //    return resultList;
        //}

        public Flight GetFlightOfferDetails(string flighturl)
        {
            if (string.IsNullOrEmpty(flighturl))
                return new Flight();
            var client = new RestClient(flighturl);
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {AccessToken}");
         //   request.AddParameter("application/json", flight, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var res = JObject.Parse(response.Content);
            if (res["data"] == null)
                return new Flight();
            Dictionary<string, string> airlines = new Dictionary<string, string>();

            if (res["dictionaries"] != null)
            {
                var carriers = res["dictionaries"]["carriers"];
                if (carriers != null)
                {
                    foreach (var carrier in carriers)
                    {
                        if (Airlines.AirlineCodes == null)
                            Airlines.AirlineCodes = new Dictionary<string, string>();
                        if (string.IsNullOrEmpty(Airlines.AirlineCodes[((Newtonsoft.Json.Linq.JProperty)carrier).Name]))
                            Airlines.AirlineCodes.Add(((Newtonsoft.Json.Linq.JProperty)carrier).Name, ((Newtonsoft.Json.Linq.JProperty)carrier).Value.ToString());
                    }
                }
            }

            var item = res["data"].FirstOrDefault();

            var seg = item["itineraries"][0]["segments"][0];
            var al = airlines[seg["carrierCode"].Value<string>()];
            Flight f = new Flight()
            {
                FlightOffer = item.Value<string>(),
                DepartureDate = seg["departure"]["at"].Value<DateTime>(),
                ArrivalDate = seg["arrival"]["at"].Value<DateTime>(),
                NoOfStops = seg["numberOfStops"].Value<int>(),
                NoOfBookableSeats = item["numberOfBookableSeats"].Value<int>(),
                IsOneWay = item["oneWay"].Value<bool>(),
                Origin = Airports.GetNameByCode(seg["departure"]["iataCode"].Value<string>()),
                Destination = Airports.GetNameByCode(seg["arrival"]["iataCode"].Value<string>()),
                AvgPrice = item["price"]["total"].Value<decimal>(),
                Currency = item["price"]["currency"].Value<string>(),
                Airline = string.IsNullOrEmpty(al) ? item["carrierCode"].Value<string>() : al,
                //flightDestinations = item["links"]?["flightDestinations"].Value<string>(),
                //flightOffers = item["links"]?["flightOffers"].Value<string>(),
            };
            return f;
        }

        public FlightPrices GetFlightOfferPrices(string flightOffer)
        {
            if (string.IsNullOrEmpty(flightOffer))
                return new FlightPrices();
            var client = new RestClient($"{BaseUrl}/v1/shopping/flight-offers/pricing");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {AccessToken}");

            string body = $"{{\"data\": {{\"type\": \"flight-offers-pricing\",\"flightOffers\": [{flightOffer}]}}}}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var res = JObject.Parse(response.Content);
            if (res["data"] == null)
                return new FlightPrices();
            var flightPrice = JsonConvert.DeserializeObject<FlightPrices>(res["data"].ToString());
            if (flightPrice!= null) {
                foreach (var item in flightPrice.flightOffers)
                {
                    item.FlightOfferJson = JsonConvert.SerializeObject(item);
                } }
           // flightPrice.FlightOffersJson = res["data"]["flightOffers"]?.ToString();
            return flightPrice;
        }

        public FlightPrices CreateFlightOrder(string flightOffer, bookingObject bookingObj)
        {
            if (string.IsNullOrEmpty(flightOffer))
                return new FlightPrices();
            var client = new RestClient($"{BaseUrl}/v1/shopping/flight-offers/pricing");
            client.Timeout = -1;
            var request = new RestRequest(Method.POST);
            request.AddHeader("Content-Type", "application/json");
            request.AddHeader("Authorization", $"Bearer {AccessToken}");

            string body = $"{{\"data\": {{\"type\": \"flight-order\",\"flightOffers\": [{flightOffer}],\"travelers\": {JsonConvert.SerializeObject(bookingObj.travelers)},\"remarks\": {JsonConvert.SerializeObject(bookingObj.remarks)},  \"ticketingAgreement\":{JsonConvert.SerializeObject(bookingObj.ticketingAgreement)}, \"contacts\": {JsonConvert.SerializeObject(bookingObj.contacts)}}}}}";
            request.AddParameter("application/json", body, ParameterType.RequestBody);
            IRestResponse response = client.Execute(request);
            var res = JObject.Parse(response.Content);
            if (res["data"] == null)
                return new FlightPrices();
            var flightPrice = JsonConvert.DeserializeObject<FlightPrices>(res["data"].ToString());
            return flightPrice;
        }


    }
}
