using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Controllers;
using TaskManager.Data.ViewModel;
using TaskManager.Enums;

namespace TaskManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]

    public class DashboardController : BaseController
    {
        public IActionResult Index()
        {
            return View();
        }

        
    }
}