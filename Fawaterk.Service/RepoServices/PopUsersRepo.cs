using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fawaterk.Service.RepoServices
{
    public class PopUsersRepo : Repo<PopUserModel, long>
    {
        public PopUsersRepo(FawaterksDbContext tctx) : base(tctx)
        {
        }
    }
}
