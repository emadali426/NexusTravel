using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataProtection;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using TravelNexus.Web.Controllers;

namespace TravelNexus.Web
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    
    public class ApplicationUser : IdentityUser<long, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    {
        public ApplicationUser()
        {
            IsApproved = true;
            IsDeleted = false;
            EmailConfirmed = false;
            LockoutEnabled = false;
            TwoFactorEnabled = false;
            PhoneNumberConfirmed = true;
            CreationDate = DateTime.Now;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser, long> manager)
        {
            
            // Note the authenticationType must match the one defined in 
            //   CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this,
        DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CompanyName { get; set; }
        public bool IsApproved { get; set; }
        
        public bool IsDeleted { get; set; }
        public DateTime CreationDate { get; set; }
       
        //public string PhoneNumber { get; set; }
        public string CommercialRegister { get; set; }
        public string TaxCard { get; set; }
        public double NexusTax { get; set; }
        public CustomerType Type { get; set; }
    }


    
    public class ApplicationUserRole : IdentityUserRole<long>
    {

    }

    
    public class AppRole : IdentityRole<long, ApplicationUserRole>
    {

    }

    
    public class ApplicationUserClaim : IdentityUserClaim<long>
    {

    }

    
    public class ApplicationUserLogin : IdentityUserLogin<long>
    {

    }

    //public class ApplicationUserManager : UserManager<ApplicationUser, long>
    //{
        

    //    //public static ApplicationUserManager Create { get  {  return new ApplicationUserManager(new ApplicationUserStore(new ApplicationDbContext())); }

         
    //    public ApplicationUserManager(ApplicationUserStore userStore) : base(userStore)
    //    {
    //        PasswordHasher = new MyPasswordHasher();
    //    }

    //    public static ApplicationUserManager Create()
    //    {
    //        return new ApplicationUserManager(new ApplicationUserStore(ApplicationDbContext.Create()));
    //    }

    //}

    //public class ApplicationUserStore : UserStore<ApplicationUser, AppRole, long, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>
    //{
    //    public ApplicationUserStore(ApplicationDbContext ctx) : base(ctx)
    //    {

    //    }
        
    //}

    //public class MyPhoneNumberTokenProvider<TUser> : PhoneNumberTokenProvider<TUser, long> where TUser : class, IUser<long>
    //{

    //}

    //public class MyEmailTokenProvider<TUser> : EmailTokenProvider<TUser, long> where TUser : class, IUser<long>
    //{

    //}



    //public class ApplicationSignInManager : SignInManager<ApplicationUser, long>
    //{
    //    public ApplicationSignInManager(ApplicationUserManager manager ,IAuthenticationManager manageAuth) : base(manager, manageAuth)
    //    {

        //    }

        //    public static ApplicationSignInManager Create()
        //    {
        //        return new ApplicationSignInManager(ApplicationUserManager.Create(), new ApplicationAuthManager());
        //    }

        //}

        //public class ApplicationAuthManager : IAuthenticationManager
        //{
        //    public ClaimsPrincipal User { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //    public AuthenticationResponseChallenge AuthenticationResponseChallenge { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //    public AuthenticationResponseGrant AuthenticationResponseGrant { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        //    public AuthenticationResponseRevoke AuthenticationResponseRevoke { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        //    public Task<AuthenticateResult> AuthenticateAsync(string authenticationType)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public Task<IEnumerable<AuthenticateResult>> AuthenticateAsync(string[] authenticationTypes)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public void Challenge(AuthenticationProperties properties, params string[] authenticationTypes)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public void Challenge(params string[] authenticationTypes)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public IEnumerable<AuthenticationDescription> GetAuthenticationTypes()
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public IEnumerable<AuthenticationDescription> GetAuthenticationTypes(Func<AuthenticationDescription, bool> predicate)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public void SignIn(AuthenticationProperties properties, params ClaimsIdentity[] identities)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public void SignIn(params ClaimsIdentity[] identities)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public void SignOut(AuthenticationProperties properties, params string[] authenticationTypes)
        //    {
        //        throw new NotImplementedException();
        //    }

        //    public void SignOut(params string[] authenticationTypes)
        //    {
        //        throw new NotImplementedException();
        //    }
        //}

    public class MyPasswordHasher : PasswordHasher
    {
        public override PasswordVerificationResult VerifyHashedPassword(string hashedPassword, string providedPassword)
        {
            if (hashedPassword.Equals(providedPassword))
            {
                return PasswordVerificationResult.Success;
            }
            return PasswordVerificationResult.Failed;
        }

        public override string HashPassword(string password)
        {
            return (password);
        }
    }

}