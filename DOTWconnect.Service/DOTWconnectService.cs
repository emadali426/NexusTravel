using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Net.Http;
using System.IO;
using System.Web;
using System.Xml;
using Newtonsoft.Json;
using System.Xml.Serialization;

namespace DOTWconnect.Service
{
    public class DOTWconnectHotelsViewModel
    {
        public Entities.SearchHotelsResponse.Root SearchHotelsList { get; set; }
        public Entities.SearchHotelsResponse.Hotel HotelDetails { get; set; }
        public Entities.SearchAvailableHotelsResponse.Root SearchAvailableHotels { get; set; }
        //public Entities.GetRoomsResponse.Root GetRoomsDetails { get; set; }
        public DOTWconnectHotelsViewModel()
        {
            SearchHotelsList = new Entities.SearchHotelsResponse.Root();
            SearchAvailableHotels = new Entities.SearchAvailableHotelsResponse.Root();
            HotelDetails = new Entities.SearchHotelsResponse.Hotel();
        }
        

    }

    public class DOTWconnectRoomsViewModel
    {
        public Entities.GetRoomsResponse.Root GetRoomsDetails { get; set; }

        public DOTWconnectRoomsViewModel()
        {
            GetRoomsDetails = new Entities.GetRoomsResponse.Root();
        }

    }

    public partial class DOTWconnectService
    {
        private static readonly HttpClient client;
        public static string URL { get;   private set; }
        public static string Password { get;  private set; }
        public static string LoginID { get;  private set; }
        public static string CompanyCode { get;  private set; }
        public static List<string> FieldValues { get;  private set; }
        public static List<string> RoomFieldValues { get; private set; }
        public List<string> FieldValue { get; private set; }
        public static long fromDate { get; private set; }
        public static long toDate { get; private set; }
        private DOTWconnectHotelsViewModel dotWconnectHotelsViewModel;
        private DOTWconnectRoomsViewModel dotWconnectRoomsViewModel;
        //public customer SearchHotelsReq { get; set; }
        public HttpResponseMessage httpResponseMessage { get; private set; }


        static DOTWconnectService()
        {
            client = new HttpClient();
            URL = ConfigurationManager.AppSettings["DOTWconnectURL"];
            LoginID = ConfigurationManager.AppSettings["DOTWconnectID"];
            Password = ConfigurationManager.AppSettings["DOTWconnectPassword"];
            CompanyCode = ConfigurationManager.AppSettings["DOTWconnectCompanyCode"];

            

            if (FieldValues != null)
            {
                if (FieldValues.Count > 0)
                    FieldValues.Clear();
                FieldValues = null;
            }


            //if (FieldValue != null)
            //{
            //    if (FieldValue.Count > 0)
            //        FieldValue.Clear();
            //    FieldValue = null;
            //}

            if (RoomFieldValues != null)
            {
                if (RoomFieldValues.Count > 0)
                    RoomFieldValues.Clear();
                RoomFieldValues = null;
            }
            FieldValues = new List<string>();
            
            RoomFieldValues = new List<string>();

            FieldValues.Add("exclusive");
            FieldValues.Add("healthCompliant");
            FieldValues.Add("builtYear");
            FieldValues.Add("renovationYear");
            FieldValues.Add("floors");
            FieldValues.Add("noOfRooms");
            FieldValues.Add("preferred");
            FieldValues.Add("luxury");
            FieldValues.Add("fullAddress");
            FieldValues.Add("description1");
            FieldValues.Add("description2");
            FieldValues.Add("hotelName");
            FieldValues.Add("address");
            FieldValues.Add("zipCode");
            FieldValues.Add("location");
            FieldValues.Add("locationId");
            FieldValues.Add("location1");
            FieldValues.Add("location2");
            FieldValues.Add("location3");
            FieldValues.Add("cityName");
            FieldValues.Add("cityCode");
            FieldValues.Add("stateName");
            FieldValues.Add("stateCode");
            FieldValues.Add("countryName");
            FieldValues.Add("countryCode");
            FieldValues.Add("regionName");
            FieldValues.Add("regionCode");
            FieldValues.Add("areaName");
            FieldValues.Add("areaCode");
            FieldValues.Add("attraction");
            FieldValues.Add("amenitie");
            FieldValues.Add("leisure");
            FieldValues.Add("transportation");
            FieldValues.Add("hotelPhone");
            FieldValues.Add("hotelCheckIn");
            FieldValues.Add("hotelCheckOut");
            FieldValues.Add("minAge");
            FieldValues.Add("rating");
            FieldValues.Add("images");
            FieldValues.Add("fireSafety");
            FieldValues.Add("hotelPreference");
            FieldValues.Add("direct");
            FieldValues.Add("geoPoint");
            FieldValues.Add("leftToSell");
            FieldValues.Add("chain");
            FieldValues.Add("lastUpdated");
            FieldValues.Add("priority");
            FieldValues.Add("geoLocations");
            RoomFieldValues.Add("name");
            RoomFieldValues.Add("roomInfo");
            RoomFieldValues.Add("roomAmenities");
            RoomFieldValues.Add("twin");

            //SearchHotelsReq = new customer();

            //SearchHotelsReq.password = Password;
            //SearchHotelsReq.id = CompanyCode;
            //SearchHotelsReq.username = LoginID;
            //SearchHotelsReq.source = sourceType.Item1;
            //SearchHotelsReq.request = new requestType();
            //SearchHotelsReq.request.bookingDetails = new bookingDetailsType();
            //SearchHotelsReq.request.bookingDetails.rooms = new roomsType();
            //SearchHotelsReq.request.@return = new returnType();
            //SearchHotelsReq.request.@return.filters = new filtersType();

        }
        public DOTWconnectService()
        {
            dotWconnectHotelsViewModel = new DOTWconnectHotelsViewModel();
            dotWconnectRoomsViewModel = new DOTWconnectRoomsViewModel();
            FieldValue = new List<string>();
        }
        
        public string DOTRequestSender(StringBuilder XmlReq)
        {

            var httpContent = new StringContent(XmlReq.ToString(), Encoding.UTF8, "text/xml");
            httpResponseMessage = client.PostAsync(URL, httpContent).Result;

            var responseXml = httpResponseMessage.Content.ReadAsStringAsync().Result;

            XmlDocument doc = new XmlDocument();

            doc.LoadXml(responseXml);

            string responseJson = JsonConvert.SerializeObject(doc);

            return responseJson;
        }

        public void test()
        {
            var obj = new Entities.SearchHotelsRequest.Customer
            {
                Username = LoginID,
                Password = Password,
                Id = CompanyCode,
                Source = 1,
                Product = "hotel",
                Request = new Entities.SearchHotelsRequest.Request
                {
                    Command = "searchhotels",
                    BookingDetails = new Entities.SearchHotelsRequest.BookingDetails
                    {
                        FromDate = new DateTime(2021, 04, 28).ToString("yyyy-MM-dd"),
                        ToDate = new DateTime(2021, 04, 30).ToString("yyyy-MM-dd"),
                        Currency = 520,
                        Rooms = new Entities.SearchHotelsRequest.Rooms
                        {
                            No = 0,
                            Room = new List<Entities.SearchHotelsRequest.Room>
                            {
                                new Entities.SearchHotelsRequest.Room
                                {
                                    Runno = 0,
                                    AdultsCode = 1,
                                    Children = new Entities.SearchHotelsRequest.Children
                                    {
                                        No = 2,
                                        Child = new List<Entities.SearchHotelsRequest.Child>
                                        {
                                            new Entities.SearchHotelsRequest.Child
                                            {
                                                Runno = 0,
                                                ChildAge = 8
                                            },
                                            new Entities.SearchHotelsRequest.Child
                                            {
                                                Runno = 1,
                                                ChildAge = 9
                                            }
                                        }
                                    },
                                    RateBasis = 1,
                                    PassengerNationality = 7,
                                    PassengerCountryOfResidence = 7
                                }
                            }
                        }

                    },
                    Return = new Entities.SearchHotelsRequest.Return
                    {
                        Filters = new Entities.SearchHotelsRequest.Filters
                        {
                            City = 444,
                            NearbyCities = true,
                            NoPrice = true,
                            Condition = new Entities.SearchHotelsRequest.Condition
                            {

                                
                                
                                condition = new Entities.SearchHotelsRequest.Condition
                                {
                                    FieldName = "hotelId",
                                    FieldTest = "equals",
                                    FieldValues = new Entities.SearchHotelsRequest.FieldValues
                                    {
                                        FieldValue = new List<string>
                                            {
                                                "1098320"
                                            }
                                    },
                                    Operator = "AND",
                                    
                                       
                                },
                                 

                            }

                        },
                        Fields = new Entities.SearchHotelsRequest.Fields
                        {
                            Field = new List<string>
                            {
                                "hotelName",
                                "noOfRooms",
                                "amenitie"
                            },
                            RoomField = new List<string>
                            {
                                "roomInfo"
                            }
                        }
                    }
                    
                }
            
            };
            var str = new Entities.XMLMap<Entities.SearchHotelsRequest.Customer>().ObjectToXML(obj);
        }

    }
}
