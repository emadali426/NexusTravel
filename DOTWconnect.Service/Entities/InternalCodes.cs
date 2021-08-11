using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Serialization;

namespace DOTWconnect.Service.Entities
{
    public static class InternalCodes
    {
        public static Dictionary<string, CountryInfo> getAllCountriesIDs { get; set; }
        public static Dictionary<string, Dictionary<string, string>> getAllCitiesIDs { get; set; }
        public static DOTWconnectService DOTWService { get; set; }
        static InternalCodes()
        {
            DOTWService = new DOTWconnectService();
            GetAllCountriesIDs();
        }

        public static void GetAllCitiesIDs(string CountryName)
        {
            if (getAllCitiesIDs == null)
            {
                getAllCitiesIDs = new Dictionary<string, Dictionary<string, string>>();
            }

            if (InternalCodes.getAllCitiesIDs.ContainsKey(CountryName.ToUpper()))
                return;

            //var XmlPath = HttpContext.Current.Server.MapPath(@"~\App_Data\GetAllCitiesIDsRequest.xml");

            //Create Request Object with Required Parametes;
            InternalCodeRequest.Customer getAllIDs = new InternalCodeRequest.Customer();
            getAllIDs.Request.Command = "getallcities";
            getAllIDs.Request.Return.Filters.CountryCode = getAllCountriesIDs[CountryName.ToUpper()].Code;
            getAllIDs.Request.Return.Filters.CountryName = CountryName.ToUpper();
            

           //Convert Objects to Xml;
            XmlSerializer xs = new XmlSerializer(typeof(InternalCodeRequest.Customer));
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            var XmlReq = new StringBuilder();
            XmlWriter XmlWr = XmlWriter.Create(XmlReq, new XmlWriterSettings
            {
                OmitXmlDeclaration = true,

            });
            xs.Serialize(XmlWr, getAllIDs);
            //txtWriter.Close();
           
            var responseJson = DOTWService.DOTRequestSender(XmlReq);
            GetAllCitiesIDsResponse.Root responseData = JsonConvert.DeserializeObject<GetAllCitiesIDsResponse.Root >(responseJson);
            var citiesIds = new Dictionary<string, string>();
            if (responseData.Result.Cities.Count.Equals("0")) 
            {
                citiesIds.Add($"{CountryName.ToUpper()} HAS NO CITIES", null);
                getAllCitiesIDs.Add(CountryName.ToUpper(), citiesIds);
            }
            else 
            { 
                foreach (var city in responseData.Result.Cities.City)
                {
                    citiesIds.Add(city.Name.ToUpper(), city.Code);
                }
                getAllCitiesIDs.Add(CountryName.ToUpper(), citiesIds);
            }

        }
        public static void GetAllCountriesIDs()
        {
            if (getAllCountriesIDs != null)
            {
                if (!getAllCountriesIDs.Count.Equals(0))
                    getAllCountriesIDs.Clear();

                getAllCountriesIDs = null;
            }
            getAllCountriesIDs = new Dictionary<string, CountryInfo>(); 
            //var XmlPath = HttpContext.Current.Server.MapPath(@"~\App_Data\GetAllCitiesIDsRequest.xml");

            //Create Request Object with Required Parametes;
            InternalCodeRequest.Customer getAllIDs = new InternalCodeRequest.Customer();
            getAllIDs.Request.Command = "getallcountries";
            getAllIDs.Request.Return.Fields.Field = new List<string>
            {
                "regionName",
                "regionCode"
            };

            //Convert Objects to Xml;
            XmlSerializer xs = new XmlSerializer(typeof(InternalCodeRequest.Customer));
            var ns = new XmlSerializerNamespaces();
            ns.Add("", "");
            var XmlReq = new StringBuilder();
            XmlWriter XmlWr = XmlWriter.Create(XmlReq, new XmlWriterSettings
            {
                OmitXmlDeclaration = true,
                
            });
            xs.Serialize(XmlWr, getAllIDs, ns);
            //txtWriter.Close();

            var responseJson = DOTWService.DOTRequestSender(XmlReq);
            //var path = HttpContext.Current.Server.MapPath("~/App_Data/CountryCodes.json");

            //File.WriteAllText(path, responseJson);


            GetAllCountriesIDsResponse.Root responseData = JsonConvert.DeserializeObject<GetAllCountriesIDsResponse.Root>(responseJson);
            
            if(responseData.Result != null) { 
                foreach (var country in responseData.Result.Countries.Country)
                {
                    getAllCountriesIDs.Add(country.Name.ToUpper(), new CountryInfo
                    {
                        Code = country.Code,
                        CountryVAT = 0
                    });

          
                }
            }


        }
    } 
}
