using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fawaterk.Service.Entities
{
    namespace InvoiceResponseEntity
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Root
        {
            [JsonProperty("url")]
            public string Url { get; set; }

            [JsonProperty("invoiceKey")]
            public string InvoiceKey { get; set; }

            [JsonProperty("invoiceId")]
            public long InvoiceId { get; set; }

            [JsonProperty("0050, 0051, 0052, 0053, 0054, 0055")]
            public string ErrorMessage { get; set; }
        }

        
    }
}
