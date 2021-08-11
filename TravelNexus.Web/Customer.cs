using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace TravelNexus.Web
{
  

    [Table("Customers")]
    public class Customer
    {
        [Key]
        public long Id { get; set; }
        public CustomerType Type { get; set; } // 0 Admin -- 1 Customer -- 2 Partner
        public string FirstName { get; set; }
        public string LastName { get; set; }
        //public string Email { get; set; }
        [Required]
        public long UserId { get; set; }
        public bool IsApproved { get; set; }
        //Default Set to false.
        public bool IsDeleted { get; set; }
        public DateTime CreationDate { get; set; }
        // for Partners
        public string Address { get; set; }
        public string CommercialRegister { get; set; }
        public string TaxCard { get; set; }

        [ForeignKey(nameof(UserId))]
        public virtual ApplicationUser AppUser { get; set; }

    }
}