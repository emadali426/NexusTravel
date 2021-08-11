using System.Collections.Generic;
using System.Net;
using TravelNexus.Services.TboServiceReference;

namespace TravelNexus.Web.Models
{
    public class SearchHotelsViewModel
    {
        //public DOTWconnect.Service.Entities.SearchHotelsResponse.Root SearchHotelsList { get; set; }
        public DOTWconnect.Service.Entities.SearchHotelsResponse.Hotel HotelDetails { get; set; }
        //public DOTWconnect.Service.Entities.SearchAvailableHotelsResponse.Root SearchAvailableHotels { get; set; }
        public DOTWconnect.Service.Entities.GetRoomsResponse.Root GetRoomsDetails { get; set; }
        public string CheckImageAvailablity(string hotelImages)
        {

            try
            {
                HttpWebRequest requestT = (HttpWebRequest)HttpWebRequest.Create(hotelImages);
                requestT.Method = "HEAD";

                bool existsT;
                try
                {
                    requestT.GetResponse();
                    existsT = true;
                }
                catch
                {
                    existsT = false;
                }

                if (existsT)
                    return hotelImages;

                return null;
            }
            catch (System.Exception)
            {

                return null;
            }
        }

        public string CheckImagesAvailablity(DOTWconnect.Service.Entities.SearchHotelsResponse.HotelImages hotelImages)
        {

            HttpWebRequest requestT = (HttpWebRequest)HttpWebRequest.Create(hotelImages?.Thumb?.CdataSection ?? "https://imagenotfound.com/image.jpg");
            requestT.Method = "HEAD";

            bool existsT;
            try
            {
                requestT.GetResponse();
                existsT = true;
            }
            catch
            {
                existsT = false;
            }

            if (existsT)
                return hotelImages?.Thumb?.CdataSection;

            foreach (var image in hotelImages?.Image ?? new List<DOTWconnect.Service.Entities.SearchHotelsResponse.Image>())
            {
                HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(image?.Url?.CdataSection ?? "https://imagenotfound.com/image.jpg");
                request.Method = "HEAD";

                bool exists;
                try
                {
                    request.GetResponse();
                    exists = true;
                }
                catch
                {
                    exists = false;
                }

                if (exists)
                    return image?.Url?.CdataSection;
            }

            return "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcRP542P7S8Dl2OZgc8OEQKzC_cYkr9g0a4hzg&usqp=CAU";
        }

        public HotelInfoHotelRating GetRatings(string Rating)
        {
            HotelInfoHotelRating ratings = Rating == "563" ? HotelInfoHotelRating.FiveStar : Rating == "562" ? HotelInfoHotelRating.FourStar : Rating == "561" ? HotelInfoHotelRating.ThreeStar : Rating == "560" ? HotelInfoHotelRating.TwoStar : Rating == "559" ? HotelInfoHotelRating.OneStar : HotelInfoHotelRating.All;

            return ratings;
        }

    }
}