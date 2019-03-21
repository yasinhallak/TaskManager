using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using TaskManager.Controllers;
using TaskManager.Data.ViewModel;
using TaskManager.Enums;
using TaskManager.Extensions;
using TaskManager.Models;
using TaskManager.Repository;
using TaskManager.Utility;

namespace TaskManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class EmployeeController : BaseController
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IEmailSender _emailSender;
        public EmployeeController(
            IHostingEnvironment hostingEnvironment,
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager,
            IEmailSender emailSender)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _hostingEnvironment = hostingEnvironment;
        }
        // Get All Employee
        public IActionResult Index()
        {
            var list = adamUnit.EmployeeRepository.GetAllInclude();
            return View(list);
        }
        //check Email for Create
        [HttpPost]
        public JsonResult CheckEmail(string Email, string Id)
        {
            var result = false;

            if (Id == null)
            {
                if (adamUnit.AppUserRepository.IsRegisteredEmail(Email))
                {
                    result = true;
                }

                return Json(result);
            }
            else
            {
                var userDb = adamUnit.AppUserRepository.Find(Id);
                if (userDb.Email != Email)
                {
                    if (adamUnit.AppUserRepository.IsRegisteredEmail(Email))
                    {
                        result = true;
                    }

                    return Json(result);
                }
                return Json(result);
            }

        }
       
        [HttpPost]
        public ActionResult GetCityList(Country Country)
        {
            var ListCity = adamUnit.CityRepository.GetAll(x => x.Country == Country).ToList();
            ViewBag.StateOptions = new SelectList(ListCity, "Id", "Name");
            return PartialView("_StateOptionPartial");
        }
        //Create Action Method
        public IActionResult Create()
        {
            return View();
        }
        //Post Create Action Method
        [HttpPost]
        public async Task<IActionResult> Create(List<IFormFile> files, EmployeeVM employeeVm)
        {
            if (!adamUnit.AppUserRepository.IsRegisteredEmail(employeeVm.Email))
            {
                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser
                    {
                        FirstName = employeeVm.FirstName,
                        LastName = employeeVm.LastName,
                        Email = employeeVm.Email,
                        UserName = employeeVm.Email,
                        Gender = employeeVm.Gender,
                        PhoneNumber = employeeVm.PhoneNumber.ToString(),
                        SecurityStamp = Guid.NewGuid().ToString()
                    };
                    var result = await _userManager.CreateAsync(user, employeeVm.PhoneNumber.ToString());
                    employeeVm.UserId = user.Id;
                    if (result.Succeeded)
                    {
                        var emp = new Employee();
                        emp = employeeVm.ToEntity();
                        await _userManager.AddToRoleAsync(user, AppHelper.Users);
                        await adamUnit.EmployeeRepository.InserAsync(emp);
                        if (files != null && files.Count > 0)
                        {
                            string recordId = emp.Id.ToString();
                            string webRootPath = _hostingEnvironment.WebRootPath;
                            var state = adamUnit.AttachmentRepository.OnPostUpload(files, recordId,
                                AttachmentOwner.Employees, AttachmentType.Photo, webRootPath);
                        }
                        TempData["SuccessMessage"] = "Created Successfully";
                        return RedirectToAction(nameof(Index));
                    }
                }
            }

            ViewBag.Email = "Email is exist already";
            return View(employeeVm);

        }

        //Get Details Method
        public async Task<IActionResult> Details(long? id)
        {
            var employee = await adamUnit.EmployeeRepository.GetByIdAsync(id);
            if (id == null)
            {
                return NotFound();
            }
            var User = adamUnit.AppUserRepository.SingleOrDefault(x => x.Id == employee.UserId);
            var attachments = adamUnit.AttachmentRepository.GetPhoto(id.ToString(), AttachmentOwner.Employees);
            var EmpVM = new EmployeeVM();
            EmpVM.FirstName = User.FirstName;
            EmpVM.LastName = User.LastName;
            EmpVM.PhoneNumber =Convert.ToInt64(User.PhoneNumber);
            EmpVM.Gender = User.Gender;
            EmpVM.FilePath = attachments;
            ViewBag.ListCity = adamUnit.CityRepository.GetAll().ToList();

            return View(employee.ToModelDetails(EmpVM));
        }
        //Get :edit
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var employee = await adamUnit.EmployeeRepository.GetByIdAsync(id);
            if (employee == null)
            {
                return NotFound();
            }

            var User = adamUnit.AppUserRepository.SingleOrDefault(x => x.Id == employee.UserId);
            var attachments = adamUnit.AttachmentRepository.GetPhoto(id.ToString(), AttachmentOwner.Employees);
            var EmpVM = new EmployeeVM();
            EmpVM.FirstName = User.FirstName;
            EmpVM.LastName = User.LastName;
            EmpVM.PhoneNumber =Convert.ToInt64(User.PhoneNumber);
            EmpVM.Gender = User.Gender;
            EmpVM.FilePath = attachments;
            ViewBag.ListCity = adamUnit.CityRepository.GetAll(x => x.Country == employee.Country).ToList();
            return View(employee.ToModelDetails(EmpVM));
        }
        //Get Post edit
        [HttpPost]
        public async Task<IActionResult> Edit(List<IFormFile> files, EmployeeVM employeeVM)
        {
            var fileNameDb = adamUnit.AttachmentRepository.SingleOrDefault(x =>
               x.OwnerId == employeeVM.Id.ToString() && x.AttachmentOwner == AttachmentOwner.Employees);
            if (files.Count == 0 && fileNameDb == null)
            {
                ViewBag.ListCity = adamUnit.CityRepository.GetAll(x => x.Country == employeeVM.Country).ToList();
                ViewBag.file = "Please enter file or image";
                return View(employeeVM);
            }
            else
            {
                var userDb = adamUnit.AppUserRepository.Find(employeeVM.UserId);
                if (userDb.Email != employeeVM.Email)
                {
                    if (!adamUnit.AppUserRepository.IsRegisteredEmail(employeeVM.Email))
                    {
                        userDb.Email = employeeVM.Email;
                        userDb.UserName = employeeVM.Email;
                    }
                    else
                    {
                        employeeVM.FilePath =
                            adamUnit.AttachmentRepository.GetAll(x => x.OwnerId == employeeVM.Id.ToString()).ToList();
                        ViewBag.ListCity = adamUnit.CityRepository.GetAll(x => x.Country == employeeVM.Country).ToList();

                        ViewBag.Email = "Email is exists already";
                        return View(employeeVM);
                    }
                }
                if (ModelState.IsValid)
                {
                    userDb.FirstName = employeeVM.FirstName;
                    userDb.LastName = employeeVM.LastName;
                    userDb.Gender = employeeVM.Gender;
                    userDb.PhoneNumber = Convert.ToString(employeeVM.PhoneNumber);
                    var emp = adamUnit.EmployeeRepository.SingleOrDefault(x => x.Id == employeeVM.Id && x.IsDeleted == false);
                    await adamUnit.EmployeeRepository.UpdateAsync(employeeVM.ToModelEdit(emp));
                    string recordId = emp.Id.ToString();
                    // check for file if exist already in db
                    if (fileNameDb == null)
                    {
                        if (files != null && files.Count > 0)
                        {
                            string webRootPath = _hostingEnvironment.WebRootPath;
                            var state = adamUnit.AttachmentRepository.OnPostUpload(files, recordId,
                                AttachmentOwner.Employees, AttachmentType.Photo, webRootPath);
                        }
                    }
                    TempData["UpdateMessage"] = "Updated Successfully";
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.ListCity = adamUnit.CityRepository.GetAll(x => x.Country == employeeVM.Country).ToList();
                return View(employeeVM);
            }



        }



        //Post Delete Method
        [HttpGet]
        public async Task<IActionResult> DeleteConfrim(long? empId)
        {
            var empInProject = adamUnit.ProjectUserRepository.SingleOrDefault(x => x.UserId == empId);
            var empInTask = adamUnit.TaskRepository.SingleOrDefault(x => x.EmployeeId == empId);
            var result = false;
            if (empInProject == null && empInTask == null)
            {
                result = true;
                var AttachId = adamUnit.AttachmentRepository.GetAll(x => x.OwnerId == empId.ToString()).Select(x => x.Id);
                var emp = adamUnit.EmployeeRepository.SingleOrDefault(x => x.IsDeleted == false && x.Id == empId);
                var UserId = emp.UserId;
                await adamUnit.EmployeeRepository.DeleteAsync(empId);
                await adamUnit.AppUserRepository.DeleteAsync(UserId);
                foreach (var id in AttachId)
                {
                    var attachment = adamUnit.AttachmentRepository.Find(id);
                    if (attachment != null)
                    {
                        var NameAttachment = attachment.FileName;
                        var OwnerAttachment = attachment.AttachmentOwner;
                        string WebRootPath = _hostingEnvironment.WebRootPath;
                        AppHelper.FileUploadHelper.ContainsFile(NameAttachment, OwnerAttachment, WebRootPath);
                        adamUnit.AttachmentRepository.DeleteAsync(id);
                    }

                }
                TempData["DeleteMessage"] = "Deleted Successfully";
                return Json(result);
            }
            TempData["WarringMessage"] = "sorry this user belong to project or task";
            return Json(result);
        }

        //Delete File
        [HttpGet]
        public JsonResult DeleteFile(Guid Id)
        {
            var attachment = adamUnit.AttachmentRepository.Find(Id);
            if (attachment != null)
            {
                var NameAttachment = attachment.FileName;
                var OwnerAttachment = attachment.AttachmentOwner;
                string WebRootPath = _hostingEnvironment.WebRootPath;
                AppHelper.FileUploadHelper.ContainsFile(NameAttachment, OwnerAttachment, WebRootPath);
                adamUnit.AttachmentRepository.DeleteAsync(Id);
            }
            return Json("Ok");
        }

        public IActionResult Profile()
        {
            return View();
        }
    }
}