using DOTWconnect.Service.Entities;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Xml;
using System.Xml.Serialization;



namespace DOTWconnect.Service
{
    public partial class DOTWconnectService
    {
        public DOTWconnectHotelsViewModel SearchHotels(DateTime checkIn, DateTime checkOut, int CountryCode, int CityCode, int AdultsNumber = 1, int ChildrenNumber = 0, int ChildrenAges = 0,int NoOfRooms = 0)
        {

            try
            {

                //var XmlPath = HttpContext.Current.Server.MapPath(@"~\App_Data\SearchHotelRequest.xml");

                //InternalCodes.GetAllCitiesIDs(CountryName);

                //var room = new List<Entities.SearchAvailableHotels.Room>();
                //for (int i = 0; i < NoOfRooms; i++)
                //{
                //    room.Add(new Entities.SearchAvailableHotels.Room
                //    {
                //        Runno = i,
                //        AdultsCode = AdultsNumber,
                //        Children = new Entities.SearchAvailableHotels.Children
                //        {
                //            No = ChildrenNumber,

                //        },
                //        RateBasis = -1
                //    });
                //}

                Entities.SearchAvailableHotels.Customer searchAvailableHotels = new Entities.SearchAvailableHotels.Customer
                {
                    Username = DOTWconnectService.LoginID,
                    Password = DOTWconnectService.Password,
                    Id = int.Parse(DOTWconnectService.CompanyCode),
                    Source = 1,
                    Product = "hotel",
                    Request = new Entities.SearchAvailableHotels.Request
                    {
                        Command = "searchhotels",
                        BookingDetails = new Entities.SearchAvailableHotels.BookingDetails
                        {
                            FromDate = checkIn.ToString("yyy-MM-dd"),
                            ToDate = checkOut.ToString("yyyy-MM-dd"),
                            //FromDate = fromDate,
                            //ToDate = toDate,
                            Currency = 520,
                            Rooms = new Entities.SearchAvailableHotels.Rooms
                            {
                                //No = NoOfRooms,
                                //Room = room
                                No = 1,
                                Room = new List<Entities.SearchAvailableHotels.Room>
                                {
                                    new Entities.SearchAvailableHotels.Room
                                    {
                                        Runno = 0,
                                        AdultsCode = AdultsNumber,
                                        Children = new Entities.SearchAvailableHotels.Children
                                        {
                                            No = ChildrenNumber,

                                        },
                                        RateBasis = -1

                                    }

                                }
                            }

                        },
                        Return = new Entities.SearchAvailableHotels.Return
                        {
                            
                            Filters = new Entities.SearchAvailableHotels.Filters
                            {
                                City = CityCode
                                
                            }

                        }
                    }
                };

                XmlSerializer xs = new XmlSerializer(typeof(Entities.SearchAvailableHotels.Customer));
                var ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                var XmlReq = new StringBuilder();
                XmlWriter XmlWr = XmlWriter.Create(XmlReq, new XmlWriterSettings
                {
                    OmitXmlDeclaration = true,

                });
                xs.Serialize(XmlWr, searchAvailableHotels);
                //txtWriter.Close();

                string searchAvailableHotelsResponseJson = DOTRequestSender(XmlReq);

                dotWconnectHotelsViewModel.SearchAvailableHotels = JsonConvert.DeserializeObject<Entities.SearchAvailableHotelsResponse.Root>(searchAvailableHotelsResponseJson);

                if(dotWconnectHotelsViewModel.SearchAvailableHotels.result.hotels != null && dotWconnectHotelsViewModel.SearchAvailableHotels.result.hotels.hotel != null) 
                { 
                    foreach (var item in dotWconnectHotelsViewModel.SearchAvailableHotels.result.hotels.hotel)
                    {
                        FieldValue.Add(item.Hotelid);
                    }
                }


                //var HotelRoom = new List<Entities.SearchHotelsRequest.Room>();
                //for (int i = 0; i < NoOfRooms; i++)
                //{
                //    HotelRoom.Add(new Entities.SearchHotelsRequest.Room
                //    {
                //        Runno = i,
                //        AdultsCode = AdultsNumber,
                //        Children = new Entities.SearchHotelsRequest.Children
                //        {
                //            No = ChildrenNumber,

                //        },
                //        RateBasis = -1
                //    });
                //}



                Entities.SearchHotelsRequest.Customer searchData = new Entities.SearchHotelsRequest.Customer
                {
                    Username = DOTWconnectService.LoginID,
                    Password = DOTWconnectService.Password,
                    Id = DOTWconnectService.CompanyCode,
                    Source = 1,
                    Product = "hotel",
                    Language = "en",
                    Request = new Entities.SearchHotelsRequest.Request
                    {
                        Command = "searchhotels",
                        BookingDetails = new Entities.SearchHotelsRequest.BookingDetails
                        {
                            FromDate = checkIn.ToString("yyyy-MM-dd"),
                            ToDate = checkOut.ToString("yyyy-MM-dd"),
                            Currency = 520,
                            Rooms = new Entities.SearchHotelsRequest.Rooms
                            {
                                //No = NoOfRooms,
                                //Room = HotelRoom
                                No = 1,
                                Room = new List<Entities.SearchHotelsRequest.Room>
                                {
                                    new Entities.SearchHotelsRequest.Room
                                    {
                                        Runno = 0,
                                        AdultsCode = AdultsNumber,
                                        Children = new Entities.SearchHotelsRequest.Children
                                        {
                                            No = ChildrenNumber,

                                        },
                                        RateBasis = -1

                                    }

                                }
                            }

                        },
                        Return = new Entities.SearchHotelsRequest.Return
                        {
                            //GetRooms = true.ToString().ToLower(),

                            Filters = new Entities.SearchHotelsRequest.Filters
                            {
                                City = CityCode,
                                NoPrice = true,
                                Condition = new Entities.SearchHotelsRequest.Condition
                                {
                                    condition = new Entities.SearchHotelsRequest.Condition
                                    {
                                        FieldName = "hotelId",
                                        FieldTest = "equals",
                                        FieldValues = new Entities.SearchHotelsRequest.FieldValues
                                        {
                                            FieldValue = FieldValue.Count > 0 ? FieldValue : new List<string>
                                            {
                                                ""
                                            }
                                        }

                                    }
                                }
                            },
                            Fields = new Entities.SearchHotelsRequest.Fields
                            {
                                Field = FieldValues,
                                RoomField = RoomFieldValues
                            }

                        }
                    }
                };

                XmlSerializer xs2 = new XmlSerializer(typeof(Entities.SearchHotelsRequest.Customer));
                var ns2 = new XmlSerializerNamespaces();
                ns.Add("", "");
                var XmlReq2 = new StringBuilder();
                XmlWriter XmlWr2 = XmlWriter.Create(XmlReq2, new XmlWriterSettings
                {
                    OmitXmlDeclaration = true,

                });
                xs2.Serialize(XmlWr2, searchData);
                //txtWriter2.Close();

                string searchHotelsResponseJson = DOTRequestSender(XmlReq2);


                dotWconnectHotelsViewModel.SearchHotelsList = JsonConvert.DeserializeObject<Entities.SearchHotelsResponse.Root>(searchHotelsResponseJson);

                FieldValue.Clear();
                return dotWconnectHotelsViewModel;

            }
            catch
            {
                throw;
            }
           
        }


        public DOTWconnectRoomsViewModel GetRooms(string hotelId, DateTime checkIn, DateTime checkOut)
        {
            try
            {

                //var XmlPath = HttpContext.Current.Server.MapPath(@"~\App_Data\SearchHotelRequest.xml");


                Entities.GetRoomsRequest.Customer getRooms = new Entities.GetRoomsRequest.Customer
                {
                    Username = DOTWconnectService.LoginID,
                    Password = DOTWconnectService.Password,
                    Id = int.Parse(DOTWconnectService.CompanyCode),
                    Source = 1,
                    Product = "hotel",
                    Request = new Entities.GetRoomsRequest.Request
                    {
                        Command = "getrooms",
                        BookingDetails = new Entities.GetRoomsRequest.BookingDetails
                        {
                            FromDate = checkIn.ToString("yyyy-MM-dd"),
                            ToDate = checkOut.ToString("yyyy-MM-dd"),
                            Currency = 520,
                            Rooms = new Entities.GetRoomsRequest.Rooms
                            {
                                No = 1,
                                Room = new List<Entities.GetRoomsRequest.Room>
                            {
                                new Entities.GetRoomsRequest.Room
                                {
                                    Runno = 0,
                                    AdultsCode = 1,
                                    Children = new Entities.GetRoomsRequest.Children
                                    {
                                        No = 0,

                                    },
                                    RateBasis = -1

                                }

                            }
                            },
                            ProductId = hotelId
                        }
                    }
                };

                XmlSerializer xs = new XmlSerializer(typeof(Entities.GetRoomsRequest.Customer));
                var ns = new XmlSerializerNamespaces();
                ns.Add("", "");
                var XmlReq = new StringBuilder();
                XmlWriter XmlWr = XmlWriter.Create(XmlReq, new XmlWriterSettings
                {
                    OmitXmlDeclaration = true,

                });
                xs.Serialize(XmlWr, getRooms);
                
                string getRoomsResponse = DOTRequestSender(XmlReq);

                dotWconnectRoomsViewModel.GetRoomsDetails = JsonConvert.DeserializeObject<Entities.GetRoomsResponse.Root>(getRoomsResponse);

                return dotWconnectRoomsViewModel;
            }
            catch
            {

                throw;
            }
        }
    }
}
