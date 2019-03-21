using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using TaskManager.Data;

namespace TaskManager.Repository
{
    public class IdentityUserRepository:BaseRepository<IdentityUser>
    {
        public IdentityUserRepository(ApplicationDbContext context):base(context)
        {
                
        }
    }
}
