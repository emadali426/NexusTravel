using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelNexus.Services.TboServiceReference;

namespace TravelNexus.Web.Models
{
    public class HotelBookingModel
    {
        public AvailabilityAndPricingResponse RoomBookingResponse { get; set; }
        //booking data
        public int RoomIndex { get; set; }
        public string HotelName { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }
        public string HotelRating { get; set; }
        public string RoomTypeName { get; set; }
        public string RoomTypeCode { get; set; }
        public string RatePlanCode { get; set; }
        public decimal RoomFare { get; set; }
        public string Currency { get; set; }
        public decimal RoomTax { get; set; }
        public DateTime CheckInDate { get; set; }
        public DateTime CheckOutDate { get; set; }
        public int NoOfRooms { get; set; }
        public string ResultIndex { get; set; }
        public string SessionId { get; set; }
        public string HotelCode { get; set; }
        public double NoOfNight { get; set; }
        public int NoOfPersons { get; set; }
        public string HotelImage { get; set; }

        //personal info
        [Required(ErrorMessage = "The first name is required")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "The last name is required")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "The email address is required")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [System.ComponentModel.DataAnnotations.Compare("Email")]
        public string VEmail { get; set; }

        [Required(ErrorMessage = "Phone number is required")]
        [Display(Name = "Home Phone")]
        [DataType(DataType.PhoneNumber)]
        public int PhoneNumber { get; set; }
        public string CountryCode { get; set; }
        public List<SelectListItem> CountryCodes { get; set; }
        public bool RecievePromotions { get; set; }

        //card info
        public string CardNumber { get; set; }
        public string CardHolderFirstName { get; set; }
        public string CardHolderlastName { get; set; }
        public string CardExpirationMonth { get; set; }
        public string CardExpirationYear { get; set; }
        public int CvvNumber { get; set; }
        public int CardType { get; set; }
        public string CardidentificationNo { get; set; }
        public bool IsAgree { get; set; }

        public string ClientReferenceNumber { get; set; }
        public string Error { get; set; }
    }
}