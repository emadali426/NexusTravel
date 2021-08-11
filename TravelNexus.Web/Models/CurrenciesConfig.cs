
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TravelNexus.Web.Models
{
    public class CurrenciesConfig
    {
        private static readonly string FilePath = HttpContext.Current.Server.MapPath(@"~\App_Data\AdminConfigurations\CurrenciesConfig.json");

        static CurrenciesConfig()
        {
            var FileData = File.ReadAllText(FilePath);
            JsonConvert.DeserializeObject<CurrenciesConfig>(FileData);
        }

        [JsonProperty("Currencies")]
        public static Dictionary<string, double> _currencies { get; set; }

        public double this[string key] 
        { 
            get
            {
                return _currencies[key];
            }
            set 
            {
                _currencies[key] = value;
                UpdateCurrencies(this);
            } 
        }

        private static void UpdateCurrencies(CurrenciesConfig curr)
        {
            var JsonData = JsonConvert.SerializeObject(curr);
            File.WriteAllText(FilePath, JsonData);
        }
    }

}