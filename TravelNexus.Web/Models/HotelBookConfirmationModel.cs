using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelNexus.Web.Models
{
    public class HotelBookConfirmationModel
    {
        //booking Info
        public string ConfirmationNo { get; set; }
        public string InvoiceNumber { get; set; }
        public string HotelConfirmationNo { get; set; }
        public string HotelName { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public DateTime BookingDate { get; set; }
        public int NoOfRooms { get; set; }
        public string RoomName { get; set; }
        public decimal Total { get; set; }

        //personalInfo
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        //payment
        public string PaymentType { get; set; }
        public DateTime LastCancellationDeadline { get; set; }
        public string TextPolicy { get; set; }
    }
}


