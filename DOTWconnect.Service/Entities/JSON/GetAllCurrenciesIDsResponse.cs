using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DOTWconnect.Service.Entities
{
    namespace GetAllCurrenciesIDsResponse
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

        public class Option
        {
            [JsonConstructor]
            public Option(
                [JsonProperty("@runno")] string runno,
                [JsonProperty("@shortcut")] string shortcut,
                [JsonProperty("@value")] string value,
                [JsonProperty("#text")] string text
            )
            {
                this.Runno = runno;
                this.Shortcut = shortcut;
                this.Value = value;
                this.Text = text;
            }

            [JsonProperty("@runno")]
            public readonly string Runno;

            [JsonProperty("@shortcut")]
            public readonly string Shortcut;

            [JsonProperty("@value")]
            public readonly string Value;

            [JsonProperty("#text")]
            public readonly string Text;
        }

        public class Currency
        {
            [JsonConstructor]
            public Currency(
                [JsonProperty("@count")] string count,
                [JsonProperty("option")] List<Option> option
            )
            {
                this.Count = count;
                this.Option = option;
            }

            [JsonProperty("@count")]
            public readonly string Count;

            [JsonProperty("option")]
            public readonly List<Option> Option;
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
                [JsonProperty("currency")] Currency currency,
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
                this.Currency = currency;
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

            [JsonProperty("currency")]
            public readonly Currency Currency;

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
