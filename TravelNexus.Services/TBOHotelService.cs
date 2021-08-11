using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TravelNexus.Services.TboServiceReference;

namespace TravelNexus.Services
{
    public partial class TboService
    {
        public HotelSearchResponse SearchHotelOffers(DateTime CheckIn, DateTime CheckOut, string CityName,string CountryName, int NoOfRooms, int AdultCount, int ChildCount, int[] ChildAge
            , string GuestNationality)
        {
            try
            {
                ChannelFactory<IHotelService> factory = new ChannelFactory<IHotelService>("WSHttpBinding_IHotelService", new EndpointAddress("http://api.tbotechnology.in/HotelAPI_V7/HotelService.svc"));
                IHotelService proxy = factory.CreateChannel();

                //get city and nationality
                string Nationality = Country.GetCountryCodeByName(GuestNationality);
                var CityList = Cities(Country.GetCountryCodeByName(CountryName));
                var city = CityList.Where(x => x.CityName.ToLower() == CityName.ToLower()).FirstOrDefault();

                HotelSearchRequest req = new HotelSearchRequest(); /* request object*/
                req.Credentials = new AuthenticationData();
                req.Credentials.UserName = Username;
                req.Credentials.Password = Password;
                req.CheckInDate = CheckIn;
                req.CheckOutDate = CheckOut;
                if (city != null)
                {
                    req.CityId = Convert.ToInt32(city.CityCode); //;//115936;
                   
                }
                req.NoOfRooms = NoOfRooms;
                req.RoomGuests = new RoomGuest[] { new RoomGuest { AdultCount = AdultCount, ChildCount = ChildCount, ChildAge = ChildAge } };
                req.GuestNationality = Nationality;//"IN";
                HotelSearchResponse resp = new HotelSearchResponse(); /* response object */
                resp = proxy.HotelSearch(req);
                return resp;
            }
            catch
            {

                throw;
            }
        }

        public APIHotelDetails GetHotelDetails(string HotelCode)
        {
            ChannelFactory<IHotelService> factory = new ChannelFactory<IHotelService>("WSHttpBinding_IHotelService", new EndpointAddress("http://api.tbotechnology.in/HotelAPI_V7/HotelService.svc"));
            IHotelService proxy = factory.CreateChannel();

            HotelDetailsRequest req = new HotelDetailsRequest(); /* request object*/
            req.Credentials = new AuthenticationData();
            req.Credentials.UserName = Username;
            req.Credentials.Password = Password;
            req.HotelCode = HotelCode;
            HotelDetailsResponse resp = new HotelDetailsResponse(); /* response object */
            resp = proxy.HotelDetails(req);
            return resp.HotelDetails;
        }

        public HotelRoomAvailabilityResponse GetHotelRoomAvailability(string SessionId, string ResultIndex, string HotelCode)
        {
            ChannelFactory<IHotelService> factory = new ChannelFactory<IHotelService>("WSHttpBinding_IHotelService", new EndpointAddress("http://api.tbotechnology.in/HotelAPI_V7/HotelService.svc"));
            IHotelService proxy = factory.CreateChannel();

            HotelRoomAvailabilityRequest req = new HotelRoomAvailabilityRequest(); /* request object*/
            req.Credentials = new AuthenticationData();
            req.Credentials.UserName = Username;
            req.Credentials.Password = Password;
            req.SessionId = SessionId;
            req.ResultIndex = Convert.ToInt32(ResultIndex);
            req.HotelCode = HotelCode;
            HotelRoomAvailabilityResponse resp = new HotelRoomAvailabilityResponse(); /* response object */
            resp = proxy.AvailableHotelRooms(req);
            return resp;
        }
        public AvailabilityAndPricingResponse GetHotelRoomAvailabilityAndPricing_ForBook(string SessionId, int RoomIndex, string ResultIndex)
        {
            ChannelFactory<IHotelService> factory = new ChannelFactory<IHotelService>("WSHttpBinding_IHotelService", new EndpointAddress("http://api.tbotechnology.in/HotelAPI_V7/HotelService.svc"));
            IHotelService proxy = factory.CreateChannel();
            RoomCombination[] rCombination = new RoomCombination[1] { new RoomCombination { RoomIndex = new int[] { RoomIndex } } };

            AvailabilityAndPricingRequest req = new AvailabilityAndPricingRequest(); /* request object*/
            req.Credentials = new AuthenticationData();
            req.Credentials.UserName = Username;
            req.Credentials.Password = Password;
            req.SessionId = SessionId;
            req.ResultIndex = Convert.ToInt32(ResultIndex);
            req.OptionsForBooking = new BookingOptions { FixedFormat = false, RoomCombination = rCombination };
            req.IsRoomInformationRequired = true;
            AvailabilityAndPricingResponse resp = new AvailabilityAndPricingResponse(); /* response object */
            resp = proxy.AvailabilityAndPricing(req);
            return resp;
        }

        public HotelBookResponse HotelRoomBook(HotelBookRequest req)
        {
            ChannelFactory<IHotelService> factory = new ChannelFactory<IHotelService>("WSHttpBinding_IHotelService", new EndpointAddress("http://api.tbotechnology.in/HotelAPI_V7/HotelService.svc"));
            IHotelService proxy = factory.CreateChannel();

            req.Credentials = new AuthenticationData();
            req.Credentials.UserName = Username;
            req.Credentials.Password = Password;

            HotelBookResponse resp = new HotelBookResponse(); /* response object */
            resp = proxy.HotelBook(req);
            return resp;
        }

        public GenerateInvoiceResponse GenerateInvoice(GenerateInvoiceRequest req)
        {
            ChannelFactory<IHotelService> factory = new ChannelFactory<IHotelService>("WSHttpBinding_IHotelService", new EndpointAddress("http://api.tbotechnology.in/HotelAPI_V7/HotelService.svc"));
            IHotelService proxy = factory.CreateChannel();

            req.Credentials = new AuthenticationData();
            req.Credentials.UserName = Username;
            req.Credentials.Password = Password;

            GenerateInvoiceResponse resp = new GenerateInvoiceResponse(); /* response object */
            resp = proxy.GenerateInvoice(req);
            return resp;
        }

        public HotelBookingDetailResponse HotelBookingDetail(HotelBookingDetailRequest req)
        {
            ChannelFactory<IHotelService> factory = new ChannelFactory<IHotelService>("WSHttpBinding_IHotelService", new EndpointAddress("http://api.tbotechnology.in/HotelAPI_V7/HotelService.svc"));
            IHotelService proxy = factory.CreateChannel();

            req.Credentials = new AuthenticationData();
            req.Credentials.UserName = Username;
            req.Credentials.Password = Password;

            HotelBookingDetailResponse resp = new HotelBookingDetailResponse(); /* response object */
            resp = proxy.HotelBookingDetail(req);
            return resp;
        }
    }
}
