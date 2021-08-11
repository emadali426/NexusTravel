using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelNexus.Services
{
    public class Co2Emissions
    {
        public int weight { get; set; }
        public string weightUnit { get; set; }
        public string cabin { get; set; }
    }

    public partial class Segment
    {
        [JsonIgnore]
        public List<Co2Emissions> co2Emissions { get; set; }
    }



    public partial class Price
    {

        [JsonIgnore] 
        public string billingCurrency { get; set; }
    }

    public class Tax
    {
        public string amount { get; set; }
        public string code { get; set; }
    }

    public partial class Price2
    {

        [JsonIgnore] 
        public List<Tax> taxes { get; set; }
        [JsonIgnore] 
        public string refundableTaxes { get; set; }
    }

    public class BookingRequirements
    {
    }

    public class FlightPrices
    {
        public string type { get; set; }
        public List<FlightObject> flightOffers { get; set; }
        public BookingRequirements bookingRequirements { get; set; }

    }

}
