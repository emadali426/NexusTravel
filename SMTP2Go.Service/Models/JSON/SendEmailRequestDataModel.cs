using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTP2Go.Service.Models.JSON
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class CustomHeader
    {
        [JsonProperty("header")]
        public string Header { get; set; }

        [JsonProperty("value")]
        public string Value { get; set; }
    }

    public class SendEmailRequestDataModel
    {
        [JsonProperty("api_key")]
        public string ApiKey { get; set; }

        [JsonProperty("to")]
        public List<string> To { get; set; }

        [JsonProperty("sender")]
        public string Sender { get; set; }

        [JsonProperty("subject")]
        public string Subject { get; set; }

        [JsonProperty("text_body")]
        public string TextBody { get; set; }

        [JsonProperty("html_body")]
        public string HtmlBody { get; set; }

        [JsonProperty("custom_headers")]
        public List<CustomHeader> CustomHeaders { get; set; }
    }


}
