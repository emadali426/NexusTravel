using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelNexus.Services;

namespace TravelNexus.Web.Models
{
    public class FlightDetailsModel
    {
        public Flight Flight { get; set; }
        public FlightPrices prices  { get; set; }
    }
}