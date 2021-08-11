using Fawaterk.Service;
using SMTP2Go.Service.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TravelNexus.Web.Models;

namespace TravelNexus.Web
{
    public class CurrenciesModel
    {
        public CurrenciesModel()
        {
            Curr = CurrenciesConfig._currencies.ToList();
            CurrentStateCurrency = Curr[0];
        }

        public List<KeyValuePair<string, double>> Curr { get; set; }
        public KeyValuePair<string, double> CurrentStateCurrency { get; set; }
    }
    public class Currency
    {
        public string CurrName { get; set; }
        public double CurrValue { get; set; }
    }

    public class PopUserViewModel
    {
        public long PopHotelsId { get; set; }
        public long UserId { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }
        public int NumOfRooms { get; set; }
        public int NumOfAdults { get; set; }
        public int NumOfKids { get; set; }
        public string MPhone { get; set; }

    }
    public class ServiceModel
    {
        public string ServiceTitle { get; set; }
        public string Description { get; set; }
        public HttpPostedFileBase ServiceIamgeName { get; set; }
    }
    public class DashboardModel
    {
        public List<Package> pcksModel { get; set; }
        public List<Package> deletedPcksModel { get; set; }
        public List<Package> outPcksModel { get; set; }
        public List<Package> arcPcksModel { get; set; }
        public SMTPViewModel smtpModel { get; set; }
        public APIsViewModel apisModel { get; set; }
        public List<ApplicationUser> Partners { get; set; }
        public List<ApplicationUser> Clients { get; set; }
        public List<MailNewsModel> MailNewsUsers { get; set; }
        public List<OurServiceModel> OurServices { get; set; }
        public List<KeyValuePair<string, double>> Currencies { get; set; }
        public List<NewsModel> nexusNews { get; set; }
        public List<PopHotelsModel> popHotels { get; set; }
        public List<OurCustomerFavDestModel> favDests { get; set; }

        public List<Fawaterk.Service.Entities.FawaterkTrackingModel> fawaters { get; set; }

        public List<PopUserModelVM> HotelRequests { get; set; }

    }
    public class SendToEMail
    {
        public string Email { get; set; }
   
    }

    public class MailNewsViewModel
    {
        public string Subject { get; set; }
        public string Body { get; set; }
    }

    public class SendMailViewModel
    {
        public string SendTo { get; set; }
        public string Subject { get; set; }
        public string MailBody { get; set; }
    }

    public class ApproveUserModel
    {
        public long Id { get; set; }
        public float TaxCard { get; set; }
    }
    public class PackageViewModel
    {
        public long Id { get; set; }
        public string Title { get; set; }
        public DateTime OfferStartDate { get; set; }
        public DateTime OfferEndDate { get; set; }
        public DateTime PackageStartDate { get; set; }
        public DateTime PackageEndDate { get; set; }
        public bool IsDeleted { get; set; }
        public bool IsArchived { get; set; }
        public bool IsHolded { get; set; }
        public double Price { get; set; }
        public int NumOfRes { get; set; }
        public HttpPostedFileBase[] ImgsFiles { get; set; }
        public string FolderImgsName { get; set; }
        public string PackageDescription { get; set; }
        public HttpPostedFileBase MainIamgeName { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        public int ImagesCount { get; set; }
    }

    public class PopularHotelsViewModel
    {
        public string HotelName { get; set; }
        public HttpPostedFileBase[] ImgsFiles { get; set; }
        public string PopHotelsDescription { get; set; }
        public HttpPostedFileBase MainIamgeName { get; set; }
        public HttpPostedFileBase logoIamgeName { get; set; }
        public double AvgPrice { get; set; }
        public int Stars { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }

    public class NewsViewModel
    {
        public string Title { get; set; }
        public string DescriptionFileName { get; set; }
        public HttpPostedFileBase FolderImgsName { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

    }

    public class FavDestViewModel
    {
        public string DestinationName { get; set; }
        public string Description { get; set; }
        public HttpPostedFileBase ImgName { get; set; }
        public int Reviews { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }

    }

    public class ImagesMetaData
    {
        public string FolderPath { get; set; }
        public int ImageCounter { get; set; }
    }



    public class APIsViewModel
    {
        public string GoogleKey { get; set; }
        public string FawaterkKey { get; set; }
    }

    public class FrontEndManageViewModel
    {
        public bool OurService { get; set; }
        public bool OurCustomersFavDest { get; set; }
        public bool PopularHotels { get; set; }
    }

    public class CityModel
    {
        public static int Runno { get; set; }
        public string Name { get; set; }
        public static int Code { get; set; }
        public static string CountryName { get; set; }
        public static int CountryCode { get; set; }
    }

    public class PopUserModelVM
    {
        public PopHotelsModel Hotel { get; set; }
        public PopUserModel HotelRequests { get; set; }
        public ApplicationUser User { get; set; }
    }
}