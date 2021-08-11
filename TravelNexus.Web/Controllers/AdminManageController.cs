using Fawaterk.Service;
using Microsoft.AspNet.Identity.Owin;
using SMTP2Go.Service.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using TravelNexus.Web;
using TravelNexus.Web.Models;
using TravelNexus.Web.RepoServices;
using static TravelNexus.Web.Controllers.AccountController;

namespace TravelNexus.Web.Controllers
{

    [Authorize(Roles = "ADMIN")]
    public class AdminManageController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;
        private static string folderImagesPath = System.Web.HttpContext.Current.Server.MapPath(@"~\NexusImages\PackagesImages");
        private static string newsFolderImagesPath = System.Web.HttpContext.Current.Server.MapPath(@"~\NexusImages\NewsImages");
        private static string PackagesMainImagesPath = System.Web.HttpContext.Current.Server.MapPath(@"~\NexusImages\PackagesImages\PackagesMainImages");
        private static string ServiceImagesPath = System.Web.HttpContext.Current.Server.MapPath(@"~\NexusImages\ServiceImages");
        private static string PopHotelsImagesPath = System.Web.HttpContext.Current.Server.MapPath(@"~\NexusImages\PopularHotelsImages");
        private static string PopHotelsMainImagesPath = System.Web.HttpContext.Current.Server.MapPath(@"~\NexusImages\PopularHotelsImages\PopHotelsMainImages");
        private static string PopHotelsLogoImagesPath = System.Web.HttpContext.Current.Server.MapPath(@"~\NexusImages\PopularHotelsImages\PopHotelsLogoImages");
        private static string FavDestImagesPath = System.Web.HttpContext.Current.Server.MapPath(@"~\NexusImages\FavDestImages");

        public AdminManageController()
        {
        }

        public AdminManageController(ApplicationUserManager userManager, ApplicationSignInManager signInManager)
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set
            {
                _signInManager = value;
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }


        // GET: AdminManage
        public ActionResult Dashboard()
        {
            DashboardModel model = new DashboardModel();
            var uow = new Fawaterk.Service.RepoServices.UnitOfWork(new FawaterksDbContext());

            model.Partners = UserManager.Users.Where(u => u.Type == CustomerType.PARTNER && u.EmailConfirmed).ToList();
            model.Clients = UserManager.Users.Where(u => u.Type == CustomerType.CUSTOMER && u.EmailConfirmed).ToList();
            model.MailNewsUsers = new List<MailNewsModel>();
            model.OurServices = uow.ourServices.GetAll().ToList();
            model.nexusNews = uow.nexusNews.GetAll().ToList();
            //model.HotelRequests = uow.popUsers.GetAll().ToList();

            var db = new ApplicationDbContext();

            var popRequestsuser = uow.popUsers.GetAll().ToList();

            var popilarRequestVM = new List<PopUserModelVM>();

            foreach (var request in popRequestsuser)
            {
                var customer = db.Users.SingleOrDefault(u => u.Id == request.UserId);
                var hotelsRequest = request;
                var hotel = uow.popHotels.Find(h => h.Id == request.PopHotelsId).FirstOrDefault();

                popilarRequestVM.Add(new PopUserModelVM
                {
                    User = customer,
                    Hotel = hotel,
                    HotelRequests = hotelsRequest
                });
            }

            model.HotelRequests = popilarRequestVM;

            model.smtpModel = new SMTPViewModel
            {
                APIKey = SMTP2GoConfig.APIKey,
                EnableSSL = SMTP2GoConfig.EnableSSL,
                Host = SMTP2GoConfig.Host,
                Password = SMTP2GoConfig.Password,
                Port = SMTP2GoConfig.Port,
                SenderArabicName = SMTP2GoConfig.SenderArabicName,
                SenderEmail = SMTP2GoConfig.SenderEmail,
                SenderEnglishName = SMTP2GoConfig.SenderEnglishName,
                Signature = SMTP2GoConfig.Signature,
                TimeInterval = SMTP2GoConfig.TimeInterval,
                UserName = SMTP2GoConfig.UserName,

            };
            model.apisModel = new APIsViewModel
            {
                GoogleKey = APIsKeysConfig.GoogleKey,
                FawaterkKey = APIsKeysConfig.FawaterkKey
            };
            var pcks = uow.packages.GetAll().ToList();
            foreach (var pck in pcks)
            {
                pck.OutOfDated = pck.OfferEndDate.CompareTo(DateTime.Now) < 0;
            }
            model.pcksModel = pcks.FindAll(p => p.IsArchived == false && p.IsDeleted == false && p.OutOfDated == false);
            model.outPcksModel = pcks.FindAll(p => p.OutOfDated == true);
            model.deletedPcksModel = pcks.FindAll(p => p.IsDeleted == true);
            model.arcPcksModel = pcks.FindAll(p => p.IsArchived == true);
            model.popHotels = uow.popHotels.Find(h=>!h.IsDeleted).ToList();
            model.favDests = uow.favDest.GetAll().ToList();

            model.fawaters = uow.fawaterkTracking.GetAll().ToList();
            var result = new FawaterkService().UpdateFawaterksStatus(model.fawaters);

            uow.Complete();
            model.Currencies = CurrenciesConfig._currencies.ToList();
            return View(model);
        }
        [HttpPost]
        public ActionResult AddCurrency(Currency curr)
        {
            new CurrenciesConfig()[curr.CurrName] = curr.CurrValue;

            ViewBag.EMessage1 = curr.CurrName;
            ViewBag.EMessage2 = "Currency Has been Added";
            return null;
        }

        [HttpGet]
        public ActionResult AddService()
        {
            return View();
        }

        [HttpPost]
        public ActionResult AddService(ServiceModel model)
        {
            var uow = new Fawaterk.Service.RepoServices.UnitOfWork(new FawaterksDbContext());
            if (!Directory.Exists(ServiceImagesPath))
                Directory.CreateDirectory(ServiceImagesPath);

            Image mainImg = Image.FromStream(model.ServiceIamgeName.InputStream);
            mainImg.Save(ServiceImagesPath + $"\\{model.ServiceIamgeName.FileName}");
            mainImg.Dispose();
            mainImg = null;

            uow.ourServices.Add(new OurServiceModel
            {
                Description = model.Description,
                ImgName = $"/NexusImages/ServiceImages/{model.ServiceIamgeName.FileName}",
                ServiceName = model.ServiceTitle
            });

            uow.Complete();

            ViewBag.EMessage1 = model.ServiceTitle;
            ViewBag.EMessage2 = "Service Has been Added";
            return View("Error");
        }

        public ActionResult ServiceDelete(long id)
        {
            var uof = new Fawaterk.Service.RepoServices.UnitOfWork(new FawaterksDbContext());
            var pck = uof.ourServices.Get(id);
            uof.ourServices.Remove(pck);
            uof.Complete();
            ViewBag.EMessage1 = pck.ServiceName;
            ViewBag.EMessage2 = "Service Has been Deleted";
            return View("Error");
        }

        [HttpGet]
        public ActionResult SendSpecficMail(SendToEMail model)
        {
            return View(model);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult SendSpecficMail(SendMailViewModel model)
        {
            if(model == null)
            {
                ViewBag.EMessage2 = "Sorry Error Occurred";
                return View("Error");
            }

            new SMTP2Go.Service.SMTP2GoService().SendEmail(new SendEmailDataModel
            {
                APIKey = SMTP2GoConfig.APIKey,
                Body = model.MailBody,
                ReplyTo = SMTP2GoConfig.SenderEmail,
                Sender = SMTP2GoConfig.UserName,
                Subject = model.Subject,
                To = model.SendTo
            });

            ViewBag.EMessage1 = model.Subject;
            ViewBag.EMessage2 = "Mail Has been Sent to " + model.SendTo;
            return View("Error");
        }
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult MailNews(MailNewsViewModel Email)
        {
            ViewBag.EMessage2 = "Under Testing...";
            return View("Error");
        }

        public ActionResult EditInvoice(long id)
        {
            var uow = new Fawaterk.Service.RepoServices.UnitOfWork(new FawaterksDbContext());
            var model = uow.fawaterkTracking.Get(id);
            return View(model);
        }

        public ActionResult UpdateCountryVAT(string countryName, double VAT)
        {
            if (string.IsNullOrEmpty(countryName))
                return Json("Error", JsonRequestBehavior.AllowGet);

            try
            {

                CountryVATsConfig countryVATs = new CountryVATsConfig();

                countryVATs[countryName] = VAT;

            }
            catch (Exception ex)
            {
                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }


            return Json("done", JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult SMTPConfig(SMTPViewModel model)
        {
            if (model != null)
                SMTP2GoConfig.ChangeSMTPData(model);

            return View();
        }

        public ActionResult APIsConfig(APIsViewModel model)
        {
            if(model != null)
                APIsKeysConfig.UpdateAPIsFile(model);

            return View();
        }

        [HttpPost]
        public async Task<ActionResult> ApproveUser(ApproveUserModel user)
        {
            if (ModelState.IsValid) {

            var us = await UserManager.FindByIdAsync(user.Id);

                if (us != null)
                {
                    us.IsApproved = true;
                    us.TaxCard = user.TaxCard.ToString();
                    await UserManager.UpdateAsync(us);
                    return RedirectToAction("Dashboard", "AdminManage");
                }
                
                //return Json(new GeneralErrors { Result = result.Succeeded, Message = $" Admin Approved {user.Email} ....." }, JsonRequestBehavior.AllowGet);
           
            }

            ViewBag.EMessage1 = $"Approval/Change";
            ViewBag.EMessage2 = "Isn't Completed..Something Wrong Happens..";
            return View("Error");

        }

        [AllowAnonymous]
        public ActionResult PackageDetatils(long id)
        {
            var uof = new Fawaterk.Service.RepoServices.UnitOfWork(new FawaterksDbContext());
            var pck = uof.packages.Find(p=>p.Id == id && !p.IsDeleted).FirstOrDefault();
            return View(pck);
        }

        [AllowAnonymous]
        public ActionResult popHotelDetails(long id)
        {
            var uof = new Fawaterk.Service.RepoServices.UnitOfWork(new FawaterksDbContext());
            var pck = uof.popHotels.Find(h=>h.Id == id && !h.IsDeleted).FirstOrDefault();
            return View(pck);
        }

        public ActionResult PopUserRequest(PopUserViewModel model)
        {
            var uof = new Fawaterk.Service.RepoServices.UnitOfWork(new FawaterksDbContext());
            var popUser = new PopUserModel
            {
                UserId = model.UserId,
                CheckIn = model.CheckIn,
                CheckOut = model.CheckOut,
                NumOfRooms = model.NumOfRooms,
                NumOfAdults = model.NumOfAdults,
                NumOfKids = model.NumOfKids,
                PopHotelsId = model.PopHotelsId,
                MPhone = model.MPhone
            };
            uof.popUsers.Add(popUser);
            uof.Complete();

            ViewBag.EMessage2 = "Thanks, For your Request";
            return View("Error");
        }

        public ActionResult PackageArchive(long id)
        {
            var uof = new Fawaterk.Service.RepoServices.UnitOfWork(new FawaterksDbContext());
            uof.packages.Get(id).IsArchived = true;
            uof.Complete();
            return RedirectToAction("Dashboard");
        }

        [HttpGet]
        public ActionResult PackageEdit(long id)
        {
            var uof = new Fawaterk.Service.RepoServices.UnitOfWork(new FawaterksDbContext());
            var pck = uof.packages.Get(id);
            return View(pck);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult PackageEdit(PackageViewModel model)
        {
            var uof = new Fawaterk.Service.RepoServices.UnitOfWork(new FawaterksDbContext());
            var pck = uof.packages.Get(model.Id);
            pck.OfferEndDate = model.OfferEndDate;
            pck.OfferStartDate = model.OfferStartDate;
            pck.OutOfDated = false;
            pck.IsArchived = false;
            pck.IsDeleted = false;
            pck.PackageStartDate = model.PackageStartDate;
            pck.PackageEndDate = model.PackageEndDate;
            pck.Title = model.Title;
            pck.Price = model.Price;

            if (model.ImgsFiles != null && model.ImgsFiles[0] != null)
            {
                var counter = pck.ImagesCount;
                var PackfolderPath = pck.FolderImgsName;
                PackfolderPath.Replace("/", @"\");
                PackfolderPath = System.Web.HttpContext.Current.Server.MapPath($@"~{PackfolderPath}");
                List<string> fileNames = new List<string>();
                foreach (var fileName in model.ImgsFiles.ToList())
                {
                    Image img = Image.FromStream(fileName.InputStream);
                    counter++;
                    img.Save(PackfolderPath + $"\\{ counter}.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    img.Dispose();
                    img = null;
                }
                pck.ImagesCount = counter;
            }
            pck.PackageDescription = model.PackageDescription;
            uof.Complete();
            ViewBag.EMessage1 = $"Package {model.Title}";
            ViewBag.EMessage2 = "Has been Edited";
            return View("Error");
        }

        public ActionResult PackageDelete(long id)
        {
            var uof = new Fawaterk.Service.RepoServices.UnitOfWork(new FawaterksDbContext());
            var pck = uof.packages.Get(id);
            uof.packages.Remove(pck);
            uof.Complete();
            ViewBag.EMessage1 = pck.Title;
            ViewBag.EMessage2 = "Package Has been Deleted";
            return View("Error");
        }

        public ActionResult PopHotelDelete(long id)
        {
            var uof = new Fawaterk.Service.RepoServices.UnitOfWork(new FawaterksDbContext());
            var pck = uof.popHotels.Get(id);
            //uof.popHotels.Remove(pck);
            pck.IsDeleted = true;
            uof.Complete();
            ViewBag.EMessage1 = pck.HotelName;
            ViewBag.EMessage2 = "Popular Hotel Has been Deleted";
            return View("Error");
        }

        public ActionResult FavDestDelete(long id)
        {
            var uof = new Fawaterk.Service.RepoServices.UnitOfWork(new FawaterksDbContext());
            var pck = uof.favDest.Get(id);
            uof.favDest.Remove(pck);
            uof.Complete();
            ViewBag.EMessage1 = pck.DestinationName;
            ViewBag.EMessage2 = "Favourite Destination Has been Deleted";
            return View("Error");
        }

        public ActionResult NewsDelete(long id)
        {
            var uof = new Fawaterk.Service.RepoServices.UnitOfWork(new FawaterksDbContext());
            var news = uof.nexusNews.Get(id);
            uof.nexusNews.Remove(news);
            uof.Complete();
            ViewBag.EMessage1 = news.Title;
            ViewBag.EMessage2 = "News Has been Deleted";
            return View("Error");
        }

        [HttpGet]
        public ActionResult AddPackage()
        {

            return View(new PackageViewModel { OfferStartDate = DateTime.Now, OfferEndDate = DateTime.Now.AddDays(1), PackageStartDate = DateTime.Now.AddDays(2), PackageEndDate = DateTime.Now.AddDays(3) });
        }
        [HttpGet]
        public ActionResult AddNews()
        {

            return View();
        }

        [HttpGet]
        public ActionResult AddPopularHotel()
        {
            var uof = new Fawaterk.Service.RepoServices.UnitOfWork(new FawaterksDbContext());

            var popHotelNum = uof.popHotels.Count(h => !h.IsDeleted);
            if (popHotelNum >= 4)
            {
                ViewBag.EMessage1 = $"Sorry";
                ViewBag.EMessage2 = "You can't add more than 4 Hotels.";
                return View("Error");
            }

            return View();
        }

        public ActionResult AddFavDest()
        {

            return View();
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddPackage(PackageViewModel model)
        {

            string name = string.Empty;
            string HtmlViewFileName = string.Empty;
            string[] names = model.Title.Split(' ');
            for(int i = 0; i < 4; i++)
            {
                if (i < names.Length)
                    name += names[i];
                else
                    break;
            }
            
            if (name.Length > 22)
                name = name.Remove(23);

            if (!Directory.Exists(folderImagesPath))
                Directory.CreateDirectory(folderImagesPath);

            if (!Directory.Exists(PackagesMainImagesPath))
                Directory.CreateDirectory(PackagesMainImagesPath);

            int counter = 0;
            string PackfolderPath = folderImagesPath + $@"\{name}";


            if (!Directory.Exists(PackfolderPath))
                Directory.CreateDirectory(PackfolderPath);
            
            Image mainImg = Image.FromStream(model.MainIamgeName.InputStream);
            mainImg.Save(PackagesMainImagesPath + $"\\{model.MainIamgeName.FileName}");
            mainImg.Dispose();
            mainImg = null;

            if (model.ImgsFiles != null)
            {
                
                List<string> fileNames = new List<string>();
                foreach (var fileName in model.ImgsFiles.ToList())
                {
                        Image img = Image.FromStream(fileName.InputStream);
                        counter++;
                        img.Save(PackfolderPath + $"\\{counter}.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
                        img.Dispose();
                        img = null;
                }
            }

            if (counter == 0)
            {
                ModelState.AddModelError("", "Images Size Doesn't meet Aspect Ratio Restriction.");
                return View(model);
            }

            //if (model.DescriptionFileName != null)
            //{
                
            //    using (StreamReader sr = new StreamReader(model.DescriptionFileName.InputStream))
            //    {
            //        using (StreamWriter sw = new StreamWriter(PackfolderPath + $"\\{model.DescriptionFileName.FileName}"))
            //        {
            //            sw.Write(sr.ReadToEnd());
            //            HtmlViewFileName = name + $"\\{model.DescriptionFileName.FileName}";
            //        }
            //    }
            //}

            //model.DescriptionFileName.SaveAs(folderHtmlPath + $@"\{name}");

            PackfolderPath = $"/NexusImages/PackagesImages/{name}";
            var uof = new Fawaterk.Service.RepoServices.UnitOfWork(new FawaterksDbContext());
            uof.packages.Add(new Package
            {
                Title = model.Title,
                Height = model.Height,
                PackageDescription = model.PackageDescription,
                FolderImgsName = PackfolderPath,
                MainImageName = $"/NexusImages/PackagesImages/PackagesMainImages/{model.MainIamgeName.FileName}",
                ImagesCount = counter,
                IsArchived = false,
                IsDeleted = false,
                OutOfDated = false,
                OfferEndDate = model.OfferEndDate,
                OfferStartDate = model.OfferStartDate,
                PackageEndDate = model.PackageEndDate,
                PackageStartDate = model.PackageStartDate,
                Price = model.Price,
                Width = model.Width
            });
            uof.Complete();

            ViewBag.EMessage1 = $"Package {model.Title}";
            ViewBag.EMessage2 = "Has been Added to Your Packges.";
            return View("Error");
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddNews(NewsViewModel model)
        {

            if (!Directory.Exists(newsFolderImagesPath))
                Directory.CreateDirectory(newsFolderImagesPath);

            Image mainImg = Image.FromStream(model.FolderImgsName.InputStream);
            mainImg.Save(newsFolderImagesPath + $"\\{model.FolderImgsName.FileName}");
            mainImg.Dispose();
            mainImg = null;

            var uof = new Fawaterk.Service.RepoServices.UnitOfWork(new FawaterksDbContext());
            uof.nexusNews.Add(new NewsModel
            {
                Title = model.Title,

                FolderImgsName = $"/NexusImages/NewsImages/{model.FolderImgsName.FileName}",
                DescriptionFileName = model.DescriptionFileName,
                Height = model.Height,
                Width = model.Width
            });
            uof.Complete();

            ViewBag.EMessage1 = $"News {model.Title}";
            ViewBag.EMessage2 = "Has been Added to Your News.";
            return View("Error");
        }

        [HttpPost]
        public ActionResult AddFavDest(FavDestViewModel model)
        {

            if (!Directory.Exists(FavDestImagesPath))
                Directory.CreateDirectory(FavDestImagesPath);

            Image mainImg = Image.FromStream(model.ImgName.InputStream);
            mainImg.Save(FavDestImagesPath + $"\\{model.ImgName.FileName}");
            mainImg.Dispose();
            mainImg = null;

            var uof = new Fawaterk.Service.RepoServices.UnitOfWork(new FawaterksDbContext());
            uof.favDest.Add(new OurCustomerFavDestModel
            {
                DestinationName = model.DestinationName,
                Description = model.Description,
                ImgName = $"/NexusImages/FavDestImages/{model.ImgName.FileName}",
                Height = model.Height,
                Width = model.Width
            });
            uof.Complete();

            ViewBag.EMessage1 = $"Favourite Destination {model.DestinationName}";
            ViewBag.EMessage2 = "Has been Added to Your Favourite Destination";
            return View("Error");
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult AddPopularHotel(PopularHotelsViewModel model)
        {
            var uof = new Fawaterk.Service.RepoServices.UnitOfWork(new FawaterksDbContext());

            string name = string.Empty;
            string[] names = model.HotelName.Split(' ');
            for (int i = 0; i < 4; i++)
            {
                if (i < names.Length)
                    name += names[i];
                else
                    break;
            }

            if (name.Length > 22)
                name = name.Remove(23);

            if (!Directory.Exists(PopHotelsImagesPath))
                Directory.CreateDirectory(PopHotelsImagesPath);

            if (!Directory.Exists(PopHotelsMainImagesPath))
                Directory.CreateDirectory(PopHotelsMainImagesPath);

            if (!Directory.Exists(PopHotelsLogoImagesPath))
                Directory.CreateDirectory(PopHotelsLogoImagesPath);

            int counter = 0;
            string PopHotelfolderPath = PopHotelsImagesPath + $@"\{name}";


            if (!Directory.Exists(PopHotelfolderPath))
                Directory.CreateDirectory(PopHotelfolderPath);

            Image mainImg = Image.FromStream(model.MainIamgeName.InputStream);
            mainImg.Save(PopHotelsMainImagesPath + $"\\{model.MainIamgeName.FileName}");
            mainImg.Dispose();
            mainImg = null;

            Image logoImg = Image.FromStream(model.logoIamgeName.InputStream);
            logoImg.Save(PopHotelsLogoImagesPath + $"\\{model.logoIamgeName.FileName}");
            logoImg.Dispose();
            logoImg = null;

            if (model.ImgsFiles != null)
            {

                List<string> fileNames = new List<string>();
                foreach (var fileName in model.ImgsFiles.ToList())
                {
                    Image img = Image.FromStream(fileName.InputStream);
                    counter++;
                    img.Save(PopHotelfolderPath + $"\\{counter}.jpeg", System.Drawing.Imaging.ImageFormat.Jpeg);
                    img.Dispose();
                    img = null;
                }
            }

            //if (counter == 0)
            //{
            //    ModelState.AddModelError("", "Images Size Doesn't meet Aspect Ratio Restriction.");
            //    return View(model);
            //}

            //if (model.DescriptionFileName != null)
            //{

            //    using (StreamReader sr = new StreamReader(model.DescriptionFileName.InputStream))
            //    {
            //        using (StreamWriter sw = new StreamWriter(PackfolderPath + $"\\{model.DescriptionFileName.FileName}"))
            //        {
            //            sw.Write(sr.ReadToEnd());
            //            HtmlViewFileName = name + $"\\{model.DescriptionFileName.FileName}";
            //        }
            //    }
            //}

            //model.DescriptionFileName.SaveAs(folderHtmlPath + $@"\{name}");

            PopHotelfolderPath = $"/NexusImages/PopularHotelsImages/{name}";
            uof.popHotels.Add(new PopHotelsModel
            {
                HotelName = model.HotelName,
                Height = model.Height,
                PopHotelsDescription = model.PopHotelsDescription,
                FolderImgsName = PopHotelfolderPath,
                MainIamgeName = $"/NexusImages/PopularHotelsImages/PopHotelsMainImages/{model.MainIamgeName.FileName}",
                logoIamgeName = $"/NexusImages/PopularHotelsImages/PopHotelsLogoImages/{model.logoIamgeName.FileName}",
                ImagesCount = counter,
                Width = model.Width,
                AvgPrice = model.AvgPrice
            });
            uof.Complete();

            ViewBag.EMessage1 = $"Popular Hotel {model.HotelName}";
            ViewBag.EMessage2 = "Has been Added to Your Popular Hotels.";
            return View("Error");
        }

        private bool isAspectRatioValid(double r1, double r2)
        {
            if (Math.Abs(r1 - r2) < 0.01)
                return true;
            else
                return false;
        }

    }
}