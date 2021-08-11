using Fawaterk.Service;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TravelNexus.Web.Models
{
    public class APIsKeysConfig
    {
        private static readonly string FilePath = HttpContext.Current.Server.MapPath(@"~\App_Data\AdminConfigurations\APIsKeysConfig.json");

        static APIsKeysConfig()
        {
            var FileData = File.ReadAllText(FilePath);
            JsonConvert.DeserializeObject<APIsKeysConfig>(FileData);
            FawaterkService.APIKey = FawaterkKey;
        }

        [JsonProperty("GoogleKey")]
        public static string GoogleKey { get; set; }


        [JsonProperty("FawaterkKey")]
        public static string FawaterkKey { get; set; }


        public static void UpdateAPIsFile(APIsViewModel model)
        {
            GoogleKey = model.GoogleKey;
            FawaterkKey = model.FawaterkKey;
            FawaterkService.APIKey = FawaterkKey;

            var JsonData = JsonConvert.SerializeObject(new APIsKeysConfig());
            File.WriteAllText(FilePath, JsonData);
        }
    }
}