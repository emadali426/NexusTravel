using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTWconnect.Service.Entities
{
    namespace SearchAvailableHotelsResponse
    {

        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Xml
        {
            [JsonProperty("@version")]
            public string Version { get; set; }

            [JsonProperty("@encoding")]
            public string Encoding { get; set; }
        }
        public class RateType
        {
            [JsonProperty("@currencyId")]
            public int CurrencyId { get; set; }


            [JsonProperty("#text")]
            public string Text { get; set; }
        }
        public class RateBasi
        {
            [JsonProperty("rateType")]
            public RateType RateType { get; set; }

            [JsonProperty("total")]
            public double Total { get; set; }
        }

        public class RateBases
        {
            [JsonProperty("rateBasis")]
            [JsonConverter(typeof(JsonRateBasiListConverter))]
            public List<RateBasi> RateBasis { get; set; }
        }

        public class RoomType
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("rateBases")]
            public RateBases RateBases { get; set; }
        }

        public class Room
        {
            [JsonProperty("@adults")]
            public string Adults { get; set; }

            [JsonProperty("@children")]
            public string Children { get; set; }

            [JsonProperty("@extrabeds")]
            public string Extrabeds { get; set; }

            [JsonProperty("roomType")]
            [JsonConverter(typeof(JsonRoomTypeListConverter))]
            public List<RoomType> RoomType { get; set; }
        }

        public class Rooms
        {
            [JsonProperty("room")]
            [JsonConverter(typeof(JsonRoomListConverter))]
            public List<Room> Room { get; set; }
        }

        public class Hotel
        {
            [JsonProperty("@hotelid")]
            public string Hotelid { get; set; }

            [JsonProperty("rooms")]
            public Rooms rooms { get; set; }
        }

        public class Hotels
        {
            [JsonProperty("hotel")]
            public List<Hotel> hotel { get; set; }
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
            [JsonProperty("hotels")]
            public Hotels hotels { get; set; }
            [JsonProperty("successful")]
            public string successful { get; set; }
        }

        public class Root
        {
            [JsonProperty("?xml")]
            public Xml Xml { get; set; }
            [JsonProperty("result")]
            public Result result { get; set; }
        }

        public class JsonRateBasiListConverter : JsonConverter
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

                    return token.ToObject<List<RateBasi>>();
                }

                List<RateBasi> rate = new List<RateBasi>
                {
                    new RateBasi
                    {
                        RateType = token.ToObject<RateBasi>().RateType,
                        Total = token.ToObject<RateBasi>().Total
                    }
                };

                return rate;
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
                        RateBases = token.ToObject<RoomType>().RateBases
                    }
                };

                return roomType;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value);
            }
        }

        public class JsonRoomListConverter : JsonConverter
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

                    return token.ToObject<List<Room>>();
                }

                List<Room> rooms = new List<Room>
                {
                    new Room
                    {
                        Adults = token.ToObject<Room>().Adults,
                        RoomType = token.ToObject<Room>().RoomType,
                        Children = token.ToObject<Room>().Children,
                        Extrabeds = token.ToObject<Room>().Extrabeds
                    }
                };

                return rooms;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value);
            }
        }
    }
}