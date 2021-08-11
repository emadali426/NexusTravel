using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelNexus.Services
{
    public class Hotel
    {
        public Hotel()
        {
            MediaUrls = new List<string>();
            Amenities = new List<string>();
            Offers = new List<Offer>();
            AddressLines = new List<string>();
        }
        public string HotelId { get; set; }
        public string Name { get; set; }
        public int Rating { get; set; }
        public string City { get; set; }
        public string CountryCode { get; set; }
        public string description { get; set; }
        public decimal average { get; set; }
        public string Currency { get; set; }
        public string DetailsUrl { get; set; }
        public string ImageSource { get; set; }
        public List<string> MediaUrls { get; set; }
        public List<string> Amenities { get; set; }
        public List<Offer> Offers { get; set; }
        public List<string> AddressLines { get; set; }
        public string Fax { get; set; }
        public string Phone { get; set; }
        public bool Availability { get; set; }

    }

    public class Offer
    {
        public string OfferId { get; set; }
        public string Description { get; set; }
        public string RoomCategory { get; set; }
        public int Beds { get; set; }
        public string BedType { get; set; }
        public decimal Price { get; set; }
        public string Currency { get; set; }
        public string OfferDetailsUrl { get; set; }
    }
}
