using Fawaterk.Service.Entities;
using Fawaterk.Service.Entities.CreateInvoiceRequestEntity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;


namespace Fawaterk.Service
{
    public partial class FawaterkService
    {
        public FawaterkReturn FawaterkRequestSender(InvoiceRequestModel model, string redirectUrl)
        {
            if (model.pck != null)
                return FawaterkPackRequestSender(model, redirectUrl);
            else if (model.roomInfos != null)
                return FawaterkRoomsRequestSender(model, redirectUrl);
            else
                return new FawaterkReturn { RedirectUrl= null, Result=FawaterkReuslt.Error};
            
        }

        public FawaterkReuslt UpdateFawaterksStatus(List<FawaterkTrackingModel> fawters)
        {
            if(fawters != null)
            { 
                foreach(var fatora in fawters)
                {
                    var invoiceStatusRes = FawaterkStatusRequestSender(fatora.InvoiceId);
                    fatora.PaymentMethod = invoiceStatusRes.PaymentMethod;
                    fatora.PaymentStatus = invoiceStatusRes.PaymentStatus;
                    fatora.Total = invoiceStatusRes.Total;
                }
                return FawaterkReuslt.Success;
            }
            return FawaterkReuslt.Error;
        }

        public FawaterkReuslt UpdateFatoraStatus(Entities.InvoiceStatusResponseEntity.Root invoiceStatus)
        {
            var uow = new RepoServices.UnitOfWork(new FawaterksDbContext());
            var fatora = uow.fawaterkTracking.Get(invoiceStatus.InvoiceId);
            if(fatora != null)
            {
                fatora.PaymentMethod = invoiceStatus.PaymentMethod;
                fatora.PaymentStatus = invoiceStatus.PaymentStatus;
                fatora.Total = invoiceStatus.Total;
                fatora.PaidDate = DateTime.Now;
                uow.Complete();
                return FawaterkReuslt.Success;
            }
            return FawaterkReuslt.Error;
        }

        private FawaterkReturn FawaterkRoomsRequestSender(InvoiceRequestModel model, string redirectUrl)
        {
            HttpResponseMessage response;
            var reqObj = new Root
            {
                VendorKey = APIKey,
                Currency = "EGP",
                Customer = new Customer
                {
                    FirstName = model.customerInfo.FirstName,
                    LastName = model.customerInfo.LastName,
                    Email = model.customerInfo.Email,
                    Phone = model.customerInfo.Phone,
                    //Address = model.customerInfo.Address
                },
                RedirectUrl = redirectUrl,
                CartItems = new List<CartItem>
                {
                    new CartItem
                    {
                        Name = string.Format("Hotel Name: {0}",model.roomInfos.FirstOrDefault()?.HotelName ),
                        Price = model.roomInfos.FirstOrDefault().Price,
                        Quantity = 1
                    }
                },

                CartTotal = model.roomInfos.FirstOrDefault().Price
            };

            var req = JsonConvert.SerializeObject(reqObj);
            using (HttpClient client = new HttpClient())
            {
                response = client.PostAsync("https://app.fawaterk.com/api/invoice", new StringContent(req, Encoding.UTF8, "application/json")).Result;
            }
            var invoiceRes = response.Content.ReadAsAsync<Entities.InvoiceResponseEntity.Root>().Result;

            if (invoiceRes.ErrorMessage == null || invoiceRes.InvoiceId > 0)
            {
                var invoiceStatusRes = FawaterkStatusRequestSender(invoiceRes.InvoiceId);
                var uow = new RepoServices.UnitOfWork(new FawaterksDbContext());
                uow.fawaterkTracking.Add(new FawaterkTrackingModel
                {
                    InvoiceId = invoiceRes.InvoiceId,
                    CreationDate = DateTime.Now,
                    InvoiceKey = invoiceRes.InvoiceKey,
                    PaidDate = new DateTime(1993, 2, 22),
                    PaymentMethod = invoiceStatusRes.PaymentMethod,
                    PaymentStatus = invoiceStatusRes.PaymentStatus,
                    Url = invoiceRes.Url,
                    Total = invoiceStatusRes.Total,
                    UserEmail = model.customerInfo.Email,
                    UserId = model.customerInfo.UserId,
                    FatoraDescription = model.roomInfos.FirstOrDefault()?.Description,
                    ReservationId = model.roomInfos.FirstOrDefault().Id,
                    isDeleted = false
                });

                var RoomAfterREservation = uow.userRoomsReservation.Get(model.roomInfos.FirstOrDefault().Id);

                RoomAfterREservation.FatoraId = invoiceRes.InvoiceId;
                RoomAfterREservation.UserId = model.customerInfo.UserId;
                RoomAfterREservation.Status = ReservationStatus.Booked;

                //uow.pckUserRes.Add(new PackageUserReservation
                //{
                //    FatoraId = invoiceRes.InvoiceId,
                //    PackageId = model.pck.Id,
                //    UserId = model.customerInfo.UserId
                //});
                uow.Complete();
            }
            else
            {
                return new FawaterkReturn { RedirectUrl = null, Result = FawaterkReuslt.Failed };
            }

            return new FawaterkReturn { RedirectUrl = invoiceRes.Url, Result = FawaterkReuslt.Success };
            //return null;
        }

        private FawaterkReturn FawaterkPackRequestSender(InvoiceRequestModel model, string redirectUrl)
        {
            HttpResponseMessage response;
            var reqObj = new Root
            {
                VendorKey = APIKey,
                Currency = "EGP",
                Customer = new Customer
                {
                    FirstName = model.customerInfo.FirstName,
                    LastName = model.customerInfo.LastName,
                    Email = model.customerInfo.Email,
                    Phone = model.customerInfo.Phone,
                    Address = model.customerInfo.Address
                },
                RedirectUrl = redirectUrl,
                CartItems = new List<CartItem> 
                {
                    new CartItem
                    {
                        Name = string.Format("Nexus Package {0}-ID{1}", model.pck.Title, model.pck.Id.ToString()),
                        Price = model.pck.Price,
                        Quantity = model.pck.Units
                    }
                },
                
                CartTotal = (model.pck.Price * model.pck.Units)
            };

            var req = JsonConvert.SerializeObject(reqObj);
            using (HttpClient client = new HttpClient())
            {
                response = client.PostAsync("https://app.fawaterk.com/api/invoice", new StringContent(req, Encoding.UTF8, "application/json")).Result;
            }
            var invoiceRes = response.Content.ReadAsAsync<Entities.InvoiceResponseEntity.Root>().Result;
            
            if (invoiceRes.ErrorMessage == null || invoiceRes.InvoiceId > 0)
            {
                var invoiceStatusRes = FawaterkStatusRequestSender(invoiceRes.InvoiceId);
                var uow = new RepoServices.UnitOfWork(new FawaterksDbContext());
                uow.fawaterkTracking.Add(new FawaterkTrackingModel
                {
                    InvoiceId = invoiceRes.InvoiceId,
                    CreationDate = DateTime.Now,
                    InvoiceKey = invoiceRes.InvoiceKey,
                    PaidDate = new DateTime(1993, 2, 22),
                    PaymentMethod = invoiceStatusRes.PaymentMethod,
                    PaymentStatus = invoiceStatusRes.PaymentStatus,
                    Url = invoiceRes.Url,
                    Total = invoiceStatusRes.Total,
                    UserEmail = model.customerInfo.Email,
                    UserId = model.customerInfo.UserId,
                    FatoraDescription = model.pck.Title,
                    ReservationId = model.pck.Id,
                    isDeleted = false
                });
                uow.pckUserRes.Add(new PackageUserReservation
                {
                    FatoraId = invoiceRes.InvoiceId,
                    PackageId = model.pck.Id,
                    UserId = model.customerInfo.UserId
                });
                uow.Complete();
            }
            else
            {
                return new FawaterkReturn { RedirectUrl = null, Result = FawaterkReuslt.Failed };
            }

            return new FawaterkReturn { RedirectUrl = invoiceRes.Url, Result = FawaterkReuslt.Success };
        }

        public Entities.InvoiceStatusResponseEntity.Root FawaterkStatusRequestSender(long invoiceId)
        {
            Entities.InvoiceStatusResponseEntity.Root invoiceStatusRes = new Entities.InvoiceStatusResponseEntity.Root();
            using (HttpClient client = new HttpClient())
            {
                invoiceStatusRes = JsonConvert.DeserializeObject<Entities.InvoiceStatusResponseEntity.Root>(client.GetStringAsync("https://app.fawaterk.com/invoices/api/{invoiceId}/status".Replace("{ID}", invoiceId.ToString())).Result);
            }

            return invoiceStatusRes;
        }

        public HttpResponseMessage FawaterDeleteInvoiceRequest(long invoiceId)
        {
            HttpResponseMessage response;
            using (HttpClient client = new HttpClient())
            {
                response = client.GetAsync("https://app.fawaterk.com/api/deleteInvoice?invoice_id={ID}".Replace("{ID}", invoiceId.ToString())).Result;
            }
            return response;
        }
    }
}
