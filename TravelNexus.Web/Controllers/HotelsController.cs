using DOTWconnect.Service;
using Fawaterk.Service;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TravelNexus.Services;
using TravelNexus.Services.TboServiceReference;
using TravelNexus.Web.Models;
using TravelNexus.Web.RepoServices;

namespace TravelNexus.Web.Controllers
{
    public class HotelsController : Controller
    {
        // GET: Hotels
        public ActionResult Index()
        {
            
            return View();
        }
        
        public ActionResult SearchHotels(SearchModel model)
        {
            if (model.Runno < 0)
                return View();

            var City = CitiesName.Cities.City.Find(c => c.Runno == model.Runno);
            model.CityCode = City.Code.ToString();
            model.HotelSearchModel.CountryName = City.CountryName;
            model.HotelSearchModel.CityName = City.Name;
            model.HotelSearchModel.GuestNationality = model.Nationality;
            
            TboService service = new TboService();
            MapDOTWResponseToTboResponse mapDOTWTbo = new MapDOTWResponseToTboResponse();
            DOTWconnectService DOTService = new DOTWconnectService();
            HotelSearchResponse res = null;
            //AmadeusService service = new AmadeusService();
            //var res = service.SearchHotelOffers(model.CityCode);
            if (!string.IsNullOrEmpty(model.HotelSearchModel.ChildAge))
            {
                string[] ChildAge = model.HotelSearchModel.ChildAge.Split(',');
                int[] ChldAge = Array.ConvertAll(ChildAge, s => int.Parse(s));
                try
                {

                    res = service.SearchHotelOffers(model.HotelSearchModel.CheckInDate, model.HotelSearchModel.CheckOutDate,
                   model.HotelSearchModel.CityName, model.HotelSearchModel.CountryName, model.HotelSearchModel.NoOfRooms,
                   model.HotelSearchModel.RoomGuests.AdultCount, model.HotelSearchModel.RoomGuests.ChildCount, ChldAge
                   , model.HotelSearchModel.GuestNationality);
                }
                catch (Exception ex)
                {

                }

            }
            else
            {
                try
                {
                    int[] ChldAge = new int[1] { 0 };
                    res = service.SearchHotelOffers(model.HotelSearchModel.CheckInDate, model.HotelSearchModel.CheckOutDate,
                    model.HotelSearchModel.CityName, model.HotelSearchModel.CountryName, model.HotelSearchModel.NoOfRooms,
                    model.HotelSearchModel.RoomGuests.AdultCount, model.HotelSearchModel.RoomGuests.ChildCount, ChldAge
                    , model.HotelSearchModel.GuestNationality);
                }
                catch (Exception ex)
                {


                }
            }
            //Webbeds
            try
            {
                var WebbedsHotelData = DOTService.SearchHotels(model.HotelSearchModel.CheckInDate, model.HotelSearchModel.CheckOutDate, City.CountryCode, City.Code, model.HotelSearchModel.RoomGuests.AdultCount, model.HotelSearchModel.RoomGuests.ChildCount, Convert.ToInt32(model.HotelSearchModel.ChildAge),model.HotelSearchModel.NoOfRooms);
                if (res == null || res.HotelResultList == null)
                {
                    res = new HotelSearchResponse();
                    if (WebbedsHotelData.SearchHotelsList.Result.Hotels != null && WebbedsHotelData.SearchHotelsList.Result.Hotels.Hotel != null)
                    {
                        res.CheckInDate = WebbedsHotelData.SearchHotelsList.Result.Hotels.Hotel[0].HotelCheckIn;
                        res.CheckOutDate = WebbedsHotelData.SearchHotelsList.Result.Hotels.Hotel[0].HotelCheckOut;
                        res.CityId = WebbedsHotelData.SearchHotelsList.Result.Hotels.Hotel[0].CityCode;
                        res.ResponseTime = WebbedsHotelData.SearchHotelsList.Result.ElapsedTime;
                        res.SessionId = WebbedsHotelData.SearchHotelsList.Result.TID;
                        res.Status = new ResponseStatus
                        {
                            StatusCode = WebbedsHotelData.SearchHotelsList.Result.Successful,
                        };
                    }

                    res.RoomGuests = new TravelNexus.Services.TboServiceReference.RoomGuest[]
                    {
                        new TravelNexus.Services.TboServiceReference.RoomGuest
                        {
                            AdultCount = model.HotelSearchModel.RoomGuests.AdultCount,
                            ChildCount = model.HotelSearchModel.RoomGuests.ChildCount
                        }
                    };
                    res.HotelResultList = new List<Hotel_Result>();

                    res = mapDOTWTbo.MapDOTWHotelRespToTboResp(WebbedsHotelData, res);

                    TempData["DOTWHotels"] = WebbedsHotelData;
                    TempData["checkIn"] = model.HotelSearchModel.CheckInDate;
                    TempData["checkOut"] = model.HotelSearchModel.CheckOutDate;

                    return View(res);
                }


                res = mapDOTWTbo.MapDOTWHotelRespToTboResp(WebbedsHotelData, res);

                TempData["DOTWHotels"] = WebbedsHotelData;
                TempData["checkIn"] = model.HotelSearchModel.CheckInDate;
                TempData["checkOut"] = model.HotelSearchModel.CheckOutDate;


            }
            catch (Exception ex)
            {


            }
            return View(res);
        }
        public ActionResult HotelDetails(HotelItem model)
        {
            try
            {
                if (string.IsNullOrEmpty(model.HotelData?.HotelInfo?.HotelCode) && !model.redirectUrl)
                    return View(new HotelDetailsModel());

                DOTWconnectHotelsViewModel DOTWHotels = TempData["DOTWHotels"] as DOTWconnectHotelsViewModel;
                DateTime checkIn = Convert.ToDateTime(TempData["checkIn"]);
                DateTime checkOut = Convert.ToDateTime(TempData["checkOut"]);
                TempData["DOTWHotels"] = DOTWHotels;
                TempData.Keep();

                SearchHotelsViewModel HotelDetailsModel = new SearchHotelsViewModel();
                if (model.ApiName.Equals("Webbeds Api"))
                {
                    
                    var WebbedsRoomsData = new DOTWconnectService().GetRooms(model.redirectUrl ? model.HotelCode : model.HotelData.HotelInfo.HotelCode, checkIn,checkOut);

                    if (WebbedsRoomsData == null || WebbedsRoomsData.GetRoomsDetails == null || DOTWHotels == null || DOTWHotels.SearchHotelsList == null || DOTWHotels.SearchHotelsList.Result.Hotels == null || DOTWHotels.SearchHotelsList.Result.Hotels.Hotel == null)
                    {
                        return View("WebbedsHotelDetails", HotelDetailsModel);
                    }

                    HotelDetailsModel.GetRoomsDetails = WebbedsRoomsData.GetRoomsDetails;
                    HotelDetailsModel.HotelDetails = DOTWHotels.SearchHotelsList.Result.Hotels.Hotel.Find(f => f.Hotelid.Equals(model.redirectUrl ? model.HotelCode : model.HotelData.HotelInfo.HotelCode));


                    return View("WebbedsHotelDetails", HotelDetailsModel);
                }

                //Hotel model
                //AmadeusService service = new AmadeusService();
                //var res = service.GetHotelByDetailsUrl(model.DetailsUrl);
                TboService service = new TboService();
                HotelDetailsModel modelData = new HotelDetailsModel();
                modelData.HotelDetails = service.GetHotelDetails(model.HotelData.HotelInfo.HotelCode);
                modelData.Rooms = service.GetHotelRoomAvailability(model.SessionId, model.ResultIndex, model.HotelData.HotelInfo.HotelCode);
                modelData.SessionId = model.SessionId;
                modelData.ResultIndex = model.ResultIndex;
                modelData.CheckInDate = model.CheckInDate;
                modelData.CheckOutDate = model.CheckOutDate;
                modelData.NoOfRequestedRoom = model.NoOfRequestedRoom;
                modelData.NoOfPersons = model.NoOfPersons;
                modelData.HotelImage = model.HotelImage;
                return View(modelData);
            }
            catch (Exception ex)
            {
                return View(new HotelDetailsModel());
            }
        }

        public ActionResult HotelBookDetails(HotelRoomModel model)
        {
            try
            {
                TboService service = new TboService();
                HotelBookingModel modelData = new HotelBookingModel();
                AvailabilityAndPricingResponse RoomBookingResponse = service.GetHotelRoomAvailabilityAndPricing_ForBook(model.SessionId, model.RoomIndex, model.ResultIndex);
                //booking hotel and room data
                modelData.RoomBookingResponse = RoomBookingResponse;
                modelData.CheckInDate = model.CheckInDate;
                modelData.CheckOutDate = model.CheckOutDate;
                modelData.NoOfRooms = model.NoOfRequestedRoom;
                modelData.ResultIndex = model.ResultIndex;
                modelData.SessionId = model.SessionId;
                modelData.HotelCode = model.HotelCode;
                modelData.RoomIndex = model.RoomIndex;
                modelData.RoomTypeCode = model.AvailableHotelRoom.RoomTypeCode;
                modelData.RatePlanCode = model.AvailableHotelRoom.RatePlanCode;
                modelData.CityName = model.CityName;
                modelData.CountryName = model.CountryName;
                modelData.HotelName = RoomBookingResponse.HotelDetails.HotelName;
                modelData.HotelRating = RoomBookingResponse.HotelDetails.HotelRating.ToString();
                modelData.RoomTypeName = RoomBookingResponse.HotelRooms[0].RoomTypeName;
                modelData.RoomFare = RoomBookingResponse.HotelRooms[0].RoomRate.RoomFare;
                modelData.RoomTax = RoomBookingResponse.HotelRooms[0].RoomRate.RoomTax;
                modelData.Currency = RoomBookingResponse.HotelRooms[0].RoomRate.Currency;

                System.TimeSpan diffResult = model.CheckOutDate.Subtract(model.CheckInDate);
                modelData.NoOfNight = diffResult.Days;
                modelData.NoOfPersons = model.NoOfPersons;
                modelData.HotelImage = model.HotelImage;                
                modelData.CountryCodes = CountryList();

                return View("_HotelBooking", modelData);
            }
            catch (Exception ex)
            {
                return View(new HotelDetailsModel());
            }
        }

        
        private static List<SelectListItem> CountryList()
        {
            List<SelectListItem> CountryList = new List<SelectListItem>();
            CountryList.Add(new SelectListItem { Value = "0", Text = "Select ..." });
            CountryList.AddRange(Country.CountryCodes.Select(kv => new SelectListItem { Value = kv.Key, Text = kv.Value }));
            return CountryList;
        }

        public ActionResult HotelBook(HotelBookingModel model)
        {
            try
            {
                if (!ModelState.IsValid)
                    return View("_HotelBooking", new HotelBookingModel());

                model.CountryCodes = CountryList();
                TboService service = new TboService();
                HotelBookRequest hotelBookObj = MapBookModelToTBObject(model);
                //1. book
                HotelBookResponse BookResp = service.HotelRoomBook(hotelBookObj);
                if (BookResp.BookingStatus == APIHotelBookingStatus.Vouchered)
                {
                    //2. generate invoice
                    GenerateInvoiceRequest gReq = GenerateInvoiceObj(BookResp.ConfirmationNo, hotelBookObj);
                    GenerateInvoiceResponse res = service.GenerateInvoice(gReq);
                    if (!string.IsNullOrEmpty(res.InvoiceNo))
                    {
                        //3. get hotel book details
                        HotelBookingDetailRequest bdReq = GenerateHotelBookDetailsRequest(BookResp.ConfirmationNo, hotelBookObj);
                        HotelBookingDetailResponse bdresp = service.HotelBookingDetail(bdReq);

                        HotelBookConfirmationModel confirmModel = MapHotelBookingDetaillToConfirmModel(bdresp, model);
                        return View("_ThankYou", confirmModel);
                    }
                    model.Error = res.Status.Description;
                }
                else
                    model.Error = BookResp.Status.Description;
                return View("_HotelBooking", model);
            }
            catch (Exception ex)
            {
                return View("_HotelBooking", new HotelBookingModel());
            }
        }

        private static HotelBookRequest MapBookModelToTBObject(HotelBookingModel model)
        {
            HotelBookRequest hotelObj = new HotelBookRequest();
            hotelObj.CheckInDate = model.CheckInDate;
            hotelObj.CheckOutDate = model.CheckOutDate;
            hotelObj.HotelCode = model.HotelCode;
            hotelObj.HotelName = model.HotelName;
            hotelObj.NoOfRooms = model.NoOfRooms;
            hotelObj.SessionId = model.SessionId;
            hotelObj.ResultIndex = Convert.ToInt32(model.ResultIndex);
            hotelObj.RestrictDuplicateBooking = true;
            hotelObj.GuestNationality = model.CountryCode;
            hotelObj.HotelRooms = new RequestedRooms[1] {
                new RequestedRooms {
                    RoomIndex = model.RoomIndex, RoomTypeName = model.RoomTypeName,RoomTypeCode=model.RoomTypeCode,
                    RatePlanCode=model.RatePlanCode,
                    RoomRate=new Rate{ RoomFare=model.RoomFare, RoomTax=model.RoomTax, TotalFare= model.RoomFare+model.RoomTax}
                }
            };
            hotelObj.PaymentInfo = new PaymentInfo();
            int cardtype = model.CardType - 1;
            hotelObj.PaymentInfo.PaymentModeType = (PaymentModeType)cardtype;
            hotelObj.PaymentInfo.VoucherBooking = true;
            if (model.CardType == 2)
            {
                hotelObj.PaymentInfo.CvvNumber = model.CvvNumber.ToString();
                hotelObj.PaymentInfo.PaymentCard = new PaymentCard();
                hotelObj.PaymentInfo.PaymentCard.CardNumber = model.CardNumber;
                hotelObj.PaymentInfo.PaymentCard.CardHolderFirstName = model.CardHolderFirstName;
                hotelObj.PaymentInfo.PaymentCard.CardHolderlastName = model.CardHolderlastName;
                hotelObj.PaymentInfo.PaymentCard.CardExpirationMonth = model.CardExpirationMonth;
                hotelObj.PaymentInfo.PaymentCard.CardExpirationYear = model.CardExpirationYear;
                hotelObj.PaymentInfo.PaymentCard.BillingCurrency = model.Currency;
                hotelObj.PaymentInfo.PaymentCard.BillingAmount = (model.RoomFare * model.NoOfRooms) + model.RoomTax;
            }
            hotelObj.Guests = new Guest[1] { new Guest { FirstName = model.FirstName, LastName = model.LastName,Title= "Mr",
                                                         GuestInRoom=model.NoOfPersons, LeadGuest=true, GuestType= GuestType.Adult} };
            hotelObj.AddressInfo = new AddressInfo
            {
                Country = Country.GetCountryNameByCode(model.CountryCode),
                Email = model.Email,
                CountryCode = model.CountryCode,
                PhoneNo = model.PhoneNumber.ToString()
            };
            //ddmmyyHHmmssfff#abxy
            hotelObj.ClientReferenceNumber = DateTime.Now.ToString("ddmmyyHHmmssfff") + "#" + GenerateClientReferenceNumber(4);

            return hotelObj;
        }

        private static GenerateInvoiceRequest GenerateInvoiceObj(string ConfirmationNo, HotelBookRequest model)
        {
            GenerateInvoiceRequest invoiceObj = new GenerateInvoiceRequest();
            invoiceObj.ConfirmationNo = ConfirmationNo;
            invoiceObj.PaymentInfo = model.PaymentInfo;
            return invoiceObj;
        }

        private static HotelBookingDetailRequest GenerateHotelBookDetailsRequest(string ConfirmationNo, HotelBookRequest model)
        {
            HotelBookingDetailRequest hotelbookingdetailsObj = new HotelBookingDetailRequest();
            hotelbookingdetailsObj.ConfirmationNo = ConfirmationNo;
            hotelbookingdetailsObj.ClientReferenceNumber = model.ClientReferenceNumber;
            hotelbookingdetailsObj.PaymentModeType = model.PaymentInfo.PaymentModeType;
            return hotelbookingdetailsObj;
        }
        private static HotelBookConfirmationModel MapHotelBookingDetaillToConfirmModel(HotelBookingDetailResponse resp, HotelBookingModel bookModel)
        {
            HotelBookConfirmationModel model = new HotelBookConfirmationModel();
            model.CheckInDate = resp.BookingDetail.CheckInDate;
            model.CheckOutDate = resp.BookingDetail.CheckOutDate;
            model.BookingDate = resp.BookingDetail.BookingDate;
            model.ConfirmationNo = resp.BookingDetail.ConfirmationNo;
            model.InvoiceNumber = resp.BookingDetail.InvoiceNumber;
            model.HotelName = resp.BookingDetail.HotelName;
            model.RoomName = resp.BookingDetail.Roomtype[0].RoomName;
            model.NoOfRooms = resp.BookingDetail.NoOfRooms;
            model.Total = resp.BookingDetail.Roomtype[0].RoomRate.TotalFare;
            model.FirstName = bookModel.FirstName;
            model.LastName = bookModel.LastName;
            model.Email = bookModel.Email;
            model.Country = Country.GetCountryNameByCode(bookModel.CountryCode);
            int cardtype = bookModel.CardType - 1;
            model.PaymentType = ((PaymentModeType)cardtype).ToString();
            model.LastCancellationDeadline = resp.BookingDetail.HotelCancelPolicies.LastCancellationDeadline.Value;
            model.TextPolicy = resp.BookingDetail.HotelCancelPolicies.DefaultPolicy;

            return model;
        }


        public static string GenerateClientReferenceNumber(int length)
        {
            Random random = new Random();
            string characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }
            return result.ToString();
        }

    }
}