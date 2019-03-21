using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data;
using TaskManager.Extensions;

namespace TaskManager.Repository
{
    public class RoleRepository:BaseRepository<IdentityRole>
    {
        private readonly ApplicationDbContext _context;
        public RoleRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }

        public string GetUserIdOfRole()
        {
            var roleId = _context.Roles.Where(x => x.Name == AppHelper.SuperAdminEndUser).FirstOrDefault().Id;
            var UserId = _context.UserRoles.Where(x => x.RoleId == roleId).FirstOrDefault().UserId;

            return UserId;
        }
    }
}
