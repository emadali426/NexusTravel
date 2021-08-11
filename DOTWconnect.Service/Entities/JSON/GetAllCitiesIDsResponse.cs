using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTWconnect.Service.Entities
{
   namespace GetAllCitiesIDsResponse
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Xml
        {
            [JsonConstructor]
            public Xml(
                [JsonProperty("@version")] string version,
                [JsonProperty("@encoding")] string encoding
            )
            {
                this.Version = version;
                this.Encoding = encoding;
            }

            [JsonProperty("@version")]
            public readonly string Version;

            [JsonProperty("@encoding")]
            public readonly string Encoding;
        }

        public class City
        {
            [JsonConstructor]
            public City(
                [JsonProperty("@runno")] string runno,
                [JsonProperty("name")] string name,
                [JsonProperty("code")] string code
            )
            {
                this.Runno = runno;
                this.Name = name;
                this.Code = code;
            }

            [JsonProperty("@runno")]
            public string Runno;

            [JsonProperty("name")]
            public string Name;

            [JsonProperty("code")]
            public string Code;
        }

        public class Cities
        {
            [JsonConstructor]
            public Cities(
                [JsonProperty("@count")] string count,
                [JsonProperty("city")] List<City> city
            )
            {
                this.Count = count;
                this.City = city;
            }

            [JsonProperty("@count")]
            public readonly string Count;

            [JsonProperty("city")]
            [JsonConverter(typeof(JsonCityListConverter))]
            public readonly List<City> City;
        }

        public class Result
        {
            [JsonConstructor]
            public Result(
                [JsonProperty("@command")] string command,
                [JsonProperty("@tID")] string tID,
                [JsonProperty("@ip")] string ip,
                [JsonProperty("@sip")] string sip,
                [JsonProperty("@xmlns:a")] string xmlnsA,
                [JsonProperty("@xmlns:c")] string xmlnsC,
                [JsonProperty("@date")] string date,
                [JsonProperty("@version")] string version,
                [JsonProperty("@elapsedTime")] string elapsedTime,
                [JsonProperty("cities")] Cities cities,
                [JsonProperty("successful")] string successful
            )
            {
                this.Command = command;
                this.TID = tID;
                this.Ip = ip;
                this.Sip = sip;
                this.XmlnsA = xmlnsA;
                this.XmlnsC = xmlnsC;
                this.Date = date;
                this.Version = version;
                this.ElapsedTime = elapsedTime;
                this.Cities = cities;
                this.Successful = successful;
            }

            [JsonProperty("@command")]
            public readonly string Command;

            [JsonProperty("@tID")]
            public readonly string TID;

            [JsonProperty("@ip")]
            public readonly string Ip;

            [JsonProperty("@sip")]
            public readonly string Sip;

            [JsonProperty("@xmlns:a")]
            public readonly string XmlnsA;

            [JsonProperty("@xmlns:c")]
            public readonly string XmlnsC;

            [JsonProperty("@date")]
            public readonly string Date;

            [JsonProperty("@version")]
            public readonly string Version;

            [JsonProperty("@elapsedTime")]
            public readonly string ElapsedTime;

            [JsonProperty("cities")]
            public readonly Cities Cities;

            [JsonProperty("successful")]
            public readonly string Successful;
        }

        public class Root
        {
            [JsonConstructor]
            public Root(
                [JsonProperty("?xml")] Xml xml,
                [JsonProperty("result")] Result result
            )
            {
                this.Xml = xml;
                this.Result = result;
            }

            [JsonProperty("?xml")]
            public readonly Xml Xml;

            [JsonProperty("result")]
            public readonly Result Result;
        }


        public class JsonCityListConverter : JsonConverter
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

                    return token.ToObject<List<City>>();
                }

                List<City> cities = new List<City>
                {
                    new City(token.ToObject<City>().Runno,
                        token.ToObject<City>().Name,
                        token.ToObject<City>().Code)
                };

                return cities;
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                serializer.Serialize(writer, value);
            }
        }


    }
}
