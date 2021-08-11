using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fawaterk.Service.Entities
{
   
    public class CustomerInfo
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Address { get; set; }
        public long UserId { get; set; }
        
    }

    public class InvoiceRequestModel
    {
        public List<UserRoomReservation> roomInfos { get; set; }
        public CustomerInfo customerInfo { get; set; }
        public Package pck { get; set; }
    }
}
