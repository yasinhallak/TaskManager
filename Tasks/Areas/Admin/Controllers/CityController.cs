using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.UI.Pages.Internal.Account.Manage;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;
using TaskManager.Controllers;
using TaskManager.Data.ViewModel;
using TaskManager.Enums;
using TaskManager.Models;

namespace TaskManager.Areas.Admin.Controllers
{
    
    [Area("Admin")]
    [Authorize]
    public class CityController : BaseController
    {
        private readonly IStringLocalizer<HomeController> _localizer;

        public CityController(IStringLocalizer<HomeController> localizer)
        {
            _localizer = localizer;
        }
        public IActionResult Index()
        {
            var list = adamUnit.CityRepository.GetAll().Select(x => x.ToModel()).ToList();
            return View(list);
        }
        //Create Action Method
        public IActionResult Create()
        {
        
            return View();
        }
        //Post Create Action Method
        [HttpPost]
        public async Task<IActionResult> Create(CityVM Cityvm)
        {
            if (!adamUnit.CityRepository.Exists(x => x.Name == Cityvm.Name  && x.Country==Cityvm.Country))
            {
                if (ModelState.IsValid)
                {
                    await adamUnit.CityRepository.InserAsync(Cityvm.ToEntity());
                    TempData["SuccessMessage"] = "Created Successfully";
                    return RedirectToAction(nameof(Index));

                }
            }
            ViewBag.Name = "Name is Exists already";
            return View(Cityvm);
        }

        //Get edit Method
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var City = await adamUnit.CityRepository.GetByIdAsync(id);
            return View(City.ToModel());
        }

        //Post Edit Action Method
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit( CityVM cityVm)
        {
            var NameDb = adamUnit.CityRepository.Find(cityVm.Id);
            if (NameDb.Name != cityVm.Name)
            {
                if (adamUnit.CityRepository.Exists(x => x.Name == cityVm.Name && x.Country == cityVm.Country))
                {
                    ViewBag.Name = "Name is Exists already";
                    return View(cityVm);
                }
            }
            if (ModelState.IsValid)
            {
                await adamUnit.CityRepository.UpdateAsync(cityVm.ToModelEdit(NameDb));
                TempData["UpdateMessage"] = "Updated Successfully";
                return RedirectToAction(nameof(Index));
            }
            return View(cityVm);
        }

        //Get Details Method
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var City = await adamUnit.CityRepository.GetByIdAsync(id);
            return View(City.ToModel());
        }

       
        //Post Delete Method
        [HttpGet]
        public async Task<IActionResult> DeleteConfrim(long CityId)
        {
            var Employee = adamUnit.EmployeeRepository.SingleOrDefault(x => x.CityId == CityId);
            var customer = adamUnit.CustomerRepository.SingleOrDefault(x => x.CityId == CityId);
            var result = false;
            if (Employee == null && customer==null)
            {
                result = true;
                var city = await adamUnit.CityRepository.GetByIdAsync(CityId);
                await adamUnit.CityRepository.DeleteAsync(city.Id);
                TempData["DeleteMessage"] = "Deleted Successfully";
                return Json(result);
            }
            TempData["WarringMessage"] = "Sorry Exsit User belong To this city";
            return Json(result);
        }
    }
}