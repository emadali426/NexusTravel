//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using System.Threading;
//using System.Web;
//using System.Web.Mvc;
//using Microsoft.AspNet.Identity;
//using System.Web.Configuration;
//using Autofac.Integration.Mvc;
//using Autofac;
//using TravelNexus.Web.Models;
//using TravelNexus.Web.RepoServices;
////using ERP_Plus.Filters;




//namespace TravelNexus.Web
//{
   
//    public class BaseController : Controller
//    {
//        ////
//        // GET: /Base/

//        //public ApplicationDbContext db = new ApplicationDbContext();
//        public UnitOfWork db = new UnitOfWork(new ApplicationDbContext());
//        public string Direction;
//        public string UserId;
//        public static bool ExistCurrentAction = false;
        
//        public BaseController()
//        {
           
//        }
//        protected override IAsyncResult BeginExecuteCore(AsyncCallback callback, object state)
//        {
//            if (Request.Cookies["Direction"] == null)
//            {
//                Response.Cookies.Set(new HttpCookie("Language", "English"));
//                Response.Cookies.Set(new HttpCookie("Direction", "ltr")); Response.Cookies.Set(new HttpCookie("culture", "en-US"));
//                Thread.CurrentThread.CurrentUICulture = new CultureInfo("en-US");
//            }
          
//            Direction = Request.Cookies["Direction"] == null ? "rtl" : Request.Cookies["Direction"].Value;
//            return base.BeginExecuteCore(callback, state);
//        }

      
        
//        public ActionResult GetClientName(long UserId)
//        {
//            var TblClients = db.Customers.FindCustomer(s => !s.IsDeleted && s.UserId == UserId);
//            var clientName = TblClients == null ? "" : TblClients.AppUser.UserName;
//            return Json(new { ClientName = clientName});
//        }
//        public int GetCurrentUserId()
//        {
//            var UserId = User.Identity.GetUserId<long>();
//            var _User = db.Customers.FindCustomer(s => s.UserId == UserId);
//            if (_User != null)
//                return (int)_User.Id;
//            else
//                return 0;
//        }
//        protected override IAsyncResult BeginExecute(System.Web.Routing.RequestContext requestContext, AsyncCallback callback, object state)
//        {
            
//            return base.BeginExecute(requestContext, callback, state);
//        }
//        public static byte[] ConevertToBytes(System.IO.Stream stream)
//        {
//            long originalPosition = 0;

//            if (stream.CanSeek)
//            {
//                originalPosition = stream.Position;
//                stream.Position = 0;
//            }

//            try
//            {
//                byte[] readBuffer = new byte[4096];

//                int totalBytesRead = 0;
//                int bytesRead;

//                while ((bytesRead = stream.Read(readBuffer, totalBytesRead, readBuffer.Length - totalBytesRead)) > 0)
//                {
//                    totalBytesRead += bytesRead;

//                    if (totalBytesRead == readBuffer.Length)
//                    {
//                        int nextByte = stream.ReadByte();
//                        if (nextByte != -1)
//                        {
//                            byte[] temp = new byte[readBuffer.Length * 2];
//                            Buffer.BlockCopy(readBuffer, 0, temp, 0, readBuffer.Length);
//                            Buffer.SetByte(temp, totalBytesRead, (byte)nextByte);
//                            readBuffer = temp;
//                            totalBytesRead++;
//                        }
//                    }
//                }

//                byte[] buffer = readBuffer;
//                if (readBuffer.Length != totalBytesRead)
//                {
//                    buffer = new byte[totalBytesRead];
//                    Buffer.BlockCopy(readBuffer, 0, buffer, 0, totalBytesRead);
//                }
//                return buffer;
//            }
//            finally
//            {
//                if (stream.CanSeek)
//                {
//                    stream.Position = originalPosition;
//                }
//            }
//        }
       
//	}
//    public class UserData
//    {
//        public string UserId { get; set; }
//        public int EmpId { get; set; }
//        public int Privilege { get; set; }
//        public int? UnitId { get; set; }
//        public int? DeptId { get; set; }
//        public string  EmpName { get; set; }
     
//    }

//}