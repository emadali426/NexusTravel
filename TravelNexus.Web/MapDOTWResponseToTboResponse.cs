using DOTWconnect.Service;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using TravelNexus.Services.TboServiceReference;
using TravelNexus.Web.Models;

namespace TravelNexus.Web
{
    public class MapDOTWResponseToTboResponse
    {
        public List<Hotel_Result> hotel_Results { get;  set; }
        public static Dictionary<string, double> averagePrice { get;  set; }
        public MapDOTWResponseToTboResponse()
        {
            hotel_Results = new List<Hotel_Result>();

            if (averagePrice == null)
            {
                averagePrice = new Dictionary<string, double>();
            }

        }
        public HotelSearchResponse MapDOTWHotelRespToTboResp(DOTWconnectHotelsViewModel DOTWHotelResp, HotelSearchResponse TboHotelResp)
        {

            hotel_Results.Clear();

            //double oneHotelAveragePrice = 0;
            //double roomTypesCount = 0;
            if(DOTWHotelResp.SearchAvailableHotels.result.hotels != null && DOTWHotelResp.SearchAvailableHotels.result.hotels.hotel != null && DOTWHotelResp.SearchHotelsList.Result.Hotels != null && DOTWHotelResp.SearchHotelsList.Result.Hotels.Hotel != null)
            {
                foreach (var hotelPrice in DOTWHotelResp.SearchAvailableHotels.result.hotels.hotel)
                {
                    //roomTypesCount = 0;
                    //oneHotelAveragePrice = 0;


                    if (hotelPrice != null && hotelPrice.rooms != null)
                    {
                        if (hotelPrice.rooms.Room.Count > 0)
                        {
                            averagePrice[hotelPrice.Hotelid] = hotelPrice.rooms.Room[0].RoomType[0].RateBases.RateBasis[0].Total;
                        }
                    }



                    //foreach (var priceOfRooms in hotelPrice.rooms.Room)
                    //{
                    //    //roomTypesCount += priceOfRooms.RoomType.Count;
                    //    foreach (var roomType in priceOfRooms.RoomType)
                    //    {
                    //        if(roomType.RateBases != null) 
                    //        { 
                    //            foreach (var priceOfRoom in roomType.RateBases.RateBasis)
                    //            {
                    //                oneHotelAveragePrice = priceOfRoom.Total;
                    //            }
                    //        }

                    //    }
                    //}

                    //averagePrice[hotelPrice.Hotelid] = oneHotelAveragePrice;

                }

                foreach (var dotwHotels in DOTWHotelResp.SearchHotelsList.Result.Hotels.Hotel)
                {
                    Hotel_Result hotel = new Hotel_Result()
                    {
                        ResultIndex = -1,
                        IsHalal = false,
                        IsPackageRate = false,
                        IsPkgProperty = false,
                        MappedHotel = true,

                        MinHotelPrice = new MinHotelPrice
                        {
                            B2CRates = false,
                            Currency = "USD",
                            TotalPrice = (decimal)averagePrice.Where(w => w.Key.Equals(dotwHotels.Hotelid)).FirstOrDefault().Value

                        },
                        HotelInfo = new HotelInfo()
                        {
                            HotelAddress = dotwHotels.FullAddress.HotelStreetAddress,
                            HotelCode = dotwHotels.Hotelid,
                            HotelContactNo = dotwHotels.HotelPhone,
                            HotelDescription = dotwHotels.Description1.Language.CdataSection,
                            HotelName = dotwHotels.HotelName,
                            HotelPicture = CheckImageAvailablity(dotwHotels.Images?.HotelImages),
                            Latitude = decimal.Parse(dotwHotels.GeoPoint.Lat),
                            Longitude = decimal.Parse(dotwHotels.GeoPoint.Lng),
                            Rating = dotwHotels.Rating == "563" ? HotelInfoHotelRating.FiveStar : dotwHotels.Rating == "562" ? HotelInfoHotelRating.FourStar : dotwHotels.Rating == "561" ? HotelInfoHotelRating.ThreeStar : dotwHotels.Rating == "560" ? HotelInfoHotelRating.TwoStar : dotwHotels.Rating == "559" ? HotelInfoHotelRating.OneStar : HotelInfoHotelRating.All,
                        },


                    };

                    hotel_Results.Add(hotel);
                }            

                hotel_Results.ForEach(item => TboHotelResp.HotelResultList.Add(item));
            }

            return TboHotelResp;
        }

        public HotelDetailsModel MapDOTWHotelDetailsToTboHotelDetails(DOTWconnectHotelsViewModel DOTWHotelResp, DOTWconnectRoomsViewModel DOTWRoomsResp , HotelDetailsModel hotelDetailsModel, string hotelId)
        {
            DOTWconnect.Service.Entities.SearchHotelsResponse.Hotel hotelDOTW = DOTWHotelResp.SearchHotelsList.Result.Hotels.Hotel.Find(f => f.Hotelid.Equals(hotelId));
            
            DOTWconnect.Service.Entities.GetRoomsResponse.Hotel hotelDetailDOTW = DOTWRoomsResp.GetRoomsDetails.result.Hotel;
            if (hotelDetailDOTW != null)
            {


                hotelDetailsModel.HotelDetails = new APIHotelDetails
                {
                    Address = hotelDOTW.FullAddress.HotelStreetAddress,
                    CityName = hotelDOTW.CityName,
                    CountryCode = hotelDOTW.CountryCode,
                    CountryName = hotelDOTW.CountryName,
                    Description = hotelDOTW.Description1.Language.CdataSection,
                    HotelCode = hotelDetailDOTW.Id,
                    HotelLocation = hotelDOTW.Location,
                    HotelName = hotelDetailDOTW.Name,
                    HotelRating = hotelDOTW.Rating == "563" ? HotelInfoHotelRating.FiveStar : hotelDOTW.Rating == "562" ? HotelInfoHotelRating.FourStar : hotelDOTW.Rating == "561" ? HotelInfoHotelRating.ThreeStar : hotelDOTW.Rating == "560" ? HotelInfoHotelRating.TwoStar : hotelDOTW.Rating == "559" ? HotelInfoHotelRating.OneStar : HotelInfoHotelRating.All,
                    PhoneNumber = hotelDOTW.HotelPhone,
                    Image = CheckImageAvailablity(hotelDOTW.Images?.HotelImages),
                    ImageUrls = new ImageUrlDetails[]
                    {

                    },
                    HotelFacilities = new string[]
                    {
                    hotelDOTW.FireSafety,
                    hotelDOTW.Floors,
                    hotelDOTW.NoOfRooms
                    },

                };


                hotelDetailsModel.Rooms = new HotelRoomAvailabilityResponse();

                hotelDetailsModel.Rooms.HotelRooms = new Hotel_Room[hotelDetailDOTW.Rooms.Room[0].RoomType.Count];


                for (int y = 0; y < int.Parse(hotelDetailDOTW.Rooms.Count); y++)
                {
                    for (int i = 0; i < int.Parse(hotelDetailDOTW.Rooms.Room[y].Count); i++)
                    {
                        Hotel_Room oneRoom = new Hotel_Room();
                        oneRoom.RoomAdditionalInfo = new RoomInformation();
                        //hotelDOTW.Rooms.Room.RoomType.Find(f => f.Roomtypecode.Equals(hotelDetailDOTW.Rooms.Room[y].RoomType[i].Roomtypecode)).RoomAmenities.Amenity.ForEach(e => oneRoom.Amenities += (e.Text + "|"));
                        oneRoom.RoomIndex = int.Parse(hotelDetailDOTW.Rooms.Room[y].RoomType[i].Runno);
                        oneRoom.RoomTypeCode = hotelDetailDOTW.Rooms.Room[y].RoomType[i].Roomtypecode;
                        oneRoom.RoomTypeName = hotelDetailDOTW.Rooms.Room[y].RoomType[i].Name;
                        oneRoom.RoomAdditionalInfo.MaxAdult = hotelDetailDOTW.Rooms.Room[y].RoomType[i].RoomInfo.MaxAdult;
                        oneRoom.RoomAdditionalInfo.MaxChild = hotelDetailDOTW.Rooms.Room[y].RoomType[i].RoomInfo.MaxChildren;
                        oneRoom.RoomAdditionalInfo.MaxOccupancy = hotelDetailDOTW.Rooms.Room[y].RoomType[i].RoomInfo.MaxOccupancy;
                        oneRoom.RoomAdditionalInfo.MaxAge = hotelDetailDOTW.Rooms.Room[y].RoomType[i].RoomInfo.MaxChildAge;
                        oneRoom.RoomAdditionalInfo.MinAge = hotelDetailDOTW.Rooms.Room[y].RoomType[i].RoomInfo.MaxChildAge;


                        hotelDetailsModel.Rooms.HotelRooms[i] = oneRoom;
                        hotelDetailsModel.Rooms.HotelRooms[i].RoomRate = new RoomRate
                        {
                            PrefPrice = hotelDetailDOTW.Rooms.Room[y].RoomType[i].RateBases.RateBasis[0].Total.Text
                        };
                    }

                }


                hotelDetailsModel.CheckInDate = DateTime.Parse(hotelDOTW.HotelCheckIn);
                hotelDetailsModel.CheckOutDate = DateTime.Parse(hotelDOTW.HotelCheckOut);
                hotelDetailsModel.HotelImage = CheckImageAvailablity(hotelDOTW.Images?.HotelImages);
                hotelDetailsModel.SessionId = DOTWRoomsResp.GetRoomsDetails.result.TID;
                hotelDetailsModel.AvgPerNight = (decimal)averagePrice[hotelId];

                hotelDetailsModel.Images = new List<string>();

                hotelDetailsModel.Images.Add(hotelDOTW.Images?.HotelImages?.Thumb?.CdataSection ?? "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRP542P7S8Dl2OZgc8OEQKzC_cYkr9g0a4hzg&usqp=CAU");


                foreach (var img in hotelDOTW.Images?.HotelImages?.Image)
                {
                    hotelDetailsModel.Images.Add(img?.Url?.CdataSection ?? "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRP542P7S8Dl2OZgc8OEQKzC_cYkr9g0a4hzg&usqp=CAU");
                }

                return hotelDetailsModel;
            }
            else
            {
                return null;
            }
        }

        
        private string CheckImageAvailablity(DOTWconnect.Service.Entities.SearchHotelsResponse.HotelImages hotelImages)
        {
            
            HttpWebRequest requestT = (HttpWebRequest)HttpWebRequest.Create(hotelImages?.Thumb?.CdataSection ?? "https://imagenotfound.com/image.jpg");
            requestT.Method = "HEAD";

            bool existsT;
            try
            {
                requestT.GetResponse();
                existsT = true;
            }
            catch
            {
                existsT = false;
            }

            if (existsT)
                return hotelImages?.Thumb?.CdataSection;

            foreach (var image in hotelImages?.Image ?? new List<DOTWconnect.Service.Entities.SearchHotelsResponse.Image>())
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(image?.Url?.CdataSection ?? "https://imagenotfound.com/image.jpg");
                request.Method = "HEAD";
                
                bool exists;
                try
                {
                    request.GetResponse();
                    exists = true;
                }
                catch
                {
                    exists = false;
                }

                if (exists)
                    return image?.Url?.CdataSection;
            }
            
            return "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRP542P7S8Dl2OZgc8OEQKzC_cYkr9g0a4hzg&usqp=CAU";
        }
    }
}