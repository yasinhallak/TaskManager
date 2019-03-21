using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Controllers;
using TaskManager.Models;

namespace TaskManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]

    public class ResetPasswordController : BaseController
    {
        private readonly UserManager<IdentityUser> _userManager;

        public ResetPasswordController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ResetPassword()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> ResetPassword( ResetPassword resetPassword)
        {
            if (ModelState.IsValid)
            {

            
            var user = await _userManager.FindByEmailAsync(resetPassword.Email);
            var newpasswordHash = _userManager.PasswordHasher.HashPassword(user, resetPassword.Password);
            user.PasswordHash = newpasswordHash;
             await  adamUnit.IdentityUserRepository.UpdateAsync(user);
                TempData["SuccessMessage"] = "Resete Successfully";
                return RedirectToAction("Index", "task");
            }
            return View(resetPassword);
        }

    }


}