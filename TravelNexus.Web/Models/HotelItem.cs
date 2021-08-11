using System;
using TravelNexus.Services.TboServiceReference;

namespace TravelNexus.Web.Models
{
    public class HotelItem
    {
        public Hotel_Result HotelData { get; set; }
        public string SessionId { get; set; }
        public string ResultIndex { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NoOfRequestedRoom { get; set; }
        public int NoOfPersons { get; set; }
        public string HotelImage { get; set; }
        public string ApiName { get; set; }
        public string HotelCode { get; set; }
        public bool redirectUrl { get; set; }
        
    }
}