using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fawaterk.Service.Entities
{
    namespace CreateInvoiceRequestEntity
    {
        // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
        public class CartItem
        {
            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("price")]
            public double Price { get; set; }

            [JsonProperty("quantity")]
            public int Quantity { get; set; }
        }

        public class Customer
        {
            [JsonProperty("first_name")]
            public string FirstName { get; set; }

            [JsonProperty("last_name")]
            public string LastName { get; set; }

            [JsonProperty("email")]
            public string Email { get; set; }

            [JsonProperty("phone")]
            public string Phone { get; set; }

            [JsonProperty("address")]
            public string Address { get; set; }
        }

        public class Root
        {
            [JsonProperty("vendorKey")]
            public string VendorKey { get; set; }

            [JsonProperty("cartItems")]
            public List<CartItem> CartItems { get; set; }

            [JsonProperty("currency")]
            public string Currency { get; set; }

            [JsonProperty("cartTotal")]
            public double CartTotal { get; set; }

            [JsonProperty("customer")]
            public Customer Customer { get; set; }

            [JsonProperty("redirectUrl")]
            public string RedirectUrl { get; set; }
        }



    }
}
