using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fawaterk.Service.RepoServices
{
    public class OurServiceModelRepo : Repo<OurServiceModel, long>
    {
        public OurServiceModelRepo(FawaterksDbContext fctx) : base(fctx)
        {

        }
    }
}
