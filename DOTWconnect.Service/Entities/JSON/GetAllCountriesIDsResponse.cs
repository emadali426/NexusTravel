using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTWconnect.Service.Entities
{
    namespace GetAllCountriesIDsResponse
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

        public class Country
        {
            [JsonConstructor]
            public Country(
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
            public readonly string Runno;

            [JsonProperty("name")]
            public readonly string Name;

            [JsonProperty("code")]
            public readonly string Code;
        }

        public class Countries
        {
            [JsonConstructor]
            public Countries(
                [JsonProperty("@count")] string count,
                [JsonProperty("country")] List<Country> country
            )
            {
                this.Count = count;
                this.Country = country;
            }

            [JsonProperty("@count")]
            public readonly string Count;

            [JsonProperty("country")]
            public readonly List<Country> Country;
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
                [JsonProperty("countries")] Countries countries,
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
                this.Countries = countries;
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

            [JsonProperty("countries")]
            public readonly Countries Countries;

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


    }
}
