using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TravelNexus.Services;

namespace TravelNexus.Web.Models
{
    public class FlightBookingModel
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Mobile { get; set; }
        public string FlightDetails { get; set; }
    }
}