using Fawaterk.Service;
using Fawaterk.Service.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fawaterk.Service.RepoServices
{
    public class FawaterkTrackingModelRepo : Repo<FawaterkTrackingModel, long>
    {
        public FawaterkTrackingModelRepo(FawaterksDbContext fctx) : base(fctx)
        {

        }


    }
}