using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TravelNexus.Web.Models
{
    public class CountryVATsConfig
    {
        private static readonly string FilePath = HttpContext.Current.Server.MapPath(@"~\App_Data\AdminConfigurations\CountryVATsConfig.json");
        private static bool FirstCreation = true;
        private static double clientTax;

        [JsonProperty("CountryVATs")]
        public static Dictionary<string, double> _countryVAT { get; set; }

        [JsonProperty("ClientTax")]
        public static double ClientTax
        {
            get { return clientTax; }
            set
            {
                clientTax = value;
                UpdateJsonFile();
            }
        }

        public double this[string key]
        {
            get { return _countryVAT[key]; }
            set
            {
                _countryVAT[key] = value;
                UpdateJsonFile();
            }
        }
        static CountryVATsConfig()
        {
            var FileData = File.ReadAllText(FilePath);
            JsonConvert.DeserializeObject<CountryVATsConfig>(FileData);
            FirstCreation = false;
        }

      

        private static void UpdateJsonFile()
        {
            if (CountryVATsConfig.FirstCreation == true)
                return;

            var JsonData = JsonConvert.SerializeObject(new CountryVATsConfig());
            File.WriteAllText(CountryVATsConfig.FilePath, JsonData);
        }

    }
}