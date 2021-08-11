using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelNexus.Services.TboServiceReference;

namespace TravelNexus.Web.Models
{
    public class HotelDetailsModel
    {
        public APIHotelDetails HotelDetails { get; set; }

        public HotelRoomAvailabilityResponse Rooms { get; set; }
        public string SessionId { get; set; }
        public string ResultIndex { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NoOfRequestedRoom { get; set; }
        public int NoOfPersons { get; set; }
        public string HotelImage { get; set; }
        public decimal AvgPerNight { get; set; }
        public List<string> Images { get; set; }
    }
}