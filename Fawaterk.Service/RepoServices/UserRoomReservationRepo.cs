using Fawaterk.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Fawaterk.Service.RepoServices
{
    public class UserRoomReservationRepo : Repo<UserRoomReservation, long>
    {
        public UserRoomReservationRepo(FawaterksDbContext tctx) : base(tctx)
        {

        }
    }
}