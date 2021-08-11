using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DOTWconnect.Service.Entities
{
    namespace SearchHotelsResponse
    {

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Xml
        {

            [JsonProperty("@version")]
            public string Version { get; set; }

            [JsonProperty("@encoding")]
            public string Encoding { get; set; }
        }

        public class FullAddress
        {

            [JsonProperty("hotelStreetAddress")]
            public string HotelStreetAddress { get; set; }

            [JsonProperty("hotelZipCode")]
            public string HotelZipCode { get; set; }

            [JsonProperty("hotelCountry")]
            public string HotelCountry { get; set; }

            [JsonProperty("hotelState")]
            public string HotelState { get; set; }

            [JsonProperty("hotelCity")]
            public string HotelCity { get; set; }
        }

        public class Language
        {

            [JsonProperty("@id")]
            public string Id { get; set; }

            [JsonProperty("@name")]
            public string Name { get; set; }

            [JsonProperty("#cdata-section")]
            public string CdataSection { get; set; }

            [JsonProperty("amenitieItem")]
            [JsonConverter(typeof(JsonAmenitieItemListConverter))]
            public List<AmenitieItem> AmenitieItem { get; set; }


            [JsonProperty("leisureItem")]
            [JsonConverter(typeof(JsonLeisureItemListConverter))]
            public List<LeisureItem> LeisureItem { get; set; }


            [JsonProperty("businessItem")]
            [JsonConverter(typeof(JsonBusinessItemListConverter))]
            public List<BusinessItem> BusinessItem { get; set; }

        }

        public class Description1
        {

            [JsonProperty("language")]
            public Language Language { get; set; }
        }

        public class Description2
        {

            [JsonProperty("language")]
            public Language Language { get; set; }
        }


        public class AmenitieItem
        {

            [JsonProperty("@id")]
            public string Id { get; set; }

            [JsonProperty("#text")]
            public string Text { get; set; }
        }

        public class Amenitie
        {
            [JsonProperty("@count")]
            public string Count { get; set; }

            [JsonProperty("language")]
            public Language Language { get; set; }
        }


        public class LeisureItem
        {

            [JsonProperty("@id")]
            public string Id { get; set; }

            [JsonProperty("#text")]
            public string Text { get; set; }
        }

        public class Leisure
        {

            [JsonProperty("@count")]
            public string Count { get; set; }

            [JsonProperty("language")]
            public Language Language { get; set; }
        }


        public class BusinessItem
        {

            [JsonProperty("@id")]
            public string Id { get; set; }

            [JsonProperty("#text")]
            public string Text { get; set; }
        }

        public class Business
        {

            [JsonProperty("@count")]
            public string Count { get; set; }

            [JsonProperty("language")]
            public Language Language { get; set; }
        }


        public class Dist
        {

            [JsonProperty("@attr")]
            public string Attr { get; set; }

            [JsonProperty("#text")]
            public string Text { get; set; }
        }


        public class Airport
        {

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("dist")]
            [JsonConverter(typeof(JsonDistListConverter))]
            public List<Dist> Dist { get; set; }

            [JsonProperty("directions")]
            public string Directions { get; set; }
        }

        public class Airports
        {

            [JsonProperty("@name")]
            public string Name { get; set; }

            [JsonProperty("airport")]
            [JsonConverter(typeof(JsonAirPortListConverter))]
            public List<Airport> Airport { get; set; }
        }


        public class Rail
        {

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("dist")]
            [JsonConverter(typeof(JsonDistListConverter))]
            public List<Dist> Dist { get; set; }

            [JsonProperty("directions")]
            public string Directions { get; set; }
        }

        public class Rails
        {

            [JsonProperty("@name")]
            public string Name { get; set; }

            [JsonProperty("rail")]
            [JsonConverter(typeof(JsonRailListConverter))]
            public List<Rail> Rail { get; set; }
        }

        public class Subway
        {

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("dist")]
            [JsonConverter(typeof(JsonDistListConverter))]
            public List<Dist> Dist { get; set; }

            [JsonProperty("directions")]
            public string Directions { get; set; }
        }

        public class Subways
        {

            [JsonProperty("@name")]
            public string Name { get; set; }

            [JsonProperty("subway")]
            [JsonConverter(typeof(JsonSubwayListConverter))]
            public List<Subway> Subway { get; set; }
        }

        public class Cruise
        {

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("dist")]
            [JsonConverter(typeof(JsonDistListConverter))]
            public List<Dist> Dist { get; set; }

            [JsonProperty("directions")]
            public string Directions { get; set; }
        }

        public class Cruises
        {

            [JsonProperty("@name")]
            public string Name { get; set; }

            [JsonProperty("cruise")]
            [JsonConverter(typeof(JsonCruiseListConverter))]
            public List<Cruise> Cruise { get; set; }
        }

        public class Transportation
        {

            [JsonProperty("airports")]
            public Airports Airports { get; set; }

            [JsonProperty("rails")]
            public Rails Rails { get; set; }

            [JsonProperty("subways")]
            public Subways Subways { get; set; }

            [JsonProperty("cruises")]
            public Cruises Cruises { get; set; }
        }

        public class Thumb
        {

            [JsonProperty("#cdata-section")]
            public string CdataSection { get; set; }
        }

        public class Category
        {

            [JsonProperty("@id")]
            public string Id { get; set; }

            [JsonProperty("#text")]
            public string Text { get; set; }
        }

        public class Url
        {

            [JsonProperty("#cdata-section")]
            public string CdataSection { get; set; }
        }

        public class Image
        {

            [JsonProperty("@runno")]
            public string Runno { get; set; }

            [JsonProperty("alt")]
            public string Alt { get; set; }

            [JsonProperty("category")]
            public Category Category { get; set; }

            [JsonProperty("url")]
            public Url Url { get; set; }
        }

        public class HotelImages
        {

            [JsonProperty("@count")]
            public string Count { get; set; }

            [JsonProperty("thumb")]
            public Thumb Thumb { get; set; }

            [JsonProperty("image")]
            [JsonConverter(typeof(JsonImageListConverter))]
            public List<Image> Image { get; set; }
        }

        public class Images
        {

            [JsonProperty("hotelImages")]
            public HotelImages HotelImages { get; set; }
        }

        public class HotelPreference
        {

            [JsonProperty("@count")]
            public string Count { get; set; }
        }

        public class GeoPoint
        {

            [JsonProperty("lat")]
            public string Lat { get; set; }

            [JsonProperty("lng")]
            public string Lng { get; set; }
        }

        public class GeoLocation
        {

            [JsonProperty("@runno")]
            public string Runno { get; set; }

            [JsonProperty("@id")]
            public string Id { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("type")]
            public string Type { get; set; }

            [JsonProperty("distance")]
            public string Distance { get; set; }
        }

        public class GeoLocations
        {
            [JsonProperty("@count")]
            public string Count { get; set; }

            [JsonProperty("geoLocation")]
            [JsonConverter(typeof(JsonGeoLocationListConverter))]
            public List<GeoLocation> GeoLocation { get; set; }
        }

        public class Amenity
        {

            [JsonProperty("@id")]
            public string Id { get; set; }

            [JsonProperty("#text")]
            public string Text { get; set; }
        }

        public class RoomAmenities
        {

            [JsonProperty("@count")]
            public string Count { get; set; }

            [JsonProperty("amenity")]
            [JsonConverter(typeof(JsonAmenityListConverter))]
            public List<Amenity> Amenity { get; set; }
        }

        public class RoomInfo
        {

            [JsonProperty("maxAdult")]
            public string MaxAdult { get; set; }

            [JsonProperty("maxExtraBed")]
            public string MaxExtraBed { get; set; }

            [JsonProperty("maxChildren")]
            public string MaxChildren { get; set; }
        }

        public class RoomCapacityInfo
        {

            [JsonProperty("roomPaxCapacity")]
            public string RoomPaxCapacity { get; set; }

            [JsonProperty("allowedAdultsWithoutChildren")]
            public string AllowedAdultsWithoutChildren { get; set; }

            [JsonProperty("allowedAdultsWithChildren")]
            public string AllowedAdultsWithChildren { get; set; }

            [JsonProperty("maxExtraBed")]
            public string MaxExtraBed { get; set; }
        }

        public class RoomType
        {

            [JsonProperty("@runno")]
            public string Runno { get; set; }

            [JsonProperty("@roomtypecode")]
            public string Roomtypecode { get; set; }

            [JsonProperty("roomAmenities")]
            public RoomAmenities RoomAmenities { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("twin")]
            public string Twin { get; set; }

            [JsonProperty("roomInfo")]
            public RoomInfo RoomInfo { get; set; }

            [JsonProperty("roomCapacityInfo")]
            public RoomCapacityInfo RoomCapacityInfo { get; set; }
        }

        public class Room
        {
            [JsonProperty("@runno")]
            public string Runno { get; set; }

            [JsonProperty("@count")]
            public string Count { get; set; }

            [JsonProperty("roomType")]
            [JsonConverter(typeof(JsonRoomTypeListConverter))]
            public List<RoomType> RoomType { get; set; }
        }

        public class Rooms
        {

            [JsonProperty("@count")]
            public string Count { get; set; }

            [JsonProperty("room")]
            public Room Room { get; set; }
        }

        public class Hotel
        {

            [JsonProperty("@runno")]
            public string Runno { get; set; }

            [JsonProperty("@preferred")]
            public string Preferred { get; set; }

            [JsonProperty("@cityname")]
            public string CityNameAttr { get; set; }

            [JsonProperty("@hotelid")]
            public string Hotelid { get; set; }

            [JsonProperty("exclusive")]
            public string Exclusive { get; set; }

            [JsonProperty("healthCompliant")]
            public string HealthCompliant { get; set; }

            [JsonProperty("builtYear")]
            public string BuiltYear { get; set; }

            [JsonProperty("renovationYear")]
            public string RenovationYear { get; set; }

            [JsonProperty("floors")]
            public string Floors { get; set; }

            [JsonProperty("noOfRooms")]
            public string NoOfRooms { get; set; }

            [JsonProperty("luxury")]
            public string Luxury { get; set; }

            [JsonProperty("fullAddress")]
            public FullAddress FullAddress { get; set; }

            [JsonProperty("description1")]
            public Description1 Description1 { get; set; }

            [JsonProperty("description2")]
            public Description2 Description2 { get; set; }

            [JsonProperty("hotelName")]
            public string HotelName { get; set; }

            [JsonProperty("address")]
            public string Address { get; set; }

            [JsonProperty("zipCode")]
            public string ZipCode { get; set; }

            [JsonProperty("location")]
            public string Location { get; set; }

            [JsonProperty("locationId")]
            public string LocationId { get; set; }

            [JsonProperty("location1")]
            public string Location1 { get; set; }

            [JsonProperty("location2")]
            public string Location2 { get; set; }

            [JsonProperty("location3")]
            public string Location3 { get; set; }

            [JsonProperty("cityName")]
            public string CityName { get; set; }

            [JsonProperty("cityCode")]
            public string CityCode { get; set; }

            [JsonProperty("stateName")]
            public string StateName { get; set; }

            [JsonProperty("stateCode")]
            public string StateCode { get; set; }

            [JsonProperty("countryName")]
            public string CountryName { get; set; }

            [JsonProperty("countryCode")]
            public string CountryCode { get; set; }

            [JsonProperty("regionName")]
            public string RegionName { get; set; }

            [JsonProperty("regionCode")]
            public string RegionCode { get; set; }

            [JsonProperty("areaName")]
            public string AreaName { get; set; }

            [JsonProperty("areaCode")]
            public string AreaCode { get; set; }

            [JsonProperty("attraction")]
            public string Attraction { get; set; }

            [JsonProperty("amenitie")]
            public Amenitie Amenitie { get; set; }

            [JsonProperty("leisure")]
            public Leisure Leisure { get; set; }

            [JsonProperty("business")]
            public Business Business { get; set; }

            [JsonProperty("transportation")]
            public Transportation Transportation { get; set; }

            [JsonProperty("hotelPhone")]
            public string HotelPhone { get; set; }

            [JsonProperty("hotelCheckIn")]
            public string HotelCheckIn { get; set; }

            [JsonProperty("hotelCheckOut")]
            public string HotelCheckOut { get; set; }

            [JsonProperty("minAge")]
            public string MinAge { get; set; }

            [JsonProperty("rating")]
            public string Rating { get; set; }

            [JsonProperty("images")]
            public Images Images { get; set; }

            [JsonProperty("fireSafety")]
            public string FireSafety { get; set; }

            [JsonProperty("hotelPreference")]
            public HotelPreference HotelPreference { get; set; }

            [JsonProperty("direct")]
            public string Direct { get; set; }

            [JsonProperty("geoPoint")]
            public GeoPoint GeoPoint { get; set; }

            [JsonProperty("chain")]
            public string Chain { get; set; }

            [JsonProperty("lastUpdated")]
            public string LastUpdated { get; set; }

            [JsonProperty("priority")]
            public string Priority { get; set; }

            [JsonProperty("geoLocations")]
            public GeoLocations GeoLocations { get; set; }

            [JsonProperty("rooms")]
            public Rooms Rooms { get; set; }
        }

        public class Hotels
        {

            [JsonProperty("@count")]
            public string Count { get; set; }

            [JsonProperty("hotel")]
            [JsonConverter(typeof(JsonHotelListConverter))]
            public List<Hotel> Hotel { get; set; }
        }

        public class Result
        {

            [JsonProperty("@command")]
            public string Command { get; set; }

            [JsonProperty("@tID")]
            public string TID { get; set; }

            [JsonProperty("@ip")]
            public string Ip { get; set; }

            [JsonProperty("@date")]
            public string Date { get; set; }

            [JsonProperty("@version")]
            public string Version { get; set; }

            [JsonProperty("@elapsedTime")]
            public string ElapsedTime { get; set; }

            [JsonProperty("currencyShort")]
            public string CurrencyShort { get; set; }

            [JsonProperty("hotels")]
            public Hotels Hotels { get; set; }

            [JsonProperty("successful")]
            public string Successful { get; set; }
        }

        public class Root
        {
            [JsonProperty("?xml")]
            public Xml Xml { get; set; }

            [JsonProperty("result")]
            public Result Result { get; set; }
        }

        public class JsonHotelListConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return true;
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                JToken token = JToken.Load(reader);
                if (token.Type == JTokenType.Array)
                {
                    return token.ToObject<List<Hotel>>();
                }

                List<Hotel> hotels = new List<Hotel>
                {
                    new Hotel
                    {
                        Address = token.ToObject<Hotel>().Address,
                        Amenitie = token.ToObject<Hotel>().Amenitie,
                        AreaCode = token.ToObject<Hotel>().AreaCode,
                        AreaName = token.ToObject<Hotel>().AreaName,
                        Attraction = token.ToObject<Hotel>().Attraction,
                        BuiltYear = token.ToObject<Hotel>().BuiltYear,
                        FullAddress = token.ToObject<Hotel>().FullAddress,
                        Business = token.ToObject<Hotel>().Business,
                        Chain = token.ToObject<Hotel>().Chain,
                        CityCode = token.ToObject<Hotel>().CityCode,
                        CityNameAttr = token.ToObject<Hotel>().CityNameAttr,
                        CityName = token.ToObject<Hotel>().CityName,
                        CountryCode = token.ToObject<Hotel>().CountryCode,
                        CountryName = token.ToObject<Hotel>().CountryName,
                        Description1 = token.ToObject<Hotel>().Description1,
                        Description2 = token.ToObject<Hotel>().Description2,
                        Direct = token.ToObject<Hotel>().Direct,
                        Exclusive = token.ToObject<Hotel>().Exclusive,
                        FireSafety = token.ToObject<Hotel>().FireSafety,
                        Floors = token.ToObject<Hotel>().Floors,
                        GeoLocations = token.ToObject<Hotel>().GeoLocations,
                        GeoPoint = token.ToObject<Hotel>().GeoPoint,
                        HealthCompliant = token.ToObject<Hotel>().HealthCompliant,
                        HotelCheckIn = token.ToObject<Hotel>().HotelCheckIn,
                        HotelCheckOut = token.ToObject<Hotel>().HotelCheckOut,
                        Hotelid = token.ToObject<Hotel>().Hotelid,
                        HotelName = token.ToObject<Hotel>().HotelName,
                        HotelPhone = token.ToObject<Hotel>().HotelPhone,
                        HotelPreference = token.ToObject<Hotel>().HotelPreference,
                        Images = token.ToObject<Hotel>().Images,
                        LastUpdated = token.ToObject<Hotel>().LastUpdated,
                        Leisure = token.ToObject<Hotel>().Leisure,
                        Location = token.ToObject<Hotel>().Location,
                        Location1 = token.ToObject<Hotel>().Location1,
                        Location2 = token.ToObject<Hotel>().Location2,
                        Location3 = token.ToObject<Hotel>().Location3,
                        LocationId = token.ToObject<Hotel>().LocationId,
                        Luxury = token.ToObject<Hotel>().Luxury,
                        MinAge = token.ToObject<Hotel>().MinAge,
                        NoOfRooms = token.ToObject<Hotel>().NoOfRooms,
                        Preferred = token.ToObject<Hotel>().Preferred,
                        Priority = token.ToObject<Hotel>().Priority,
                        Rating = token.ToObject<Hotel>().Rating,
                        RegionCode = token.ToObject<Hotel>().RegionCode,
                        RegionName = token.ToObject<Hotel>().RegionName,
                        RenovationYear = token.ToObject<Hotel>().RenovationYear,
                        Rooms = token.ToObject<Hotel>().Rooms,
                        Runno = token.ToObject<Hotel>().Runno,
                        StateCode = token.ToObject<Hotel>().StateCode,
                        StateName = token.ToObject<Hotel>().StateName,
                        Transportation = token.ToObject<Hotel>().Transportation,
                        ZipCode = token.ToObject<Hotel>().ZipCode                       
                    }
                };

                return hotels;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value);
            }
        }

        public class JsonRoomTypeListConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return true;
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                JToken token = JToken.Load(reader);

                if (token.Type == JTokenType.Array)
                {

                    return token.ToObject<List<RoomType>>();
                }

                List<RoomType> roomType = new List<RoomType>
                {
                    new RoomType
                    {
                        Name = token.ToObject<RoomType>().Name,
                        RoomAmenities = token.ToObject<RoomType>().RoomAmenities,
                        RoomCapacityInfo = token.ToObject<RoomType>().RoomCapacityInfo,
                        RoomInfo = token.ToObject<RoomType>().RoomInfo,
                        Roomtypecode = token.ToObject<RoomType>().Roomtypecode,
                        Runno = token.ToObject<RoomType>().Runno,
                        Twin = token.ToObject<RoomType>().Twin,
                    }
                };

                return roomType;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value);
            }
        }

        public class JsonAmenityListConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return true;
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                JToken token = JToken.Load(reader);

                if (token.Type == JTokenType.Array)
                {

                    return token.ToObject<List<Amenity>>();
                }

                List<Amenity> amenities = new List<Amenity>
                {
                    new Amenity
                    {
                        Id = token.ToObject<Amenity>().Id,
                        Text = token.ToObject<Amenity>().Text                       
                    }
                };

                return amenities;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value);
            }
        }

        public class JsonGeoLocationListConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return true;
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                JToken token = JToken.Load(reader);

                if (token.Type == JTokenType.Array)
                {

                    return token.ToObject<List<GeoLocation>>();
                }

                List<GeoLocation> geoLocations = new List<GeoLocation>
                {
                    new GeoLocation
                    {
                        Distance = token.ToObject<GeoLocation>().Distance,
                        Id = token.ToObject<GeoLocation>().Id,
                        Name = token.ToObject<GeoLocation>().Name,
                        Runno = token.ToObject<GeoLocation>().Runno,
                        Type = token.ToObject<GeoLocation>().Type
                    }
                };

                return geoLocations;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value);
            }
        }

        public class JsonCruiseListConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return true;
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                JToken token = JToken.Load(reader);

                if (token.Type == JTokenType.Array)
                {

                    return token.ToObject<List<Cruise>>();
                }

                List<Cruise> cruises = new List<Cruise>
                {
                    new Cruise
                    {
                        Directions = token.ToObject<Cruise>().Directions,
                        Dist = token.ToObject<Cruise>().Dist,
                        Name = token.ToObject<Cruise>().Name,

                    }
                };

                return cruises;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value);
            }
        }

        public class JsonDistListConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return true;
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                JToken token = JToken.Load(reader);

                if (token.Type == JTokenType.Array)
                {

                    return token.ToObject<List<Dist>>();
                }

                List<Dist> dists = new List<Dist>
                {
                    new Dist
                    {
                       Attr = token.ToObject<Dist>().Attr,
                       Text = token.ToObject<Dist>().Text
                    }
                };

                return dists;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value);
            }
        }

        public class JsonSubwayListConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return true;
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                JToken token = JToken.Load(reader);

                if (token.Type == JTokenType.Array)
                {

                    return token.ToObject<List<Subway>>();
                }

                List<Subway> subways = new List<Subway>
                {
                    new Subway
                    {
                        Directions = token.ToObject<Subway>().Directions,
                        Dist = token.ToObject<Subway>().Dist,
                        Name = token.ToObject<Subway>().Name,
                    }
                };

                return subways;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value);
            }
        }

        public class JsonRailListConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return true;
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                JToken token = JToken.Load(reader);

                if (token.Type == JTokenType.Array)
                {

                    return token.ToObject<List<Rail>>();
                }

                List<Rail> rails = new List<Rail>
                {
                    new Rail
                    {
                        Directions = token.ToObject<Rail>().Directions,
                        Dist = token.ToObject<Rail>().Dist,
                        Name = token.ToObject<Rail>().Name
                    }
                };

                return rails;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value);
            }
        }

        public class JsonAirPortListConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return true;
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                JToken token = JToken.Load(reader);

                if (token.Type == JTokenType.Array)
                {

                    return token.ToObject<List<Airport>>();
                }

                List<Airport> airports = new List<Airport>
                {
                    new Airport
                    {
                        Directions = token.ToObject<Airport>().Directions,
                        Dist = token.ToObject<Airport>().Dist,
                        Name = token.ToObject<Airport>().Name
                    }
                };

                return airports;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value);
            }
        }

        public class JsonAmenitieItemListConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return true;
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                JToken token = JToken.Load(reader);

                if (token.Type == JTokenType.Array)
                {

                    return token.ToObject<List<AmenitieItem>>();
                }

                List<AmenitieItem> amenitieItems = new List<AmenitieItem>
                {
                    new AmenitieItem
                    {
                        Id = token.ToObject<AmenitieItem>().Id,
                        Text = token.ToObject<AmenitieItem>().Text
                    }
                };

                return amenitieItems;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value);
            }
        }

        public class JsonBusinessItemListConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return true;
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                JToken token = JToken.Load(reader);

                if (token.Type == JTokenType.Array)
                {

                    return token.ToObject<List<BusinessItem>>();
                }

                List<BusinessItem> businessItems = new List<BusinessItem>
                {
                    new BusinessItem
                    {
                        Id = token.ToObject<BusinessItem>().Id,
                        Text = token.ToObject<BusinessItem>().Text
                    }
                };

                return businessItems;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value);
            }
        }

        public class JsonLeisureItemListConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return true;
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                JToken token = JToken.Load(reader);

                if (token.Type == JTokenType.Array)
                {

                    return token.ToObject<List<LeisureItem>>();
                }

                List<LeisureItem> leisureItems = new List<LeisureItem>
                {
                    new LeisureItem
                    {
                        Id = token.ToObject<LeisureItem>().Id,
                        Text = token.ToObject<LeisureItem>().Text
                    }
                };

                return leisureItems;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value);
            }
        }

        public class JsonImageListConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return true;
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                JToken token = JToken.Load(reader);

                if (token.Type == JTokenType.Array)
                {

                    return token.ToObject<List<Image>>();
                }

                List<Image> images = new List<Image>
                {
                    new Image
                    {
                        Alt = token.ToObject<Image>().Alt,
                        Category = token.ToObject<Image>().Category,
                        Runno = token.ToObject<Image>().Runno,
                        Url = token.ToObject<Image>().Url
                    }
                };

                return images;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value);
            }
        }
    }
}