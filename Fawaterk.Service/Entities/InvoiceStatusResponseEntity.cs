using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fawaterk.Service.Entities
{
    namespace InvoiceStatusResponseEntity
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class Root
        {
            [JsonProperty("invoice_id")]
            public int InvoiceId { get; set; }

            [JsonProperty("payment_status")]
            public PaymentStatus PaymentStatus { get; set; }

            [JsonProperty("payment_method")]
            public string PaymentMethod { get; set; }

            [JsonProperty("total")]
            public double Total { get; set; }
        }


    }
}
