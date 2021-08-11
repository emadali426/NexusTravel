using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fawaterk.Service.RepoServices
{
    public class NewsRepo : Repo<NewsModel, long>
    {
        public NewsRepo(FawaterksDbContext tctx) : base(tctx)
        {

        }
    }
}
