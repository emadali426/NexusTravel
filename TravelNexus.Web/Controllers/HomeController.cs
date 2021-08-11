using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelNexus.Services;
using TravelNexus.Web.Models;
using DOTWconnect.Service.Entities;
using DOTWconnect.Service;
using Fawaterk.Service;
using System.Net.Mail;
using SMTP2Go.Service.Models;

namespace TravelNexus.Web.Controllers
{
   
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //new Fawaterk.Service.FawaterkService().FawaterkRequestSender(new Fawaterk.Service.Entities.InvoiceRequestModel {
            //    customerInfo = new Fawaterk.Service.Entities.CustomerInfo
            //    {
            //        Address = "EGY",
            //        Email = "abdelrahmanali.work2021@gmail.com",
            //        FirstName = "AbdelRahman",
            //        LastName = "Ali",
            //        Phone = "0100000000",
            //        UserId = 1
            //    },
            //    roomInfo = new Fawaterk.Service.Entities.RoomInfo
            //    {
            //        Price = 1500,
            //        RoomName = "AAA Room"
            //    }
            //});



            //search.Packages = new List<Package>(new TransactionsDbContext().Packages.Where(p => p.ExpireDate.CompareTo(DateTime.Now) > 0).ToList());

            //InternalCodes.GetAllCountriesIDs();
            var uow = new Fawaterk.Service.RepoServices.UnitOfWork(new FawaterksDbContext());
            var pck = uow.packages.Find(p => (p.OfferEndDate.CompareTo(DateTime.Now) >= 0) && (p.OfferStartDate.CompareTo(DateTime.Now) <= 0) && !p.IsArchived && !p.IsDeleted ).ToList();
            var os = uow.ourServices.GetAll().ToList();
            var nexNews = uow.nexusNews.GetAll().ToList();
            var popHotels = uow.popHotels.Find(h=>!h.IsDeleted).OrderByDescending(p => p.Id).Take(4).ToList();
            var favDests = uow.favDest.GetAll().ToList();
            var lastNexNews = uow.nexusNews.GetAll().OrderByDescending(p => p.Id).Take(1).FirstOrDefault();
            var model = new SearchModel
            {
                StartDate = DateTime.Now,
                EndDate = DateTime.Now,
                HotelSearchModel = new HotelSearchModel
                {
                    CountriesIds = InternalCodes.getAllCountriesIDs
                },
                Packages = pck,
                OurServices = os,
                NexusNews = nexNews,
                FavDests = favDests,
                PopHotels = popHotels,
                LastNexusNews = lastNexNews
            };
            
            
            return View(model);
        }

        public ActionResult Error()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }

        public ActionResult Contact()
        {
            return View();
        }

        public JsonResult AutoGetCities(string Prefix)
        {
            var Names = CitiesName.Cities.City.FindAll(p => p.Name.ToUpper().Contains(Prefix.ToUpper()));

            return Json(Names, JsonRequestBehavior.AllowGet);
        }

        public JsonResult AutoGetCountries(string Prefix)
        {
            var Names = CountryVATsConfig._countryVAT.Where(p => p.Key.ToUpper().Contains(Prefix.ToUpper())).ToList();

            return Json(Names, JsonRequestBehavior.AllowGet);
        }

        //public ActionResult locationCheck(long runo, string NatName)
        //{
        //    bool res = runo != -1 && CountryVATsConfig._countryVAT.ContainsKey(NatName);
        //    return Json(new
        //    {
        //        value = "Please Select Value From List",
        //        result = res
        //    }, JsonRequestBehavior.AllowGet); 
        //} 

        public ActionResult GetCityIds(string countryName)
        {
            if (string.IsNullOrEmpty(countryName))
            {
                return Json(new List<object>(), JsonRequestBehavior.AllowGet);
            }

            

            InternalCodes.GetAllCitiesIDs(countryName);

            //var dataCitiesOfCountry = InternalCodes.getAllCitiesIDs;

            //Dictionary<string, string> NoCities = new Dictionary<string, string>();

            //NoCities.Add("No Cities Found", "No Cities Found");

            //if (dataCitiesOfCountry.ContainsKey(countryName.ToUpper()) == false)
            //    return Json(NoCities, JsonRequestBehavior.AllowGet);
        
            //Dictionary<string, string> data = dataCitiesOfCountry[countryName.ToUpper()];

            //var data = dataCities.Select(o => new { Name = o.Key, Code = o.Value });

            return Json(InternalCodes.getAllCitiesIDs[countryName.ToUpper()], JsonRequestBehavior.AllowGet);
        }

        public ActionResult Message()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public ActionResult SentToMailList(string Email)
        {
            string Host =  SMTP2GoConfig.Host;
            string Password =  SMTP2GoConfig.Password;
            int Port = SMTP2GoConfig.Port;
            string SenderEmail = SMTP2GoConfig.SenderEmail;
            string Username = SMTP2GoConfig.UserName;

            SmtpClient smtpClient = new SmtpClient(Host,Port);
            smtpClient.UseDefaultCredentials = false;
            smtpClient.Credentials = new System.Net.NetworkCredential(Username, Password);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtpClient.EnableSsl = true;

            MailMessage mail = new MailMessage();
            mail.From = new MailAddress(Email);
            mail.To.Add(new MailAddress(SenderEmail));
            mail.Subject = "Add to mail list";
            mail.Body = "Please, add me to the mail list";
            try
            {
                smtpClient.Send(mail);
                return Json("Email send successfully");
            }
            catch (Exception ex)
            {
                return Json(ex.Message);
            }
        }
    }
}