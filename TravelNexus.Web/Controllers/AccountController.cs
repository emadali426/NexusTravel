using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using DOTWconnect.Service.Entities;
using Fawaterk.Service;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using Newtonsoft.Json;
using SMTP2Go.Service.Models;
using TravelNexus.Web.Models;
using TravelNexus.Web.RepoServices;
using Facebook;

namespace TravelNexus.Web.Controllers
{
    
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        //private ActionResult RedirectToLocal(string returnUrl)
        //{
        //    if (Url.IsLocalUrl(returnUrl))
        //    {
        //        return Redirect(returnUrl);
        //    }
        //    return RedirectToAction("Index", "Home");
        //}

        //public AccountController()
        //    : this(ApplicationUserManager.Create(), ApplicationSignInManager.Create())
        //{

        //} 

        public string AppID { get; private set; }
        public string AppSecret { get; private set; }
        public AccountController()
        {
            AppID = ConfigurationManager.AppSettings["AppID"];
            AppSecret = ConfigurationManager.AppSettings["AppSecret"];
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInMang)
        {
            UserManager = userManager;
            SignInManager = signInMang;
            AppID = ConfigurationManager.AppSettings["AppID"];
            AppSecret = ConfigurationManager.AppSettings["AppSecret"];
        }

        //public ApplicationUserManager UserManager { get; private set; }
        //public ApplicationSignInManager SignInManager { get; private set; }

        public ApplicationSignInManager SignInManager
        {
            get { return HttpContext.GetOwinContext().Get<ApplicationSignInManager>(); }
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

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string ReturnUrl, string HotelId)
        {
            if (User.Identity.IsAuthenticated)
            {
                if (ReturnUrl.Contains("AdminManage"))
                {
                    if (User.IsInRole("ADMIN"))
                        return RedirectPermanent(ReturnUrl);
                    else
                        return RedirectPermanent("/home/index");
                }
                else
                {
                    return RedirectPermanent(ReturnUrl);
                }
            }
            //TempData["ReturnUrl"] = ReturnUrl;
            //TempData["HotelId"] = HotelId;
            return View();

        }



        //public async Task QuickRegister()
        //{
        //    RegisterViewModel model = new RegisterViewModel();
        //    model.Email = "admin";
        //    model.Password = "654321";
        //    model.ConfirmPassword = model.Password;
        //    var user = new ApplicationUser() { UserName = model.Email, Email = model.Email };
        //    var result = await UserManager.CreateAsync(user, model.Password);
        //    if (result.Succeeded)
        //    {
        //        //var admin = db.Customers.Find(1);
        //        //admin.Id = user.Id;
        //        //db.Complete();
        //        HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    }
        //}

        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        //[Route("Account/Login")]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            string ReturnUrl = TempData["ReturnUrl"] as string;
            string HotelId = TempData["HotelId"] as string;
            //if (!ModelState.IsValid)
            //{


            //    return RedirectToLocal($"{returnUrl}/#travelo-login");

            //}
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);
            var user = await UserManager.FindByEmailAsync(model.Email);



            switch (result)
            {
                case SignInStatus.Success:

                    if (!await UserManager.IsEmailConfirmedAsync(user.Id))
                    {
                        
                        SignOutHelper();
                        return Json(new
                        {
                            value = "<form action=\"/Account/ResendEmailConfirmationCode\" method=\"post\">" +
                                    $"<input type = \"hidden\" name = \"Id\" value = \"{user.Id}\" />" +
                                    $"<input type = \"hidden\" name = \"Email\" value = \"{user.Email}\" />" +
                                    "<h6>" +
                                        "You Have to Confirm Your Email.  <input type = \"submit\" value = \"Resend Confirmation Code\" />" +
                                    "</h6>" +
                                    "</form> ",
                            reload = false
                        }, JsonRequestBehavior.AllowGet);
                    }
                    else if (user.Type == CustomerType.PARTNER && user.IsApproved == false)
                    {
                        SignOutHelper();
                        return Json(new { value = "Please Wait For Admin Approval.", reload = false }, JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                       
                        if (string.IsNullOrEmpty(ReturnUrl))
                            return RedirectToAction("Index", "Home");

                        if (string.IsNullOrEmpty(HotelId))
                            return RedirectPermanent(ReturnUrl);

                        string[] redReturn = ReturnUrl.Split('/');

                        var hotelId = new HotelItem();
                        hotelId.ApiName = "Webbeds Api";
                        hotelId.HotelCode = HotelId;
                        hotelId.redirectUrl = true;

                        return RedirectToAction(redReturn[2], redReturn[1], hotelId);
                        
                    }

                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    //ModelState.AddModelError("", "Invalid Login Attempt.");
                    return Json(new { value = "Invalid Login Attempt.", reload = false }, JsonRequestBehavior.AllowGet);
            }
            
        }

        [AllowAnonymous]
        public async Task<ActionResult> ExLogin(LoginViewModel model)
        {
            var result = await SignInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, shouldLockout: false);

            string ReturnUrl = TempData["ReturnUrl"] as string;
            string HotelId = TempData["HotelId"] as string;

            switch (result)
            {
                case SignInStatus.Success:

                    if (string.IsNullOrEmpty(ReturnUrl))
                        return RedirectToAction("Index", "Home");

                    if (string.IsNullOrEmpty(HotelId))
                        return RedirectPermanent(ReturnUrl);

                    string[] redReturn = ReturnUrl.Split('/');

                    var hotelId = new HotelItem();
                    hotelId.ApiName = "Webbeds Api";
                    hotelId.HotelCode = HotelId;
                    hotelId.redirectUrl = true;

                    return RedirectToAction(redReturn[2], redReturn[1], hotelId);

                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    //ModelState.AddModelError("", "Invalid Login Attempt.");
                    return Json(new { value = "Invalid Login Attempt.", reload = false }, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        [AllowAnonymous]
        public async Task<ActionResult> ResendEmailConfirmationCode(RSECViewModel user)
        {
         
            
            UserManager.UserTokenProvider = tokenProvider(UserManager);
            string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
            var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
            string body = string.Empty;
            using (StreamReader reader = new StreamReader(Server.MapPath("~/App_Data/MailTemplate/AccountConfirmation.html")))
            {
                body = reader.ReadToEnd();
            }
            body = body.Replace("{ConfirmationLink}", callbackUrl).Replace("{UserName}", user.Email).Replace("{MessageHeader}", "Confirmation Mail").Replace("{Message}", "Confirming Your Email").Replace("{btnName}", "Confirm My Account");


                var status = new SMTP2Go.Service.SMTP2GoService().SendEmail(new SendEmailDataModel
                {
                    APIKey = SMTP2GoConfig.APIKey,
                    Body = body,
                    ReplyTo = SMTP2GoConfig.SenderEmail,
                    Sender = SMTP2GoConfig.UserName,
                    Subject = "Travel Nexus Confirmation Mail",
                    To = user.Email
                });

                if (status == false)
                {
                    ViewBag.EMessage2 = "Sorry, Something Wrong Happend.";
                    return View("Error");
                }
           
            return View();
        }


        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Require that the user has already logged in via username/password or external login
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // The following code protects for brute force attacks against the two factor codes. 
            // If a user enters incorrect codes for a specified amount of time then the user account 
            // will be locked out for a specified amount of time. 
            // You can configure the account lockout settings in IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent: model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Invalid code.");
                    return View(model);
            }
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult PartnerRegister(/*string ReturnUrl, string HotelId*/)
        {
            //TempData["ReturnUrlRegister"] = ReturnUrl;
            //TempData["HotelIdRegister"] = HotelId;
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> PartnerRegister(RegisterViewModel model)
        {
            //string ReturnUrl = TempData["ReturnUrlRegister"] as string;
            //string HotelId = TempData["HotelIdRegister"] as string;

            if (ModelState.IsValid)
            {
                //if (!model.Password.Equals(model.ConfirmPassword))
                //{
                //    ModelState.AddModelError("", "Password Does not Match......");
                //    return View(model);
                //}

                var User = await UserManager.FindByEmailAsync(model.Email);

                if (User != null)
                {
                    ModelState.AddModelError("", "Email Already Exist.. Please Try Anther Email");
                    return View(model);
                }

                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    IsApproved = false,
                    LockoutEnabled = false,
                    IsDeleted = false,
                    CompanyName = model.CompanyName,
                    TaxCard = model.TaxCard,
                    CommercialRegister = model.CommercialRegister,
                    Type = CustomerType.PARTNER,

                };
                try { 
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    UserManager.SetLockoutEnabled(user.Id, false);
                    await UserManager.AddToRoleAsync(user.Id, "PARTNER");
                    //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                    // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                    // Send an email with this link
                    UserManager.UserTokenProvider = tokenProvider(UserManager);
                    string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                    string body = string.Empty;
                    using (StreamReader reader = new StreamReader(Server.MapPath("~/App_Data/MailTemplate/AccountConfirmation.html")))
                    {
                        body = reader.ReadToEnd();
                    }
                    body = body.Replace("{ConfirmationLink}", callbackUrl).Replace("{UserName}", model.Email).Replace("{MessageHeader}", "Confirmation Mail").Replace("{Message}", "Confirming Your Email").Replace("{btnName}", "Confirm My Account");


                        var status = new SMTP2Go.Service.SMTP2GoService().SendEmail(new SendEmailDataModel
                        {
                            APIKey = SMTP2GoConfig.APIKey,
                            Body = body,
                            ReplyTo = SMTP2GoConfig.SenderEmail,
                            Sender = SMTP2GoConfig.UserName,
                            Subject = "Travel Nexus Confirmation Mail",
                            To = user.Email
                        });

                        if(status == false)
                        {
                            ViewBag.EMessage2 = "Sorry, Something Wrong Happend.";
                            return View("Error");
                        }

                        //if (UserManager.FindByEmail(user.Email).IsApproved) { 
                        //    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        //    return RedirectToAction("Index", "Home");
                        //}
                        //else
                        //    return RedirectToAction("Error", "Home");
                    ViewBag.EMessage1 = "Thanks For Your Registeration...";
                    ViewBag.EMessage2 = "Please Check Your Email To Confirm It...";
                    return View("Error");

                }
                }
                catch (Exception ex)
                {
                    ViewBag.EMessage1 = ex.Message;
                    ViewBag.EMessage2 = "Sorry, Something Wrong Happend in Server...Try To Login Later.";
                    return View("Error");
                }


            }

            //if (!string.IsNullOrEmpty(ReturnUrl))
            //{
            //    return Login(ReturnUrl, HotelId); 
            //}

            return View(model);
        }

        //
        // GET: /Account/Register
        [HttpGet]
        [AllowAnonymous]
        //[Route("Account/Register")]
        public ActionResult Register(/*string ReturnUrl, string HotelId*/)
        {
            //TempData["ReturnUrlRegister"] = ReturnUrl;
            //TempData["HotelIdRegister"] = HotelId;
            return View(new RegisterViewModel());
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            //string ReturnUrl = TempData["ReturnUrlRegister"] as string;
            //string HotelId = TempData["HotelIdRegister"] as string;

            if (ModelState.IsValid)
            {
                //if (!model.Password.Equals(model.ConfirmPassword))
                //{
                //    ModelState.AddModelError("", "Password Does not Match......");
                //    return View(model);
                //}

                var User = await UserManager.FindByEmailAsync(model.Email);

                if (User != null)
                {
                    ModelState.AddModelError("", "Email Already Exist.. Please Try Anther Email");
                    return View(model);
                }

                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    IsApproved = true,
                    LockoutEnabled = false,
                    IsDeleted = false,
                    NexusTax = 10.0,
                    Type = CustomerType.CUSTOMER,
                    
                };
                try { 
                var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        UserManager.SetLockoutEnabled(user.Id, false);
                        await UserManager.AddToRoleAsync(user.Id, "CUSTOMER");
                        //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                        // Send an email with this link
                        UserManager.UserTokenProvider = tokenProvider(UserManager);
                        string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        string body = string.Empty;
                        using (StreamReader reader = new StreamReader(Server.MapPath("~/App_Data/MailTemplate/AccountConfirmation.html")))
                        {
                            body = reader.ReadToEnd();
                        }
                        body = body.Replace("{ConfirmationLink}", callbackUrl).Replace("{UserName}", model.Email).Replace("{MessageHeader}", "Confirmation Mail").Replace("{Message}", "Confirming Your Email").Replace("{btnName}", "Confirm My Account");


                        var status = new SMTP2Go.Service.SMTP2GoService().SendEmail(new SendEmailDataModel
                        {
                            APIKey = SMTP2GoConfig.APIKey,
                            Body = body,
                            ReplyTo = SMTP2GoConfig.SenderEmail,
                            Sender = SMTP2GoConfig.UserName,
                            Subject = "Travel Nexus Confirmation Mail",
                            To = user.Email
                        });

                        if (status == false)
                        {
                            ViewBag.EMessage2 = "Sorry, Something Wrong Happend.";
                            return View("Error");
                        }

                        //if (UserManager.FindByEmail(user.Email).IsApproved) { 
                        //    await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        //    return RedirectToAction("Index", "Home");
                        //}
                        //else
                        //    return RedirectToAction("Error", "Home");
                        ViewBag.EMessage1 = "Thanks For Your Registeration...";
                        ViewBag.EMessage2 = "Please Check Your Email To Confirm It...";
                        return View("Error");
                    }
                }
                catch(Exception ex)
                {
                    ViewBag.EMessage1 = ex.Message;
                    ViewBag.EMessage2 = "Sorry, Something Wrong Happend in Server...Try To Login Later.";
                    return View("Error");
                }
               

            }

            //if (!string.IsNullOrEmpty(ReturnUrl))
            //{
            //    return Login(ReturnUrl, HotelId); 
            //}

            return View(model);
        }

        [AllowAnonymous]
        public async Task<bool> ExRegister(RegisterViewModel model)
        {
            //string ReturnUrl = TempData["ReturnUrlRegister"] as string;
            //string HotelId = TempData["HotelIdRegister"] as string;


                //if (!model.Password.Equals(model.ConfirmPassword))
                //{
                //    ModelState.AddModelError("", "Password Does not Match......");
                //    return View(model);
                //}

            var User = await UserManager.FindByEmailAsync(model.Email);

            if (User != null)
            {
                return false;
            }

            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
                IsApproved = true,
                LockoutEnabled = false,
                IsDeleted = false,
                NexusTax = 10.0,
                EmailConfirmed = true,
                Type = CustomerType.CUSTOMER,

            };
            try
            {
                var result = await UserManager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    return true;
                }
            }
            catch (Exception ex)
            {
                return false;
            }



            return false;
            //if (!string.IsNullOrEmpty(ReturnUrl))
            //{
            //    return Login(ReturnUrl, HotelId); 
            //}
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(long userId, string code)
        {
            var result = new IdentityResult();
            if (userId <= 0 || code == null)
            {
                return View("Error");
            }
            UserManager.UserTokenProvider = tokenProvider(UserManager);
            try
            {
                result = await UserManager.ConfirmEmailAsync(userId, code);
            } 
            catch 
            {
                
            }
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await UserManager.FindByNameAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // Don't reveal that the user does not exist or is not confirmed
                    return View("ForgotPasswordConfirmation");
                }

                // For more information on how to enable account confirmation and password reset please visit https://go.microsoft.com/fwlink/?LinkID=320771
                // Send an email with this link
                UserManager.UserTokenProvider = tokenProvider(UserManager);
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                //await UserManager.SendEmailAsync(user.Id, "Reset Password", "Please reset your password by clicking <a href=\"" + callbackUrl + "\">here</a>");
                string body = string.Empty;
                using (StreamReader reader = new StreamReader(Server.MapPath("~/App_Data/MailTemplate/AccountConfirmation.html")))
                {
                    body = reader.ReadToEnd();
                }
                body = body.Replace("{ConfirmationLink}", callbackUrl).Replace("{UserName}", model.Email).Replace("{MessageHeader}", "Reset Password").Replace("{Message}", "Resetting Your Password").Replace("{btnName}", "Reset Password");


                var status = new SMTP2Go.Service.SMTP2GoService().SendEmail(new SendEmailDataModel
                {
                    APIKey = SMTP2GoConfig.APIKey,
                    Body = body,
                    ReplyTo = SMTP2GoConfig.SenderEmail,
                    Sender = SMTP2GoConfig.UserName,
                    Subject = "Travel Nexus Reset Password",
                    To = user.Email
                });

                if (status == false)
                {
                    ViewBag.EMessage2 = "Sorry, Something Wrong Happend.";
                    return View("Error");
                }

                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(long userId, string code)
        {
            var test = new ResetPasswordViewModel
            {
                Code = code,
                Id = userId
            };
            return code == null ? View("Error") : View(test);
        }



        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            if (!model.Password.Equals(model.ConfirmPassword))
            {
                ModelState.AddModelError("", "Password Does not Match..");
                return View(model);
            }

            UserManager.UserTokenProvider = tokenProvider(UserManager);
            var user = await UserManager.FindByIdAsync(model.Id);
            if (user == null)
            {
                // Don't reveal that the user does not exist
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }



        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generate the token and send it
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Sign in the user with this external login provider if the user already has a login
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // If the user does not have an account, then prompt the user to create an account
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }


        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            //Session["Workaround"] = 0;
            //ControllerContext.HttpContext.Session.RemoveAll();
            //HttpContext.Session["RunSession"] = "1";
            // Request a redirect to the external login provider
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }






        #region Helpers
        // Used for XSRF protection when adding external logins
        //private const string XsrfKey = "XsrfId";

        //private IAuthenticationManager AuthenticationManager
        //{
        //    get
        //    {
        //        return HttpContext.GetOwinContext().Authentication;
        //    }
        //}

        //private void AddErrors(IdentityResult result)
        //{
        //    foreach (var error in result.Errors)
        //    {
        //        ModelState.AddModelError("", error);
        //    }
        //}



        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion




        [AllowAnonymous]
        public async Task<ActionResult> UserLogin(LoginViewModel model)
        {
            var Result = SignOut();
            var ExistUser = await UserManager.FindAsync(model.Email, model.Password);
            if (ExistUser != null)
            {
                await SignInAsync(ExistUser, model.RememberMe);

            }
            else
            {
                return Json(new { Result = false, Message = "Invalid User" });
            }
            return Json(new { Result = true, UserId = ExistUser.Id });
        }

        //
        // POST: /Account/Disassociate
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Disassociate(string loginProvider, string providerKey)
        {
            ManageMessageId? message = null;
            IdentityResult result = await UserManager.RemoveLoginAsync(0, new UserLoginInfo(loginProvider, providerKey));
            if (result.Succeeded)
            {
                message = ManageMessageId.RemoveLoginSuccess;
            }
            else
            {
                message = ManageMessageId.Error;
            }
            return RedirectToAction("Manage", new { Message = message });
        }

        // GET: /Account/Manage
        public ActionResult Manage(ManageMessageId? message)
        {
            ViewBag.StatusMessage =
                message == ManageMessageId.ChangePasswordSuccess ? "Your password has been changed."
                : message == ManageMessageId.SetPasswordSuccess ? "Your password has been set."
                : message == ManageMessageId.RemoveLoginSuccess ? "The external login was removed."
                : message == ManageMessageId.Error ? "An error has occurred."
                : "";
            ViewBag.HasLocalPassword = HasPassword();
            ViewBag.ReturnUrl = Url.Action("Manage");
            return View();
        }

        //
        // POST: /Account/Manage
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Manage(ManageUserViewModel model)
        {
            bool hasPassword = HasPassword();
            ViewBag.HasLocalPassword = hasPassword;
            ViewBag.ReturnUrl = Url.Action("Manage");
            if (hasPassword)
            {
                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.ChangePasswordAsync(/*User.Identity.GetUserId()*/0, model.OldPassword, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.ChangePasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }
            else
            {
                // User does not have a password so remove any validation errors caused by a missing OldPassword field
                ModelState state = ModelState["OldPassword"];
                if (state != null)
                {
                    state.Errors.Clear();
                }

                if (ModelState.IsValid)
                {
                    IdentityResult result = await UserManager.AddPasswordAsync(/*User.Identity.GetUserId()*/0, model.NewPassword);
                    if (result.Succeeded)
                    {
                        return RedirectToAction("Manage", new { Message = ManageMessageId.SetPasswordSuccess });
                    }
                    else
                    {
                        AddErrors(result);
                    }
                }
            }

            // If we got this far, something failed, redisplay form
            return View(model);
        }

        //
        // POST: /Account/ExternalLogin
        //[HttpPost]
        //[AllowAnonymous]
        //[ValidateAntiForgeryToken]
        //public ActionResult ExternalLogin(string provider, string returnUrl)
        //{
        //    // Request a redirect to the external login provider
        //    return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        //}

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId >= 0)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }


        //
        // POST: /Account/LinkLogin
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LinkLogin(string provider)
        {
            // Request a redirect to the external login provider to link a login for the current user
            return new ChallengeResult(provider, Url.Action("LinkLoginCallback", "Account"), User.Identity.GetUserId());
        }

        //
        // GET: /Account/LinkLoginCallback
        public async Task<ActionResult> LinkLoginCallback()
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync(XsrfKey, User.Identity.GetUserId());
            if (loginInfo == null)
            {
                return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
            }
            var result = await UserManager.AddLoginAsync(/*User.Identity.GetUserId()*/0, loginInfo.Login);
            if (result.Succeeded)
            {
                return RedirectToAction("Manage");
            }
            return RedirectToAction("Manage", new { Message = ManageMessageId.Error });
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Get the information about the user from the external login provider
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }


        #region Helpers
        // Used for XSRF protection when adding external logins
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private async Task SignInAsync(ApplicationUser user, bool isPersistent)
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ExternalCookie);
            var identity = await UserManager.CreateIdentityAsync(user, DefaultAuthenticationTypes.ApplicationCookie);
            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = isPersistent }, identity);
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private bool HasPassword()
        {
            var user = UserManager.FindById(User.Identity.GetUserId<long>());

            if (user != null)
            {
                return user.PasswordHash != null;
            }
            return false;

        }

        public enum ManageMessageId
        {
            ChangePasswordSuccess,
            SetPasswordSuccess,
            RemoveLoginSuccess,
            Error
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }


        [HttpGet]
        public ActionResult SignOut()
        {

            Session.Clear();
            ViewBag.UserId = "";
            ViewBag.Name = "";
            ViewBag.Authenticated = false;
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        public void SignOutHelper()
        {
            Session.Clear();
            ViewBag.UserId = "";
            ViewBag.Name = "";
            ViewBag.Authenticated = false;
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
        }

        private IUserTokenProvider<ApplicationUser, long> tokenProvider(ApplicationUserManager um)
        {
            var provider = new DpapiDataProtectionProvider("TravelNexusWebApp");
            return new DataProtectorTokenProvider<ApplicationUser, long>(provider.Create("TravelNexus"));
           
        }

        #endregion
        public ActionResult Delete()
        {
            string signed_request = Request.Form["signed_request"];

            if (!String.IsNullOrEmpty(signed_request))
            {
                string[] split = signed_request.Split('.');

                string signatureRaw = DecodeBase64(split[0]);
                string dataRaw = DecodeBase64(split[1]);

                // the decoded signature
                byte[] signature = Convert.FromBase64String(signatureRaw);

                byte[] dataBuffer = Convert.FromBase64String(dataRaw);

                // JSON object
                var json = Encoding.UTF8.GetString(dataBuffer);

                byte[] appSecretBytes = Encoding.UTF8.GetBytes("6f6ee8df82c2fa5faa75d7b8e9de33f8");
                System.Security.Cryptography.HMAC hmac = new System.Security.Cryptography.HMACSHA256(appSecretBytes);
                byte[] expectedHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(split[1]));
                if (!expectedHash.SequenceEqual(signature))
                {
                    throw new Exception("Invalid signature");
                }
                var fbUser = JsonConvert.DeserializeObject<FacebookUserDTO>(json);
                return Json(new { url = $"https://travel-nexus.com.com/fb/info/{fbUser.user_id}", confirmation_code = $"{fbUser.user_id}" });
            }
            return null;
        }

        public class FacebookUserDTO
        {
            public int user_id { get; set; }
        }

        public static string DecodeBase64(string value)
        {
            var valueBytes = System.Convert.FromBase64String(value);
            return Encoding.UTF8.GetString(valueBytes);
        }

        private Uri RedirectUri
        {
            get
            {
                var uriBuilder = new UriBuilder(Request.Headers["Referer"].ToString());
                uriBuilder.Query = null;
                uriBuilder.Fragment = null;
                uriBuilder.Path = Url.Action("FacebookCallback");
                return uriBuilder.Uri;
            }
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Facebook()
        {
            var fb = new FacebookClient();
            var loginurl = fb.GetLoginUrl(new
            {
                client_id = AppID,
                client_secret = AppSecret,
                redirect_uri = RedirectUri.AbsoluteUri,
                response_type = "code",
                scope = "email"
            });

            return Redirect(loginurl.AbsoluteUri);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult> FacebookCallback(string code)
        {
            var fb = new FacebookClient();
            dynamic result = fb.Post("oauth/access_token", new
            {
                client_id = AppID,
                client_secret = AppSecret,
                redirect_uri = RedirectUri.AbsoluteUri,
                code=code
            });
            var accesstoken = result.access_token;
            fb.AccessToken = accesstoken;
            dynamic data = fb.Get("me?fields=link,first_name,currency,last_name,email,gender,locale,timezone,verified,picture,age_range");

            var User = await UserManager.FindByEmailAsync(data.email);

            if (User != null)
            {
                var loginVM = new LoginViewModel
                {
                    Email = data.email,
                    Password = "Facebook@123!",
                    RememberMe = false
                };

                await ExLogin(loginVM);

                return RedirectToAction("Index", "Home");
            }

            if (data != null || data.email != null)
            {
                var registerVM = new RegisterViewModel
                {
                    Email = data.email,
                    Password = "Facebook@123!",
                    FirstName = data.first_name,
                    LastName = data.last_name,
                    PhoneNumber = "01023436359"
                };

                bool resReg = await ExRegister(registerVM);

                if (resReg)
                {
                    var loginVM2 = new LoginViewModel
                    {
                        Email = data.email,
                        Password = "Facebook@123!",
                        RememberMe = false
                    };

                    await ExLogin(loginVM2);

                    return RedirectToAction("Index", "Home");
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
