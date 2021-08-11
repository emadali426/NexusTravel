using Fawaterk.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fawaterk.Service.RepoServices
{
    public class PackagesRepo : Repo<Package, long>
    {
        public PackagesRepo(FawaterksDbContext tctx) : base(tctx)
        {

        }
    }
}