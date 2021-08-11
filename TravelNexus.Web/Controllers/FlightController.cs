using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelNexus.Services;
using TravelNexus.Web.Models;

namespace TravelNexus.Web.Controllers
{
    public class FlightController : Controller
    {

        public ActionResult SearchFlights(SearchModel model)
        {
            try
            {
                AmadeusService service = new AmadeusService();
                //      if(string.IsNullOrEmpty(model.To))
                //      {
                //var res =  service.SearchFlightInspiration(model.From);
                //  return View(res);

                //      }
                //      else if(model.StartDate.Date==model.EndDate.Date &&model.StartDate.Date== DateTime.Today.Date)
                //      {
                //          var res = service.SearchCheapestFlightDates(model.From, model.To);
                //          return View(res);
                //      }
                //      else { 
                var res = service.SearchFlightOffers(model.From, model.To, model.StartDate, model.EndDate, model.NoOfAdults, model.NonStop);
                return View(res);
                //}
            }
            catch (Exception ex)
            {
                return View(new List<Flight>());
            }
        }

        [HttpPost]
        public ActionResult FlightDetails(Flight model)
        {
            try
            {
                //if( string.IsNullOrEmpty(model)) 
                if (model == null)
                    return View(new FlightDetailsModel());
                AmadeusService service = new AmadeusService();
                var flight = service.MapFlightDetails(JToken.Parse(model.FlightOffer));//service.GetFlightOfferDetails(model.FlightOffer);
                var prices = service.GetFlightOfferPrices(model.FlightOffer);
                return View(new FlightDetailsModel() { Flight = flight, prices = prices });
            }
            catch (Exception ex)
            {
                return View(new FlightDetailsModel());
            }
        }
        [HttpPost]
        public ActionResult BookFlight(FlightObject model)
        {
            try
            {
                //if( string.IsNullOrEmpty(model)) 
                if (model == null)
                    return View(new FlightBookingModel()); //{ FlightDetails = model }
                return View(new FlightBookingModel() { FlightDetails = model.FlightOfferJson });
            }
            catch (Exception ex)
            {
                return View(new FlightBookingModel());
            }
        }

        [HttpPost]
        public ActionResult CompleteBookingFlight(FlightBookingModel model)
        {
            try
            {
                //if( string.IsNullOrEmpty(model)) 
                if (model == null)
                    return View(new FlightBookingModel());
                AmadeusService service = new AmadeusService();
                bookingObject obj = new bookingObject()
                {

                    contacts = new Contact1[1] {
                        new Contact1(){
                            address=new TravellerAddress()
                            {
                                cityName= "Cairo", countryCode="EG",postalCode="12345", lines=new string[1]{"test address"}
                            },
                            addresseeName= new Addresseename(){firstName= model.FirstName.ToUpper(), lastName=model.LastName.ToUpper() },
                            companyName="TEST COMPANY",
                            emailAddress=model.Email,
                            phones=new Phone[1]
                            {
                                new Phone() { countryCallingCode = "02", deviceType = "MOBILE", number = model.Phone }
                            },
                            purpose="STANDARD",

                        }
                    },
                    remarks = new Remarks() { general = new General[1] { new General() { subType = "GENERAL_MISCELLANEOUS", text = "ONLINE BOOKING FROM NEXUS TRAVEL" } } },
                    ticketingAgreement = new Ticketingagreement() { delay = "6D", option = "DELAY_TO_CANCEL" },
                    travelers = new Traveler[1]
                    {
                        new Traveler()
                        {
                            id=1,
                            dateOfBirth="1987-01-01",
                            gender="FEMALE",
                            name=new Name(){ firstName=model.FirstName,lastName= model.LastName },

                            contact = new Contact()
                            {
                                emailAddress = model.Email,
                                phones = new Phone[1]
                                {
                                    new Phone() { countryCallingCode = "02", deviceType = "MOBILE", number = model.Phone }
                                }
                            },
                            documents=new Document[1]
                            {
                                new Document()
                                {
                                    documentType="PASSPORT",
                                    birthPlace="Cairo",
                                    issuanceLocation="Cairo",
                                     issuanceDate="2017-01-01",
                                    number="123456789",
                                   expiryDate="2022-01-01",
                                    issuanceCountry="EG",
                                    validityCountry="EG",
                                    nationality="EG",
                                    holder=true,

                                }
                            },
                        }
                    }
                };
                var flight = service.CreateFlightOrder(model.FlightDetails, obj);
                return View(model);
            }
            catch (Exception ex)
            {
                return View(new FlightBookingModel());
            }
        }

    }
}