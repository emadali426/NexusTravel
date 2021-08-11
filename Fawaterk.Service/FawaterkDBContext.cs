using Fawaterk.Service.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fawaterk.Service
{
    public class FawaterksDbContext : DbContext, IDisposable
    {
        static FawaterksDbContext()
        {
            Database.SetInitializer<FawaterksDbContext>(new FawaterkDbInit());
        }
        public FawaterksDbContext()
          : base("FawaterkDBTransactions")
        {

            Database.Initialize(false);
        }

        public virtual DbSet<FawaterkTrackingModel> Fawaterk { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<UserHotelsWhishList> UserHotelsWhishLists { get; set; }
        public virtual DbSet<UserRoomReservation> UserRoomReservations { get; set; }
        public virtual DbSet<NewsModel> News { get; set; }
        public virtual DbSet<OurServiceModel> OurService { get; set; }
        public virtual DbSet<OurCustomerFavDestModel> OurCustomerFavDest { get; set; }
        public virtual DbSet<PopularHotelsModel> PopularHotels { get; set; }
        public virtual DbSet<MailNewsModel> MailNews { get; set; }
        public virtual DbSet<PackageUserReservation> PackageUserInvoiceRes { get; set; }
        public virtual DbSet<PopHotelsModel> PopHotels { get; set; }
        public virtual DbSet<PopUserModel> PopUsers { get; set; }

        public static FawaterksDbContext Create()
        {
            return new FawaterksDbContext();
        }


        public class FawaterkDbInit : CreateDatabaseIfNotExists<FawaterksDbContext>
        {

            protected override void Seed(FawaterksDbContext context)
            {
                PerformInitialSetup(context); base.Seed(context);
            }

            private void PerformInitialSetup(FawaterksDbContext context)
            {

            }

            public override void InitializeDatabase(FawaterksDbContext context)
            {
                base.InitializeDatabase(context);

            }

        }
    }
}
