using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Web.Configuration;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Data.Common;
using System.Configuration;
using System.Data.Entity.Migrations;
using Microsoft.AspNet.Identity;


//using TravelNexus.Web.ORM;

namespace TravelNexus.Web
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, AppRole, long, ApplicationUserLogin, ApplicationUserRole, ApplicationUserClaim>, IDisposable
    {
        //private static ApplicationDbContext _ctx;
        static ApplicationDbContext()
        {
            Database.SetInitializer<ApplicationDbContext>(new IdentityDbInit());
        }
        public ApplicationDbContext()
          : base("NexusDBCustomerLoginConnection")
        {
            

            Database.Initialize(false);
        }

        //public virtual DbSet<Customer> Customers { get; set; }
        //public virtual DbSet<CustomerClaim> CustomersClaims { get; set; }
        //public virtual DbSet<CustomerLogin> CustomersLogins { get; set; }
        //public virtual DbSet<CustomerRole> CustomersRoles { get; set; }
        //public virtual DbSet<Role> AppRoles { get; set; }



        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            //Map to My Defined Tables.
            modelBuilder.Entity<ApplicationUser>().ToTable("Customers");
            modelBuilder.Entity<ApplicationUserRole>().ToTable("CustomerRoles");
            modelBuilder.Entity<AppRole>().ToTable("AppRoles");
            modelBuilder.Entity<ApplicationUserClaim>().ToTable("CustomersClaims");
            modelBuilder.Entity<ApplicationUserLogin>().ToTable("CustomersLogins");




        }

        

        public class IdentityDbInit : CreateDatabaseIfNotExists<ApplicationDbContext>
        {
            
            protected override void Seed(ApplicationDbContext context)
            {
                PerformInitialSetup(context); base.Seed(context);
            }
            public override void InitializeDatabase(ApplicationDbContext context)
            {
                base.InitializeDatabase(context);
                
            }
            private bool PerformInitialSetup(ApplicationDbContext ctx)
            {
                if (ctx.Database.Exists())
                {
                    CreateRoles(ctx);
                    var adminUser = CreateAdmin();
                    var userManager = new ApplicationUserManager(new ApplicationUserStore(ctx));
                    var result = userManager.Create(adminUser, adminUser.PasswordHash);
                    if (result.Succeeded)
                    {
                        userManager.AddToRole(adminUser.Id, "ADMIN");
                    }
                    else
                    {
                        throw new Exception("Error Result False");
                    }
                    // This will run Seed() method

                    
                }
                return false;
            }

            private void CreateRoles(ApplicationDbContext ctx)
            {
                IEnumerable<AppRole> roles = new List<AppRole>
            {
                new AppRole
                {
                    Name = "ADMIN"
                },
                new AppRole
                {
                    Name = "CUSTOMER"
                },
                new AppRole
                {
                    Name = "PARTNER"
                }
            };
                ctx.Set<AppRole>().AddRange(roles);
            }

            private ApplicationUser CreateAdmin()
            {

                return new ApplicationUser
                {
                    FirstName = "ADMINISTRATOR",
                    UserName = "admin@admin.com",
                    Email = "admin@admin.com",
                    PasswordHash = "Admin!@#123",
                    Type = CustomerType.ADMIN,
                    EmailConfirmed = true,
                };


            }


        }


       
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        

        public static long CurrentUserId { get; set; }
        
        
    }
   
}