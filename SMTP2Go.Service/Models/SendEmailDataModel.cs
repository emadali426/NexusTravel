using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTP2Go.Service.Models
{
    public class SendEmailDataModel
    {
        public string APIKey { get; set; }
        public string To { get; set; }
        public string Sender { get; set; }
        public string ReplyTo { get; set; }
        public string Subject { get; set; }
        public string Body { get; set; }
    }
}
