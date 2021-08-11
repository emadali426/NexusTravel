using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fawaterk.Service.RepoServices
{
    public class FavDestRepo : Repo<OurCustomerFavDestModel, long>
    {
        public FavDestRepo(FawaterksDbContext tctx) : base(tctx)
        {

        }
    }
}
