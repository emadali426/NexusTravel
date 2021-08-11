using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fawaterk.Service.Entities
{
    public class FawaterkTrackingModel
    {
        [Key]
        public long Id { get; set; }
        public long UserId { get; set; }
        public string UserEmail { get; set; }
        public string Url { get; set; }
        public string InvoiceKey { get; set; }
        public long InvoiceId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime PaidDate { get; set; }
        public PaymentStatus PaymentStatus { get; set; }
        public string PaymentMethod { get; set; }
        public double Total { get; set; }
        public string FatoraDescription { get; set; }
        public long ReservationId { get; set; }
        public bool isDeleted { get; set; }
    }

    public enum PaymentStatus
    {
        UnPaied = 0,
        Paied = 1
    }

   
    

   
}
