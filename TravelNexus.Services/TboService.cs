using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using TravelNexus.Services.TboServiceReference;

namespace TravelNexus.Services
{
    public partial class TboService
    {
        public string BaseUrl { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string SiteName { get; set; }

        public TboService()
        {
            BaseUrl = ConfigurationManager.AppSettings["TboBaseUrl"];
            Username = ConfigurationManager.AppSettings["TboUsername"];
            Password = ConfigurationManager.AppSettings["TboPassword"];
            SiteName = ConfigurationManager.AppSettings["TboSiteName"];
        }
        public void GetHotels()
        {
            using (TboServiceReference.HotelServiceClient client = new TboServiceReference.HotelServiceClient("BasicHttpBinding_IHotelService1"))
            {
                TboServiceReference.GiataHotels[] hotels = null;
                TboServiceReference.APIHotelDetails[] hotelDetails = null;

                var status = client.TBOHotelCodeList(new TboServiceReference.AuthenticationData() { UserName = Username, Password = Password, SiteName = SiteName }, "CAI", "true", out hotels, out hotelDetails);
                // client.ClientCredentials=new System.ServiceModel.Description.ClientCredentials(){UserName=Username,Password=Password,Domain=;

            }
        }

        public CountryList[] Countries()
        {
            ChannelFactory<IHotelService> factory = new ChannelFactory<IHotelService>("WSHttpBinding_IHotelService", new EndpointAddress("http://api.tbotechnology.in/HotelAPI_V7/HotelService.svc"));
            IHotelService proxy = factory.CreateChannel();

            CountryListRequest req = new CountryListRequest(); /* request object*/
            req.Credentials = new AuthenticationData();
            req.Credentials.UserName = Username;
            req.Credentials.Password = Password;
            CountryListResponse resp = new CountryListResponse(); /* response object */
            try {
                
                resp = proxy.CountryList(req);
            }
            catch
            {

            }


            return resp.CountryList;

        }

        public CityList[] Cities(string CountryCode)
        {
            ChannelFactory<IHotelService> factory = new ChannelFactory<IHotelService>("WSHttpBinding_IHotelService", new EndpointAddress("http://api.tbotechnology.in/HotelAPI_V7/HotelService.svc"));

            IHotelService proxy = factory.CreateChannel();

            DestinationCityListRequest req = new DestinationCityListRequest(); /* request object*/
            req.Credentials = new AuthenticationData();
            req.Credentials.UserName = Username;
            req.Credentials.Password = Password;
            req.CountryCode = CountryCode;
            req.ReturnNewCityCodes = "TRUE";
            DestinationCityListResponse resp = new DestinationCityListResponse(); /* response object */
            resp = proxy.DestinationCityList(req);
            return resp.CityList;
        }
    }
}
