﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fawaterk.Service.RepoServices
{
    public class PopHotelsRepo : Repo<PopHotelsModel, long>
    {
        public PopHotelsRepo(FawaterksDbContext tctx) : base(tctx)
        {

        }
    }
}
