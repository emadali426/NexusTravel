using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Collections.Generic;

namespace TravelNexus.Services
{
    public partial class AmadeusService
    {

        public List<Hotel> SearchHotelOffers(string cityCode)
        {
            try
            {
                if (string.IsNullOrEmpty(cityCode))
                    return new List<Hotel>();
                if (cityCode.Length > 3)
                {
                    string code = Airports.GetCodeByName(cityCode);
                    cityCode = string.IsNullOrEmpty(code) ? cityCode : code;
                }
                var resultList = new List<Hotel>();
                var client = new RestClient($"{BaseUrl}/v2/shopping/hotel-offers?cityCode={cityCode}");
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", $"Bearer {AccessToken}");
                IRestResponse response = client.Execute(request);
                var responseContent = response.Content;
                var res = JObject.Parse(responseContent);
                foreach (var item in res["data"])
                {
                    var hotel = item["hotel"];
                    var offers = item["offers"][0]["price"];
                    Hotel f = new Hotel()
                    {
                        HotelId = hotel["hotelId"].Value<string>(),
                        Name = hotel["name"].Value<string>(),
                        Rating = hotel["rating"] == null ? 0 : hotel["rating"].Value<int>(),
                        City = hotel["address"]["cityName"].Value<string>(),
                        description = hotel["description"] == null ? "" : hotel["description"]["text"].Value<string>(),
                        average = offers["variations"]["average"] == null ? 0 :
                        offers["variations"]["average"]["base"] == null ? offers["variations"]["average"]["total"].Value<decimal>() : offers["variations"]["average"]["base"].Value<decimal>(),
                        Currency = offers["currency"].Value<string>(),
                        ImageSource = (hotel["media"] == null || hotel["media"][0] == null) ? "" : hotel["media"][0]["uri"].Value<string>(),
                        DetailsUrl = item["self"].Value<string>()
                    };
                    resultList.Add(f);
                }
                return resultList;
            }
            catch (Exception ex) { return new List<Hotel>(); }
        }
        public Hotel GetHotelByDetailsUrl(string url)
        {
            try
            {
                if (string.IsNullOrEmpty(url))
                    return new Hotel();
                var client = new RestClient(url);
                client.Timeout = -1;
                var request = new RestRequest(Method.GET);
                request.AddHeader("Authorization", $"Bearer {AccessToken}");
                IRestResponse response = client.Execute(request);
                var responseContent = response.Content;
                var res = JObject.Parse(responseContent);
                Hotel h = MapHotelDetails(res);
                return h;
            }
            catch (Exception ex) { return new Hotel(); }
        }

        private  Hotel MapHotelDetails(JObject res)
        {
            var item = res["data"];

            var hotel = item["hotel"];
            List<string> addressLines = new List<string>();
            if (hotel["address"]["lines"] != null)
            {
                foreach (var line in hotel["address"]["lines"])
                {
                    addressLines.Add(line.Value<string>());
                }
            }
            List<string> amenities = new List<string>();
            if (hotel["amenities"] != null)
            {
                foreach (var amenity in hotel["amenities"])
                {
                    amenities.Add(amenity.Value<string>().Replace("_", " "));
                }
            }
            List<string> media = new List<string>();
            if (hotel["media"] != null)
            {
                foreach (var medium in hotel["media"])
                {
                    media.Add(medium["uri"].Value<string>());
                }
            }
            List<Offer> offers = new List<Offer>();
            if (hotel["offers"] != null)
            {
                foreach (var offer in hotel["offers"])
                {
                    var room = offer["room"];
                    var price = offer["price"];
                    offers.Add(new Offer()
                    {
                        OfferId = offer["id"].Value<string>(),
                        Description =  room["description"]?["text"].Value<string>(),
                        RoomCategory =  room["typeEstimated"]?["category"].Value<string>(),
                        Beds = ((room == null || room["typeEstimated"] == null) ? 1 : room["typeEstimated"]["beds"].Value<int>()),
                        BedType = room["typeEstimated"]?["bedType"].Value<string>(),
                        Price = price == null ? 0 : price["total"].Value<decimal>(),
                        Currency = price == null ? "" : price["currency"].Value<string>(),
                        OfferDetailsUrl = offer["self"].Value<string>(),
                    });
                }
            }
            Hotel h = new Hotel()
            {
                HotelId = hotel["hotelId"].Value<string>(),
                Name = hotel["name"].Value<string>(),
                Rating = hotel["rating"] == null ? 0 : hotel["rating"].Value<int>(),
                City = hotel["address"]?["cityName"].Value<string>(),
                CountryCode = hotel["address"]?["countryCode"].Value<string>(),
                AddressLines = addressLines,
                Amenities = amenities,
                MediaUrls=media,
                Offers = offers,
                description = hotel["description"]?["text"].Value<string>(),// hotel["description"] == null ? "" :
                Fax =  hotel["contact"]?["fax"].Value<string>(),
                Phone =  hotel["contact"]?["phone"].Value<string>(),
            };
            return h;
        }
    }
}
