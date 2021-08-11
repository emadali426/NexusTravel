using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace TravelNexus.Web.Models
{
    public class FrontEndManage
    {
        private static readonly string FilePath = HttpContext.Current.Server.MapPath(@"~\App_Data\AdminConfigurations\FrontEndManagment.json");

        static FrontEndManage()
        {
            var FileData = File.ReadAllText(FilePath);
            JsonConvert.DeserializeObject<FrontEndManage>(FileData);
        }

        [JsonProperty("OurService")]
        public static bool OurService { get; set; }

        [JsonProperty("OurCustomersFavDest")]
        public static bool OurCustomersFavDest { get; set; }

        [JsonProperty("PopularHotels")]
        public static bool PopularHotels { get; set; }

        public static void UpdateFrontEndFile(FrontEndManageViewModel model)
        {
            OurService = model.OurService;
            OurCustomersFavDest = model.OurCustomersFavDest;
            PopularHotels = model.PopularHotels;

            var JsonData = JsonConvert.SerializeObject(new FrontEndManage());
            File.WriteAllText(FilePath, JsonData);
        }
    }
}