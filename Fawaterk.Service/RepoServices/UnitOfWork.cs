using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Fawaterk.Service;

namespace Fawaterk.Service.RepoServices
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly FawaterksDbContext _context;
        public UserHotelsWhishListRepo userHotelsWhishList { get; set; }
        public UserRoomReservationRepo userRoomsReservation { get; set; }
        public PackagesRepo packages { get; private set; }
        public NewsRepo nexusNews { get; private set; }
        public FawaterkTrackingModelRepo fawaterkTracking { get; set; }
        public PackageUserReservationRepo pckUserRes { get; set; }
        public OurServiceModelRepo ourServices { get; set; }
        public PopHotelsRepo popHotels { get; set; }
        public PopUsersRepo popUsers { get; set; }
        public FavDestRepo favDest { get; set; }
        public UnitOfWork(FawaterksDbContext context)
        {
            _context = context;
            userHotelsWhishList = new UserHotelsWhishListRepo(_context);
            userRoomsReservation = new UserRoomReservationRepo(_context);
            packages = new PackagesRepo(_context);          
            fawaterkTracking = new FawaterkTrackingModelRepo(_context);
            pckUserRes = new PackageUserReservationRepo(_context);
            ourServices = new OurServiceModelRepo(_context);
            nexusNews = new NewsRepo(_context);
            popHotels = new PopHotelsRepo(_context);
            popUsers = new PopUsersRepo(_context);
            favDest = new FavDestRepo(context);
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}