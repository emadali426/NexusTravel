using System;
using System.Data.Entity.Migrations;
using System.Security.Claims;
using System.Web;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Host.SystemWeb;
using Microsoft.Owin.Infrastructure;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.Cookies;
using Microsoft.Owin.Security.Facebook;
using Microsoft.Owin.Security.Google;
using Owin;
//using HelperPro.Models;
//using TravelNexus.Web;

namespace TravelNexus.Web
{
    public partial class Startup
    {
        // For more information on configuring authentication, please visit https://go.microsoft.com/fwlink/?LinkId=301864
        public void ConfigureAuth(IAppBuilder app)
        {
            // Configure the db context, user manager and signin manager to use a single instance per request
            
            app.CreatePerOwinContext(ApplicationDbContext.Create);
            app.CreatePerOwinContext<ApplicationUserManager>(ApplicationUserManager.Create);
            app.CreatePerOwinContext<ApplicationSignInManager>(ApplicationSignInManager.Create);

            app.SetDefaultSignInAsAuthenticationType(DefaultAuthenticationTypes.ExternalCookie);
            // Enable the application to use a cookie to store information for the signed in user
            // and to use a cookie to temporarily store information about a user logging in with a third party login provider
            // Configure the sign in cookie
            app.UseCookieAuthentication(new CookieAuthenticationOptions
            {
                AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
                LoginPath = new PathString("/Account/Login"),
                Provider = new CookieAuthenticationProvider
                {
                     //Enables the application to validate the security stamp when the user logs in.
                     //This is a security feature which is used when you change a password or add an external login to your account.
                    OnValidateIdentity = SecurityStampValidator.OnValidateIdentity<ApplicationUserManager, ApplicationUser, long>(
                        validateInterval: TimeSpan.FromMinutes(30), (m, u) => u.GenerateUserIdentityAsync(m), c => c.GetUserId<long>()
                        )
                },
                CookieManager = new SystemWebCookieManager()
            });
          

         
            app.UseExternalSignInCookie(DefaultAuthenticationTypes.ExternalCookie);

            // Enables the application to temporarily store user information when they are verifying the second factor in the two-factor authentication process.
            //app.UseTwoFactorSignInCookie(DefaultAuthenticationTypes.TwoFactorCookie, TimeSpan.FromMinutes(5));
            // Enables the application to remember the second login verification factor such as phone or email.
            // Once you check this option, your second step of verification during the login process will be remembered on the device where you logged in from.
            // This is similar to the RememberMe option when you log in.
            //app.UseTwoFactorRememberBrowserCookie(DefaultAuthenticationTypes.TwoFactorRememberBrowserCookie);

            // Uncomment the following lines to enable logging in with third party login providers
            //app.UseMicrosoftAccountAuthentication(
            //    clientId: "",
            //    clientSecret: "");

            //app.UseTwitterAuthentication(
            //   consumerKey: "",
            //   consumerSecret: "");

            //app.UseFacebookAuthentication(
            //   appId: "2003372809817564",
            //   appSecret: "6f6ee8df82c2fa5faa75d7b8e9de33f8"
            //   );


            app.UseFacebookAuthentication(new FacebookAuthenticationOptions() {
                AppId = "560661181773385",
                AppSecret = "59ab7631714cfec32f2aa654944b4576",
                UserInformationEndpoint = "https://graph.facebook.com/v11.0/me?fields=id,first_name,last_name,email,name",
                AuthorizationEndpoint = "https://www.facebook.com/v11.0/dialog/oauth",
                Provider = new FacebookAuthenticationProvider(),
                CookieManager = new SystemWebCookieManager(),
                Scope = { "email" }
            });

            app.UseGoogleAuthentication(new GoogleOAuth2AuthenticationOptions()
            {
                ClientId = "991017023383-74fb211uii3r6o3fuj0ubdl074qnifm3.apps.googleusercontent.com",
                ClientSecret = "1J6owCZCxPWQT6VJ-MjZdodw",
                Provider = new GoogleOAuth2AuthenticationProvider(),
                CookieManager = new SystemWebCookieManager(),
                Scope = { "email" }
            });
        }

        
    }
}