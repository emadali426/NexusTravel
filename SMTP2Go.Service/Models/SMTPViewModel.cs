using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMTP2Go.Service.Models
{
    public class SMTPViewModel
    {
        public string APIKey { get; set; }
        public string Host { get; set; }
        public int Port { get; set; }
        public string SenderArabicName { get; set; }
        public string SenderEnglishName { get; set; }
        public string SenderEmail { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Signature { get; set; }
        public string TimeInterval { get; set; }
        public bool EnableSSL { get; set; }
    }
}
