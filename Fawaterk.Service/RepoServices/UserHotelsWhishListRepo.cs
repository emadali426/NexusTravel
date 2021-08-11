using Fawaterk.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fawaterk.Service.RepoServices
{
    public class UserHotelsWhishListRepo : Repo<UserHotelsWhishList, long>
    {
        public UserHotelsWhishListRepo(FawaterksDbContext tctx) : base(tctx)
        {

        }

    }
}