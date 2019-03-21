using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Security.Cryptography.X509Certificates;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Controllers;
using TaskManager.Data.ViewModel;
using TaskManager.Enums;
using TaskManager.Extensions;
using TaskManager.Models;

namespace TaskManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize]
    public class ProjectController : BaseController
    {
        private readonly IHostingEnvironment _hostingEnvironment;

        public ProjectController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        //get All Project
        public IActionResult Index()
        {
            var CurrentUserEmial = User.Identity.Name;
            var FirstName = adamUnit.AppUserRepository.SingleOrDefault(x => x.Email == CurrentUserEmial).FirstName;
            var LastName = adamUnit.AppUserRepository.SingleOrDefault(x => x.Email == CurrentUserEmial).LastName;
            var CurrentUserId = adamUnit.AppUserRepository.SingleOrDefault(x => x.Email == CurrentUserEmial).Id;
            if (User.IsInRole(AppHelper.Users))
            {
                var EmployeeId = adamUnit.EmployeeRepository.SingleOrDefault(x => x.UserId == CurrentUserId).Id;

                var ProjectEmployee = adamUnit.ProjectUserRepository.GetAll(x => x.UserId == EmployeeId).ToList();
                if (ProjectEmployee.Count!=0)
                {
                    var list1 = adamUnit.ProjectRepository.GetAll().Select(x => x.ToModelEmp(adamUnit, CurrentUserId)).ToList();
                    return View("ReadOnlyPro", list1);
                }
                return View("ReadOnlyPro", new List<ProjectVM>());

            }
            var list = adamUnit.ProjectRepository.GetAllInclude();
            return View("ListPro",list);
        }
        [HttpPost]
        public async Task<JsonResult> Comments(string data, long recordId)
        {
            var CurrentUserEmial = User.Identity.Name;
            var FirstName = adamUnit.AppUserRepository.SingleOrDefault(x => x.Email == CurrentUserEmial).FirstName;
            var LastName = adamUnit.AppUserRepository.SingleOrDefault(x => x.Email == CurrentUserEmial).LastName;
            var CurrentUserId = adamUnit.AppUserRepository.SingleOrDefault(x => x.Email == CurrentUserEmial).Id;
            if (User.IsInRole(AppHelper.SuperAdminEndUser))
            {
                var Attacment = adamUnit.AttachmentRepository.SingleOrDefault(x => x.OwnerId == CurrentUserId).FileName;
                var comment = new Comment();
                comment.TableId = recordId;
                comment.TableName = TableName.Project;
                comment.context = data;
                comment.UserId = CurrentUserId;
                await adamUnit.CommentRepository.InserAsync(comment);
                // Ids of employee Joined to Project and send Notify to them by Admin
                var employeeIds = adamUnit.ProjectUserRepository.GetAll(x => x.ProjectId == recordId).Select(x => x.UserId)
                    .ToList();
                foreach (var empId in employeeIds)
                {
                    var notify = new Notfication();
                    notify.OwnerUserId = CurrentUserId;
                    notify.ToUserId = adamUnit.EmployeeRepository.SingleOrDefault(x => x.Id == empId).UserId;
                    notify.TableName = TableName.Project;
                    notify.TablePkId = recordId;
                    notify.Context = "AddComment";
                    await adamUnit.NotificationRepository.InserAsync(notify);
                    var url = "/Admin/Project/Details?id=" + recordId;
                    if (adamUnit.TokenFireBaseRepository.Exists(x => x.UserId == notify.ToUserId))
                    {
                        var tokens = adamUnit.TokenFireBaseRepository.GetAll(x => x.UserId == notify.ToUserId).Select(x => x.token);
                        foreach (var token in tokens)
                        {
                            NotificationController.SendPushNotification(token, notify.Context, FirstName, url, Attacment);
                        }
                    }
                }

                var idDb = comment.Id;
                return Json(new { id = comment.Id, firstName = FirstName, lastName = LastName, attacment = Attacment, date = DateTime.Now.ToShortTimeString() });
            }
            else
            {
                var employeeId = adamUnit.EmployeeRepository.SingleOrDefault(x => x.UserId == CurrentUserId).Id;
                var Attacment = adamUnit.AttachmentRepository.SingleOrDefault(x => x.OwnerId == employeeId.ToString()).FileName;
                var comment = new Comment();
                comment.TableId = recordId;
                comment.TableName = TableName.Project;
                comment.context = data;
                comment.UserId = CurrentUserId;
                await adamUnit.CommentRepository.InserAsync(comment);
                // Ids of employee Joined to Project and send Notify to them by Employee
                var employeeIds = adamUnit.ProjectUserRepository.GetAll(x => x.ProjectId == recordId && x.UserId != employeeId).Select(x => x.UserId)
                    .ToList();
                if (employeeIds.Count > 1)
                {
                    foreach (var empId in employeeIds)
                    {
                        var UserId = adamUnit.EmployeeRepository.SingleOrDefault(x => x.Id == empId && x.UserId != CurrentUserId).UserId;

                        if (UserId != null)
                        {
                            var notify = new Notfication();
                            notify.OwnerUserId = CurrentUserId;
                            notify.ToUserId = UserId;
                            notify.TableName = TableName.Project;
                            notify.TablePkId = recordId;
                            notify.Context = "AddComment";
                            await adamUnit.NotificationRepository.InserAsync(notify);
                            var url = "/Admin/Project/Details?id=" + recordId;
                            if (adamUnit.TokenFireBaseRepository.Exists(x => x.UserId == UserId))
                            {

                                var tokens = adamUnit.TokenFireBaseRepository.GetAll(x => x.UserId == UserId).Select(x => x.token);
                                foreach (var token in tokens)
                                {
                                    NotificationController.SendPushNotification(token, notify.Context, FirstName, url, Attacment);
                                }
                            }
                        }
                    }
                }
                // save and send Notification to Admin By Employee
                // UserId of Admin
                var UserIdOfAdmin = adamUnit.RoleRepository.GetUserIdOfRole();
                var notfy = new Notfication();
                notfy.OwnerUserId = CurrentUserId;
                notfy.ToUserId = UserIdOfAdmin;
                notfy.TableName = TableName.Project;
                notfy.TablePkId = recordId;
                notfy.Context = "AddComment";
                await adamUnit.NotificationRepository.InserAsync(notfy);
                var Url = "/Admin/Project/Details?id=" + recordId;
                if (adamUnit.TokenFireBaseRepository.Exists(x => x.UserId == UserIdOfAdmin))
                {
                    var tokens = adamUnit.TokenFireBaseRepository.GetAll(x => x.UserId == UserIdOfAdmin).Select(x => x.token);
                    foreach (var token in tokens)
                    {
                        NotificationController.SendPushNotification(token, notfy.Context, FirstName, Url, Attacment);
                    }
                }
                return Json(new { id=comment.Id,firstName = FirstName, lastName = LastName, attacment = Attacment, date = DateTime.Now.ToShortTimeString() });
            }
        }

        [HttpGet]
        public JsonResult GetAllcommnet(long recordId)
        {
            var list = adamUnit.CommentRepository.GetAll(x=>x.TableName==TableName.Project && x.TableId==recordId).Select(x => x.ToModel(adamUnit)).ToList();
            return Json(list);
        }

        //Creat Project
        public IActionResult Create()
        {
            ViewBag.Customer = adamUnit.CustomerRepository.GetAll().ToList();
            ViewBag.Employee = adamUnit.EmployeeRepository.GetAllInclude().ToList();
            return View();
        }
        // Create Post
        [HttpPost]
        public async Task<IActionResult> Create(List<IFormFile> files, ProjectVM projectVm)
        {
            if (!adamUnit.ProjectRepository.Exists(x => x.Name == projectVm.Name))
            {
                // Current login User Information
                var CurrentUserEmial = User.Identity.Name;
                var CurrentUserId = adamUnit.AppUserRepository.SingleOrDefault(x => x.Email == CurrentUserEmial).Id;
                var CurrentName = adamUnit.AppUserRepository.SingleOrDefault(x => x.Email == CurrentUserEmial).FirstName;
                if (!ModelState.IsValid)
                {
                    return View(projectVm);
                }
                var project = new Models.Project();
                project = projectVm.ToEntity();
                await adamUnit.ProjectRepository.InserAsync(project);
                var ProjId = project.Id;
                //save UserInProjUser
                string[] array = Regex.Split(projectVm.EmpIds, ",");
                foreach (var item in array)
                {
                    var ProjUser = new ProjectUser();
                    ProjUser.ProjectId = ProjId;
                    ProjUser.UserId = Convert.ToInt64(item);
                    await adamUnit.ProjectUserRepository.InserAsync(ProjUser);
                }
                string webRootPath = _hostingEnvironment.WebRootPath;
                var state = adamUnit.AttachmentRepository.OnPostUpload(files, ProjId.ToString(),
                    AttachmentOwner.Projects, AttachmentType.Photo, webRootPath);
                //send Notification To Users
                foreach (var item in array)
                {
                    var UserId = adamUnit.EmployeeRepository.SingleOrDefault(x => x.Id == Convert.ToInt16(item)).UserId;
                    var UserName = adamUnit.AppUserRepository.SingleOrDefault(x => x.Id == UserId).UserName;
                    var attachment = adamUnit.AttachmentRepository.FirstOrDefault(x => x.OwnerId == item).FileName;
                    var notify = new Notfication();
                    notify.OwnerUserId = CurrentUserId;
                    notify.ToUserId = UserId;
                    notify.TableName = TableName.Project;
                    notify.TablePkId = ProjId;
                    notify.Context = "AddyouToProject";
                    var Url = "/Admin/Project/Details?id=" + ProjId;
                    await adamUnit.NotificationRepository.InserAsync(notify);
                    if (adamUnit.TokenFireBaseRepository.Exists(x => x.UserId == UserId))
                    {
                        var tokens = adamUnit.TokenFireBaseRepository.GetAll(x => x.UserId == UserId).Select(x => x.token);
                        foreach (var token in tokens)
                        {
                            NotificationController.SendPushNotification(token, notify.Context, CurrentName, Url, attachment);

                        }
                    }
                }
                TempData["SuccessMessage"] = "Created Successfully";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Name = "Name is Exist Already";
            ViewBag.Customer = adamUnit.CustomerRepository.GetAll().ToList();
            ViewBag.Employee = adamUnit.EmployeeRepository.GetAllInclude().ToList();
            return View(projectVm);
        }


        //Get Details Method
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Project = await adamUnit.ProjectRepository.GetByIdAsync(id);
            var ProjectVM = new ProjectVM();
            ProjectVM.EmlpSelected = adamUnit.ProjectUserRepository.GetAll(x => x.ProjectId == Project.Id).Select(x => x.UserId).ToList();
            ProjectVM.FilePath = adamUnit.AttachmentRepository.GetPhoto(id.ToString(), AttachmentOwner.Projects);

            ViewBag.Customer = adamUnit.CustomerRepository.GetAll().ToList();
            ViewBag.Employee = adamUnit.EmployeeRepository.GetAllInclude().ToList();
            return View(Project.ToModelDetails(ProjectVM));
        }
        //Get :edit
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var Project = await adamUnit.ProjectRepository.GetByIdAsync(id);
            var ProjectVM = new ProjectVM();
            ProjectVM.EmlpSelected = adamUnit.ProjectUserRepository.GetAll(x => x.ProjectId == Project.Id).Select(x => x.UserId).ToList();
            ProjectVM.FilePath = adamUnit.AttachmentRepository.GetPhoto(id.ToString(), AttachmentOwner.Projects);

            ViewBag.Customer = adamUnit.CustomerRepository.GetAll().ToList();
            ViewBag.Employee = adamUnit.EmployeeRepository.GetAllInclude().ToList();
            return View(Project.ToModelDetails(ProjectVM));
        }
        //Post edit
        [HttpPost]
        public async Task<IActionResult> Edit(List<IFormFile> files, ProjectVM ProjectVm)
        {
            var fileNameDb = adamUnit.AttachmentRepository.SingleOrDefault(x =>
                x.OwnerId == ProjectVm.Id.ToString() && x.AttachmentOwner == AttachmentOwner.Projects);
            if (files.Count == 0 && fileNameDb == null)
            {
                ViewBag.file = "Please enter file or image";
                var Project = await adamUnit.ProjectRepository.GetByIdAsync(ProjectVm.Id);
                var ProjectVM = new ProjectVM();
                ProjectVM.EmlpSelected = adamUnit.ProjectUserRepository.GetAll(x => x.ProjectId == Project.Id).Select(x => x.UserId).ToList();
                ProjectVM.FilePath = adamUnit.AttachmentRepository.GetPhoto(ProjectVm.Id.ToString(), AttachmentOwner.Projects);

                ViewBag.Customer = adamUnit.CustomerRepository.GetAll().ToList();
                ViewBag.Employee = adamUnit.EmployeeRepository.GetAllInclude().ToList();
                return View(Project.ToModelDetails(ProjectVM));
            }
            var NameDb = adamUnit.ProjectRepository.Find(ProjectVm.Id);
            if (NameDb.Name != ProjectVm.Name)
            {
                if (adamUnit.ProjectRepository.Exists(x => x.Name == ProjectVm.Name))
                {
                    ViewBag.Name = "Name is Exist Already";
                    var Project = await adamUnit.ProjectRepository.GetByIdAsync(ProjectVm.Id);
                    var ProjectVM = new ProjectVM();
                    ProjectVM.EmlpSelected = adamUnit.ProjectUserRepository.GetAll(x => x.ProjectId == Project.Id).Select(x => x.UserId).ToList();
                    ProjectVM.FilePath = adamUnit.AttachmentRepository.GetPhoto(ProjectVm.Id.ToString(), AttachmentOwner.Projects);

                    ViewBag.Customer = adamUnit.CustomerRepository.GetAll().ToList();
                    ViewBag.Employee = adamUnit.EmployeeRepository.GetAllInclude().ToList();
                    return View(Project.ToModelDetails(ProjectVM));
                }
            }
            if (ModelState.IsValid)
            {
                var Cust = adamUnit.ProjectRepository.SingleOrDefault(x => x.Id == ProjectVm.Id && x.IsDeleted == false);
                await adamUnit.ProjectRepository.UpdateAsync(ProjectVm.ToModelEdit(Cust));


                if (files != null && files.Count > 0)
                {
                    string webRootPath = _hostingEnvironment.WebRootPath;
                    var state = adamUnit.AttachmentRepository.OnPostUpload(files, ProjectVm.Id.ToString(),
                        AttachmentOwner.Projects, AttachmentType.Photo, webRootPath);
                }
                var UserEmp = adamUnit.ProjectUserRepository.GetAll(x => x.ProjectId == ProjectVm.Id)
                    .Select(x => x.UserId).ToList();
                string[] EmpArray = Regex.Split(ProjectVm.EmpIds, ",");

                //check from Employee in ProjectUser
                foreach (var item in UserEmp)
                {
                    if (!EmpArray.Contains(item.ToString()))
                    {
                        var UserTest = adamUnit.ProjectUserRepository.SingleOrDefault(x => x.ProjectId == ProjectVm.Id && x.UserId == item);
                        await adamUnit.ProjectUserRepository.DeleteAsync(UserTest.Id);
                    }
                }

                foreach (var item1 in EmpArray)
                {
                    if (!UserEmp.Contains(Convert.ToInt64(item1)))
                    {
                        var UserCreate = new ProjectUser();
                        UserCreate.ProjectId = ProjectVm.Id;
                        UserCreate.UserId = Convert.ToInt64(item1);
                        await adamUnit.ProjectUserRepository.InserAsync(UserCreate);
                    }
                }
                TempData["UpdateMessage"] = "Updated Successfully";
                return RedirectToAction(nameof(Index));

            }


            return View(ProjectVm);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteConfrim(long ProId)
        {
            // check Project in task
            var ProjInTask = adamUnit.TaskRepository.SingleOrDefault(x => x.ProjectId == ProId);
            var AttachId = adamUnit.AttachmentRepository.GetAll(x => x.OwnerId == ProId.ToString()).Select(x => x.Id);
            var result = false;
            if (ProjInTask == null)
            {
                if (AttachId != null)
                {
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
                }
                await adamUnit.ProjectRepository.DeleteAsync(ProId);
                // Delete ProjectUser
                var ProjectUser = adamUnit.ProjectUserRepository.GetAll(x => x.ProjectId == ProId).ToList();
                foreach (var item in ProjectUser)
                {
                    await adamUnit.ProjectUserRepository.DeleteAsync(item.Id);
                }

                result = true;
                TempData["DeleteMessage"] = "Deleted Successfully";
                return Json(result);
            }
            TempData["WarringMessage"] = "Sorry this Project belong to task";
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