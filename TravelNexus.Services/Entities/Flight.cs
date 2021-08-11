using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;

namespace TravelNexus.Services
{
    // Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse); 
    public class Departure
    {
        public string iataCode { get; set; }
        public string terminal { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime at { get; set; }
    }

    public class Arrival
    {
        public string iataCode { get; set; }
        public string terminal { get; set; }
        [JsonConverter(typeof(DateTimeConverter))]
        public DateTime at { get; set; }
    }

    public class Aircraft
    {
        public string code { get; set; }
    }

    public class Operating
    {
        public string carrierCode { get; set; }
    }

    public partial class Segment
    {
        public Departure departure { get; set; }
        public Arrival arrival { get; set; }

        public string carrierCode { get; set; }

        public string number { get; set; }

        public Aircraft aircraft { get; set; }

        public Operating operating { get; set; }

        [JsonIgnore]
        public string duration { get; set; }

        public string id { get; set; }

        public int numberOfStops { get; set; }

        public bool blacklistedInEU { get; set; }

        [JsonIgnore]
        public string Airline { get; set; }
    }

    public class Itinerary
    {
        public string duration { get; set; }
        public List<Segment> segments { get; set; }
    }

    public class Fee
    {
        public string amount { get; set; }
        public string type { get; set; }
    }

    public partial class Price
    {
        public string currency { get; set; }

        public string total { get; set; }

        [JsonProperty(PropertyName = "base")]
        public string BasePrice { get; set; }

        public List<Fee> fees { get; set; }
        [JsonIgnore]
        public string grandTotal { get; set; }
    }

    public class PricingOptions
    {
        public List<string> fareType { get; set; }
        public bool includedCheckedBagsOnly { get; set; }
    }

    public partial class Price2
    {
        public string currency { get; set; }
        public string total { get; set; }
        public string @base { get; set; }
    }

    public class IncludedCheckedBags
    {
        public int quantity { get; set; }
    }

    public class FareDetailsBySegment
    {
        public string segmentId { get; set; }
        public string cabin { get; set; }
        public string fareBasis { get; set; }
        public string brandedFare { get; set; }
        public string @class { get; set; }
        public IncludedCheckedBags includedCheckedBags { get; set; }
    }

    public class TravelerPricing
    {
        public string travelerId { get; set; }
        public string fareOption { get; set; }
        public string travelerType { get; set; }
        public Price2 price { get; set; }
        public List<FareDetailsBySegment> fareDetailsBySegment { get; set; }
    }

    public partial class FlightObject
    {
        public string type { get; set; }
        public string id { get; set; }
        public string source { get; set; }
        public bool instantTicketingRequired { get; set; }
        public bool nonHomogeneous { get; set; }
        public bool oneWay { get; set; }
        public string lastTicketingDate { get; set; }
        public int numberOfBookableSeats { get; set; }
        public List<Itinerary> itineraries { get; set; }
        public Price price { get; set; }
        public PricingOptions pricingOptions { get; set; }
        public List<string> validatingAirlineCodes { get; set; }
        public List<TravelerPricing> travelerPricings { get; set; }
        [JsonIgnore]
        public string FlightOfferJson{ get; set; }
    }



    public partial class Flight : FlightObject
    {
        public string Name
        {
            get
            {
                return $"{Origin} To {Destination}";
            }
        }

        public string Origin { get; set; }
        public string Destination { get; set; }
        public DateTime DepartureDate { get; set; }
        public DateTime ArrivalDate { get; set; }
        public decimal AvgPrice { get; set; }
        public string Currency { get; set; }
        public string Airline { get; set; }
        public string flightDestinations { get; set; }
        public string flightOffers { get; set; }
        public int NoOfStops { get; set; }
        public int NoOfBookableSeats { get; set; }
        public bool IsOneWay { get; set; }
        public string TotalTime
        {
            get
            {
                TimeSpan diff = ArrivalDate - DepartureDate;

                return $"{diff.Hours} Hours, {diff.Minutes} Minutes";
            }
        }

        public string FlightOffer { get; set; }
    }


    public class DateTimeConverter : IsoDateTimeConverter
    {
        public DateTimeConverter()
        {
            base.DateTimeFormat = "YYYY-MM-DDTHH:mm:ss";
        }
    }
}