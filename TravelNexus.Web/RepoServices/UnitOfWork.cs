using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Fawaterk.Service;

namespace TravelNexus.Web.RepoServices
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UserRepo users { get; set; }
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            users = new UserRepo(_context);
          
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