using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TaskManager.Enums;
using TaskManager.Extensions;
using TaskManager.Models;
using TaskManager.Repository;

namespace TaskManager.Data
{
    public class DbInitializer : IDbInitializer
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public DbInitializer(ApplicationDbContext context,
            UserManager<IdentityUser> userManager,
            RoleManager<IdentityRole> roleManager)
        {
            _context = context;
            _roleManager = roleManager;
            _userManager = userManager;
        }
        public async void Initialize()
        {
            if (_context.Database.GetPendingMigrations().Count() > 0)
            {
                _context.Database.Migrate();
            }

           if (_context.Roles.Any(r => r.Name == AppHelper.SuperAdminEndUser)) return;

            _roleManager.CreateAsync(new IdentityRole(AppHelper.Users)).GetAwaiter().GetResult();
            _roleManager.CreateAsync(new IdentityRole(AppHelper.SuperAdminEndUser)).GetAwaiter().GetResult();
            var user=new ApplicationUser();
            user.UserName = "Admin@hotmail.com";
            user.Email = "Admin@hotmail.com";
            user.FirstName = "tarek";
            user.LastName = "Saboni";
            user.Gender = 0;
            user.EmailConfirmed = true;
            _userManager.CreateAsync( user,"Admin-89").GetAwaiter().GetResult();

            var attachment = new Attachment();
            attachment.FileName = "MyImage.jpg";
            attachment.FileExtenstion = "jpg";
            attachment.OwnerId = user.Id;
            attachment.CreationDate=DateTime.Now;
            _context.Attachments.Add(attachment);
            _context.SaveChanges();
            var userAdmin = _userManager.FindByEmailAsync("Admin@hotmail.com");
            if(userAdmin != null) { 
            await _userManager.AddToRoleAsync(await userAdmin,
                AppHelper.SuperAdminEndUser);
            }
        }
    }
}
