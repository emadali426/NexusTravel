using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace TravelNexus.Web.RepoServices
{
    public class UserRepo : Repo<ApplicationUser, long>
    {
        public UserRepo(ApplicationDbContext tctx) : base(tctx)
        {

        }
    }
}