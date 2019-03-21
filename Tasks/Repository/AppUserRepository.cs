using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Repository
{
    public class AppUserRepository:BaseRepository<ApplicationUser>
    {
        private readonly ApplicationDbContext _context;
        public AppUserRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }


        public bool IsRegisteredEmail(string email)
        {
            return _context.Users.Any(x => x.Email == email);
        }
    }
}
