using Newtonsoft.Json;
using SMTP2Go.Service.Models;
using SMTP2Go.Service.Models.JSON;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace SMTP2Go.Service
{
    public partial class SMTP2GoService
    {
        public bool SendEmail(SendEmailDataModel model)
        {
            HttpResponseMessage response;
            var reqObj = new SendEmailRequestDataModel
            {
                ApiKey = model.APIKey,
                CustomHeaders = new List<CustomHeader>
                {
                    new CustomHeader
                    {
                        Header = "Reply-To",
                        Value = model.ReplyTo
                    }
                },
                HtmlBody = model.Body,
                Sender = model.Sender,
                Subject = model.Subject,
                To = new List<string>
                {
                    model.To
                }
            };

            var req = JsonConvert.SerializeObject(reqObj);
            using (HttpClient client = new HttpClient())
            {
                response = client.PostAsync("https://api.smtp2go.com/v3/email/send", new StringContent(req, Encoding.UTF8, "application/json")).Result;
            }
            var Res = response.Content.ReadAsAsync<SendEmailResponseDataModel>().Result;

            return Res.Data.Succeeded == 1;
        }

    }
}
