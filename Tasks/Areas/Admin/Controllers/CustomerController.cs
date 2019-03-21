using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TaskManager.Controllers;
using TaskManager.Data.ViewModel;
using TaskManager.Enums;
using TaskManager.Extensions;
using TaskManager.Models;

namespace TaskManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class CustomerController : BaseController
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public CustomerController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        //GetAllCustomer
        public IActionResult Index()
        {
            var list = adamUnit.CustomerRepository.GetAll(x => x.IsDeleted == false).Select(x => x.ToModel()).ToList();
            return View(list);
        }

        //Create Action Method
        public IActionResult Create()
        {
            return View();
        }
  
        [HttpPost]
        public ActionResult GetCityList(Country country)
        {
            var ListCity = adamUnit.CityRepository.GetAll(x => x.Country == country).ToList();

            ViewBag.StateOptions = new SelectList(ListCity, "Id", "Name");
            return PartialView("_StateOptionPartial");
        }

        //Post Create Action Method
        [HttpPost]
        public async Task<IActionResult> Create(List<IFormFile> files, CustomerVM customervm)
        {
            var NameDb = adamUnit.CustomerRepository.Exists(x => x.FirstName == customervm.FirstName);
            if (NameDb == false)
            {
                if (!adamUnit.CustomerRepository.Exists(x => x.Email == customervm.Email))
                {
                    if (ModelState.IsValid)
                    {
                        var customer = new Customer();
                        customer = customervm.ToEntity();
                        await adamUnit.CustomerRepository.InserAsync(customer);
                        string recordId = customer.Id.ToString();
                        string webRootPath = _hostingEnvironment.WebRootPath;
                        var state = adamUnit.AttachmentRepository.OnPostUpload(files, recordId,
                            AttachmentOwner.Customers, AttachmentType.Photo, webRootPath);
                        TempData["SuccessMessage"] = "Created Successfully";
                        return RedirectToAction(nameof(Index));
                    }

                }
                ViewBag.Email = "Email is exist already";
                return View(customervm);
            }
            ViewBag.Name = "Name is exist already";
            return View(customervm);

        }

        //Get Details Method
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var Customer = await adamUnit.CustomerRepository.GetByIdAsync(id);

            if (Customer == null)
            {
                return NotFound();
            }


            ViewBag.ListCity = adamUnit.CityRepository.GetAll(x => x.Country == Customer.Country).ToList();
            var CutsVM = new CustomerVM();

            CutsVM.FilePath = adamUnit.AttachmentRepository.GetAll(x => x.OwnerId == id.ToString() && x.AttachmentOwner == AttachmentOwner.Customers).ToList();

            return View(Customer.ToModelDetails(CutsVM));
        }
        //check Email
        [HttpPost]
        public JsonResult CheckEmail(string Email , string Id)
        {
            var result = false;
            if (Id == null)
            {
                if (adamUnit.CustomerRepository.IsRegisteredEmail(Email))
                {
                    result = true;
                }

                return Json(result);
            }
            else
            {
                var userDb = adamUnit.CustomerRepository.Find(Convert.ToInt64(Id));
                if (userDb.Email != Email)
                {
                    if (adamUnit.CustomerRepository.IsRegisteredEmail(Email))
                    {
                        result = true;
                    }

                    return Json(result);
                }
                return Json(result);
            }
           
        }
        //check Email for Edit
      
        //Get :edit
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Customer = await adamUnit.CustomerRepository.GetByIdAsync(id);
            if (Customer == null)
            {
                return NotFound();
            }


            ViewBag.ListCity = adamUnit.CityRepository.GetAll(x => x.Country == Customer.Country).ToList();
            var CutsVM = new CustomerVM();

            CutsVM.FilePath = adamUnit.AttachmentRepository.GetAll(x => x.OwnerId == id.ToString() && x.AttachmentOwner==AttachmentOwner.Customers).ToList();

            return View(Customer.ToModelDetails(CutsVM));

        }
        //Post edit
        [HttpPost]
        public async Task<IActionResult> Edit(List<IFormFile> files, CustomerVM customerVM)
        {
            var fileNameDb = adamUnit.AttachmentRepository.SingleOrDefault(x =>
                x.OwnerId == customerVM.Id.ToString() && x.AttachmentOwner == AttachmentOwner.Customers);
            if (files.Count == 0 && fileNameDb == null)
            {
                ViewBag.ListCity = adamUnit.CityRepository.GetAll(x => x.Country == customerVM.Country).ToList();
                ViewBag.file = "Please enter  image";
                return View(customerVM);
            }
            var userDb = adamUnit.CustomerRepository.Find(customerVM.Id);
            if (userDb.Email != customerVM.Email)
            {
                if (adamUnit.CustomerRepository.Exists(x => x.Email == customerVM.Email))
                {
                    customerVM.FilePath =
                        adamUnit.AttachmentRepository.GetAll(x => x.OwnerId == customerVM.Id.ToString()).ToList();
                    ViewBag.ListCity = adamUnit.CityRepository.GetAll(x => x.Country == customerVM.Country).ToList();

                    ViewBag.Email = "Email is exists already";
                    return View(customerVM);
                   
                }
            }
            if (ModelState.IsValid)
            {
                var Cust = adamUnit.CustomerRepository.SingleOrDefault(x => x.Id == customerVM.Id && x.IsDeleted == false);
                await adamUnit.CustomerRepository.UpdateAsync(customerVM.ToModelEdit(Cust));
                string recordId = Cust.Id.ToString();
                if (files != null && files.Count > 0)
                {
                    string webRootPath = _hostingEnvironment.WebRootPath;
                    var state = adamUnit.AttachmentRepository.OnPostUpload(files, recordId,
                        AttachmentOwner.Customers, AttachmentType.Photo, webRootPath);
                }
                TempData["UpdateMessage"] = "Updated Successfully";
                return RedirectToAction(nameof(Index));
            }

            return View();
        }


        //Post Delete Method
        [HttpGet]
        public async Task<IActionResult> DeleteConfrim(long? empId)
        {
            var CustomerInProject = adamUnit.ProjectRepository.SingleOrDefault(x => x.CustomerId == empId);
            var AttachId = adamUnit.AttachmentRepository.GetAll(x => x.OwnerId == empId.ToString()).Select(x => x.Id);
            var result = false;
            if (CustomerInProject == null)
            {
                result = true;
                await adamUnit.CustomerRepository.DeleteAsync(empId);
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
            TempData["WarringMessage"] = "sorry this Customer belong to project";
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
    }
}