using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTP2Go.Service.Models.JSON
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Data
    {
        [JsonProperty("succeeded")]
        public int Succeeded { get; set; }

        [JsonProperty("failed")]
        public int Failed { get; set; }

        [JsonProperty("failures")]
        public List<object> Failures { get; set; }

        [JsonProperty("email_id")]
        public string EmailId { get; set; }
    }

    public class SendEmailResponseDataModel
    {
        [JsonProperty("request_id")]
        public string RequestId { get; set; }

        [JsonProperty("data")]
        public Data Data { get; set; }
    }


}
