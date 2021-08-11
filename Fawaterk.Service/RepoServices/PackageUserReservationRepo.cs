using Fawaterk.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fawaterk.Service.RepoServices
{
    public class PackageUserReservationRepo : Repo<PackageUserReservation, long>
    {
        public PackageUserReservationRepo(FawaterksDbContext tctx) : base(tctx)
        {

        }
    }
}