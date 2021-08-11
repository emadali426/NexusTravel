using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelNexus.Services.TboServiceReference;

namespace TravelNexus.Web.Models
{
    public class HotelRoomModel
    {
        public Hotel_Room AvailableHotelRoom { get; set; }
        public string ResultIndex { get; set; }
        public string SessionId { get; set; }
        public string HotelCode { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NoOfRequestedRoom { get; set; }
        public int NoOfPersons { get; set; }
        public string HotelImage { get; set; }
        public int RoomIndex { get; set; }
        public string CountryName { get; set; }
        public string CityName { get; set; }

    }
}