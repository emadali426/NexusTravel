using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Routing;

namespace TravelNexus.Web.Models
{
    public class WebbedsHotelBookingModel
    {
        public DOTWconnect.Service.Entities.GetRoomsResponse.RoomType RoomDetailsGetRooms { get; set; }
        public DOTWconnect.Service.Entities.SearchHotelsResponse.RoomType RoomDetailsSearchHotels { get; set; }
        public DOTWconnect.Service.Entities.GetRoomsResponse.Hotel HotelDetailsGetRooms { get; set; }
        public DOTWconnect.Service.Entities.SearchHotelsResponse.Hotel HotelDetailsSearchHotels { get; set; }
        public int modalCount { get; set; } = 0;
        public string CityId { get; set; }
        public string HotelId { get; set; }
        public string HotelName { get; set; }
        public string RoomTypeCode { get; set; }
        public string ConfNum { get; set; }
        public string APIName { get; set; }
        public string AllocationDetails { get; set; }
        public double Price { get; set; }


        public double AddTaxes(double price)
        {

            return price;
        }


    }
}