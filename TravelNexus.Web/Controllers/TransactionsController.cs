using DOTWconnect.Service;
using Fawaterk.Service;
using Microsoft.AspNet.Identity;
using SMTP2Go.Service.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using TravelNexus.Web.Models;
using TravelNexus.Web.RepoServices;

namespace TravelNexus.Web.Controllers
{
    [Authorize]
    public class TransactionsController : Controller
    {
        // GET: Transactions
        public ActionResult BookARoom(WebbedsHotelBookingModel model)
        {



            //var u = User.Identity.GetUserId<long>();
            var uow = new Fawaterk.Service.RepoServices.UnitOfWork(new FawaterksDbContext());
            var userUoW = new UnitOfWork(new ApplicationDbContext());
            var user = userUoW.users.Get(User.Identity.GetUserId<long>());
            var roomReservations = new List<UserRoomReservation>();
            var res = new UserRoomReservation
            {
                AllocationDetails = model.AllocationDetails,
                APIName = model.APIName,
                CityId = model.CityId,
                CreationDate = DateTime.Now,
                HotelId = model.HotelId,
                IsDeleted = false,
                Status = ReservationStatus.UnBooked,
                RoomTypeCode = model.RoomTypeCode,
                UserId = user.Id,
                Price = model.Price,
                HotelName = model.HotelName
            };
            uow.userRoomsReservation.Add(res);
            uow.Complete();

            roomReservations.Add(res);
            var fatora = new FawaterkService();
            var url = fatora.FawaterkRequestSender(new Fawaterk.Service.Entities.InvoiceRequestModel
            {
                customerInfo = new Fawaterk.Service.Entities.CustomerInfo
                {
                    UserId = user.Id,
                    //Address = user.PhoneNumber,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Phone = user.PhoneNumber
                },
                pck = null,
                roomInfos = roomReservations

            }, Request.Url.Scheme + "://" + Request.Url.Authority + "/Transactions/UpdateFatora");

            if (url.Result == FawaterkReuslt.Success)
                return Redirect(url.RedirectUrl);
            else
            {
                ViewBag.EMessage1 = "Package Reservation Failed";
                ViewBag.EMessage2 = "Sorry, Something Went Wrong...Mail Us With The Proplem Please";
                return View("Error");
            }
        }

        public ActionResult PopularHotelsDetails(long id)
        {

            return null;
        }

        public ActionResult BookAPackage(BookingModel model)
        {
            var uow = new Fawaterk.Service.RepoServices.UnitOfWork(new FawaterksDbContext());
            var userUoW = new UnitOfWork(new ApplicationDbContext());
            var user = userUoW.users.Get(User.Identity.GetUserId<long>());
            var PCK = uow.packages.Get(model.PackageId);
            PCK.Units = model.PPLNum;
            var fatora = new FawaterkService();
            var url = fatora.FawaterkRequestSender(new Fawaterk.Service.Entities.InvoiceRequestModel
            {
                customerInfo = new Fawaterk.Service.Entities.CustomerInfo 
                {
                    UserId = user.Id,
                    Address = user.PhoneNumber,
                    Email = user.Email,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    Phone = user.PhoneNumber
                },
                pck = PCK,
                roomInfos = null
                
            }, Request.Url.Scheme + "://" + Request.Url.Authority + "/Transactions/UpdateFatora");

            if (url.Result == FawaterkReuslt.Success)
                return Redirect(url.RedirectUrl);
            else 
            {
                ViewBag.EMessage1 = "Package Reservation Failed";
                ViewBag.EMessage2 = "Sorry, Something Went Wrong...Mail Us With The Proplem Please";
                return View("Error");
            }
        }

        [AllowAnonymous]
        public ActionResult UpdateFatora(Fawaterk.Service.Entities.InvoiceStatusResponseEntity.Root invoiceStatus)
        {
            new FawaterkService().UpdateFatoraStatus(invoiceStatus);
            var uow = new Fawaterk.Service.RepoServices.UnitOfWork(new FawaterksDbContext());
            var fatora = uow.fawaterkTracking.Find(f => f.InvoiceId == invoiceStatus.InvoiceId).FirstOrDefault();
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/App_Data/MailTemplate/FatoraMail.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{ConfirmationLink}", fatora.Url).Replace("{UserName}", fatora.UserEmail).Replace("{MessageHeader}", "Travel Nexus Your Reservation Fatora Mail").Replace("{Message}", "Fatora").Replace("{btnName}", "Open Fatora");


            new SMTP2Go.Service.SMTP2GoService().SendEmail(new SendEmailDataModel
            {
                APIKey = SMTP2GoConfig.APIKey,
                Body = body,
                ReplyTo = SMTP2GoConfig.SenderEmail,
                Sender = SMTP2GoConfig.UserName,
                Subject = "Travel Nexus Confirmation Mail",
                To = fatora.UserEmail
            });
            return View(fatora);
        }

        public ActionResult WishList(string HotelId)
        {
            DOTWconnectService dotw = new DOTWconnectService();
            var hDetails = TempData["DOTWHotels"] as DOTWconnectHotelsViewModel;
            TempData["DOTWHotels"] = hDetails;
            var oneHotel = hDetails.SearchHotelsList.Result.Hotels.Hotel.Find(f => f.Hotelid.Equals(HotelId));
            var uId = User.Identity.GetUserId<long>();
            var uow = new Fawaterk.Service.RepoServices.UnitOfWork(new FawaterksDbContext());
            var wList = new UserHotelsWhishList()
            {
                CityId = oneHotel.CityCode,
                HotelId = HotelId,
                HotelAddress = oneHotel.Address,
                HotelImage = new SearchHotelsViewModel().CheckImagesAvailablity(oneHotel.Images.HotelImages),
                HotelName = oneHotel.HotelName,
                HotelRate = int.Parse(new SearchHotelsViewModel().GetRatings(oneHotel.Rating).ToString()),
                UserId = uId
            };

            uow.userHotelsWhishList.Add(wList);
            uow.Complete();

            return Json("done", JsonRequestBehavior.AllowGet);
        }


        public ActionResult CheckToggle(long rId)
        {
            var uow = new Fawaterk.Service.RepoServices.UnitOfWork(new FawaterksDbContext());
            var uRS = uow.userRoomsReservation.Get(rId);

            uRS.IsChecked = !uRS.IsChecked;

            uow.Complete();

            return Json("done", JsonRequestBehavior.AllowGet);
        }

        public ActionResult ConfirmBooking(long UserId)
        {
            var uow = new Fawaterk.Service.RepoServices.UnitOfWork(new FawaterksDbContext());
            var uRSs = uow.userRoomsReservation.Find(f => f.IsChecked == true && f.IsDeleted == false);

            if (uRSs != null)
                ViewBag.CMSG = "We will send you a mail with the invoice";
            else
                ViewBag.CMSG = "Sorry error happened";

            return View();
        }

        [AllowAnonymous]
        public ActionResult Packages()
        {
            var uow = new Fawaterk.Service.RepoServices.UnitOfWork(new FawaterksDbContext());
            var pcks = uow.packages.Find(p => (p.IsArchived == false && p.IsDeleted == false && p.OutOfDated == false)).ToList();
            return View(pcks);
        }

        [AllowAnonymous]
        public JsonResult CurrencyName(string CurrencyName)
        {
            //var cookie = Request.Cookies["userCurr"];
            //var currValue = cookie.Value;
            //var curr = new CurrenciesConfig();
            //curr.CurrentCurrency.CurrName = CurrencyName;
            //cookie.Value = CurrencyName;
            //return Json((curr[CurrencyName]/curr[currValue]), JsonRequestBehavior.AllowGet);
            return null;
        }
    }
}