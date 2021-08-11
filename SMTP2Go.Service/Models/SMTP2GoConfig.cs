using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Web;

namespace SMTP2Go.Service.Models
{
    public class SMTP2GoConfig
    {
        private static readonly string FilePath = HttpContext.Current.Server.MapPath(@"~\App_Data\AdminConfigurations\SMTP2GoConfig.json");

        static SMTP2GoConfig()
        {
            var FileData = File.ReadAllText(FilePath);
            JsonConvert.DeserializeObject<SMTP2GoConfig>(FileData);
        }
        [JsonProperty("APIKey")]
        public static string APIKey { get; set; }
        [JsonProperty("Host")]
        public static string Host { get; set; }

        [JsonProperty("Port")]
        public static int Port { get; set; }


        [JsonProperty("SenderArabicName")]
        public static string SenderArabicName { get; set; }


        [JsonProperty("SenderEnglishName")]
        public static string SenderEnglishName { get; set; }


        [JsonProperty("SenderEmail")]
        public static string SenderEmail { get; set; }


        [JsonProperty("UserName")]
        public static string UserName { get; set; }


        [JsonProperty("Password")]
        public static string Password { get; set; }


        [JsonProperty("Signature")]
        public static string Signature { get; set; }


        [JsonProperty("TimeInterval")]
        public static string TimeInterval { get; set; }


        [JsonProperty("EnableSSL")]
        public static bool EnableSSL { get; set; }
        

     

        public static void ChangeSMTPData(SMTPViewModel model)
        {
            EnableSSL = true;
            APIKey = model.APIKey;
            Host = model.Host;
            SenderArabicName = model.SenderArabicName;
            SenderEnglishName = model.SenderEnglishName;
            SenderEmail = model.SenderEmail;
            Port = model.Port;
            UserName = model.UserName;
            Signature = model.Signature;
            TimeInterval = model.TimeInterval;

            var JsonData = JsonConvert.SerializeObject(new SMTP2GoConfig());
            File.WriteAllText(FilePath, JsonData);
        }

    }

    
}