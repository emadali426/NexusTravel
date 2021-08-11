using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TravelNexus.Services
{

    public class bookingObject
    {

        public Traveler[] travelers { get; set; }
        public Remarks remarks { get; set; }
        public Ticketingagreement ticketingAgreement { get; set; }
        public Contact1[] contacts { get; set; }
    }

    public class Remarks
    {
        public General[] general { get; set; }
    }

    public class General
    {
        public string subType { get; set; }
        public string text { get; set; }
    }

    public class Ticketingagreement
    {
        public string option { get; set; }
        public string delay { get; set; }
    }

    public class Traveler
    {
        public int id { get; set; }
        public string dateOfBirth { get; set; }
        public Name name { get; set; }
        public string gender { get; set; }
        public Contact contact { get; set; }
        public Document[] documents { get; set; }
    }

    public class Name
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
    }

    public class Contact
    {
        public string emailAddress { get; set; }
        public Phone[] phones { get; set; }
    }

    public class Phone
    {
        public string deviceType { get; set; }
        public string countryCallingCode { get; set; }
        public string number { get; set; }
    }

    public class Document
    {
        public string documentType { get; set; }
        public string birthPlace { get; set; }
        public string issuanceLocation { get; set; }
        public string issuanceDate { get; set; }
        public string number { get; set; }
        public string expiryDate { get; set; }
        public string issuanceCountry { get; set; }
        public string validityCountry { get; set; }
        public string nationality { get; set; }
        public bool holder { get; set; }
    }

    public class Contact1
    {
        public Addresseename addresseeName { get; set; }
        public string companyName { get; set; }
        public string purpose { get; set; }
        public Phone[] phones { get; set; }
        public string emailAddress { get; set; }
        public TravellerAddress address { get; set; }
    }

    public class Addresseename
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
    }

    public class TravellerAddress
    {
        public string[] lines { get; set; }
        public string postalCode { get; set; }
        public string cityName { get; set; }
        public string countryCode { get; set; }
    }
}
