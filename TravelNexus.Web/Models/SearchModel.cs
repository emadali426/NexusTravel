using DOTWconnect.Service.Entities;
using Fawaterk.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelNexus.Web.Models
{
    public class SearchModel
    {

        public string From { get; set; }
        public string To { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public int NoOfAdults { get; set; }
        public bool NonStop { get; set; }

        public string SelectedTab { get; set; }
        //Hotel
        public string CityCode { get; set; }
        public string FlightOfferData { get; set; }
        public HotelSearchModel HotelSearchModel { get; set; }
        public string Nationality { get; set; }
        public string ApiName { get; set; }
        public List<Package> Packages { get; set; }
        public List<OurServiceModel> OurServices { get; set; }
        public List<NewsModel> NexusNews { get; set; }
        public NewsModel LastNexusNews { get; set; }
        public List<PopHotelsModel> PopHotels { get; set; }
        public List<OurCustomerFavDestModel> FavDests { get; set; }
        public CityModel City { get; set; }
        public int Runno { get; set; }

    }

    #region Hotel
    public class HotelSearchModel
    {
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public string GuestNationality { get; set; }
        public int NoOfRooms { get; set; }
        public string CityName { get; set; }
        public string PreviousCityName { get; set; }
        public string CountryName { get; set; }
        public string PreviousCountryName { get; set; }
        public RoomGuest RoomGuests { get; set; }
        public string ChildAge { get; set; }
        public Dictionary<string, CountryInfo> CountriesIds { get; set; }
        public Dictionary<string, string> CitiesIds { get; set; }

    }

    public class RoomGuest
    {
        public int AdultCount{ get; set; }
        public int ChildCount { get; set; }
    }
    #endregion
}