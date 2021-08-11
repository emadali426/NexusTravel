//using Microsoft.AspNet.Identity;
//using Microsoft.AspNet.Identity.EntityFramework;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Data.Entity.Migrations;
//using System.Data.Entity.Validation;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TravelNexus.Web.Controllers;
//using TravelNexus.Web.Models;

//namespace TravelNexus.Web
//{
//    public sealed class EntitiesContextInitializer
//    {
//        private ApplicationDbContext _ctx;
//        public EntitiesContextInitializer(ApplicationDbContext Ctx) 
//        {

//            _ctx = Ctx;

//        }

//        public bool AddAdmin() 
//        {
            


//            try
//            {

//                if (_ctx.Database.Exists() && _ctx.Users.Where(u => u.Id > 0) == null)
//                {
                    
//                    return true;
//                }
               
//            }
//            catch (Exception e)
//            {

//            }
//            return false;
//        }

//        private void CreateRoles()
//        {
//            IEnumerable<AppRole> roles = new List<AppRole>
//            {
//                new AppRole
//                {
//                    Name = "ADMIN"
//                },
//                new AppRole
//                {
//                    Name = "CUSTOMER"
//                },
//                new AppRole
//                {
//                    Name = "PARTNER"
//                }
//            };
//            _ctx.Set<AppRole>().AddRange(roles);
//        }

//        private ApplicationUser CreateAdmin()
//        {

//            return new ApplicationUser
//            {
//                FirstName = "ADMINISTRATOR",
//                UserName = "abdelrahmanali.work2021@gmail.com",
//                Email = "abdelrahmanali.work2021@gmail.com",
//                PasswordHash = "Admin@Nexus2021"
//            };
            

//        }
//    }

//}
