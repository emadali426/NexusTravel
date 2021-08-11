using SMTP2Go.Service.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTP2Go.Service
{
    public partial class SMTP2GoService
    {
        static SMTP2GoService()
        {
            new SMTP2GoConfig();
        }

    }
}
