using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TravelNexus.Web.Models
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class City
    {
        [JsonProperty("@runno")]
        public int Runno { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("code")]
        public int Code { get; set; }

        [JsonProperty("countryName")]
        public string CountryName { get; set; }

        [JsonProperty("countryCode")]
        public int CountryCode { get; set; }
    }

    public class Cities
    {
        [JsonProperty("@count")]
        public int Count { get; set; }

        [JsonProperty("city")]
        public List<City> City { get; set; }
    }

    public class CitiesName
    {
        private static readonly string FilePath = HttpContext.Current.Server.MapPath(@"~\App_Data\CitiesName.json");

        static CitiesName()
        {
            var FileData = File.ReadAllText(FilePath);
            JsonConvert.DeserializeObject<CitiesName>(FileData);
        }

        [JsonProperty("cities")]
        public static Cities Cities { get; set; }
    }
}