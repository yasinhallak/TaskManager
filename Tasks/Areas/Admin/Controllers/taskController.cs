using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using System.Transactions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using TaskManager.Controllers;
using TaskManager.Data.ViewModel;
using TaskManager.Enums;
using TaskManager.Extensions;
using TaskManager.hubs;
using TaskManager.Models;

namespace TaskManager.Areas.Admin.Controllers
{
   
    [Area("Admin")]
    [Authorize]
    public class taskController : BaseController
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public taskController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }
        //Get All Tasks
        public IActionResult Index(int? Projec)
        {
            var CurrentUserEmial = User.Identity.Name;
            var FirstName = adamUnit.AppUserRepository.SingleOrDefault(x => x.Email == CurrentUserEmial).FirstName;
            var LastName = adamUnit.AppUserRepository.SingleOrDefault(x => x.Email == CurrentUserEmial).LastName;
            var CurrentUserId = adamUnit.AppUserRepository.SingleOrDefault(x => x.Email == CurrentUserEmial).Id;

            var list = adamUnit.TaskRepository.GetAll(x=>x.ParentId==null).Select(x => x.ToModel()).ToList();

            if (Projec == null)
            {
                list = adamUnit.TaskRepository.GetAll(x => x.ParentId == null).Select(X => X.ToModel()).ToList();
            }
            else
            {
                list = adamUnit.TaskRepository.GetAll(x => x.ProjectId == Convert.ToInt64(Projec) && x.ParentId == null).Select(X => X.ToModel()).ToList();
            }
            if (User.IsInRole(AppHelper.Users))
            {
                var employeeId = adamUnit.EmployeeRepository.SingleOrDefault(x => x.UserId == CurrentUserId).Id;
                var List1 = adamUnit.TaskRepository.GetAll(x => x.EmployeeId == employeeId && x.ParentId == null).Select(x => x.ToModel()).ToList();
                return View("ReadOnlyList", List1);

            }
            ViewBag.Projec = new SelectList(adamUnit.ProjectRepository.GetAll().ToList(), "Id", "Name");
            return View("List", list);

        }
        // show task
        public IActionResult ShowTask(long Id)
        {
          var taskvm=new TaskVM();
          taskvm.ParentId = adamUnit.TaskRepository.SingleOrDefault(x => x.Id == Id).Id;
          taskvm.Employees= adamUnit.EmployeeRepository.GetAllInclude().ToList();
          taskvm.Projects = adamUnit.ProjectRepository.GetAll().ToList();

            return PartialView("_PartialTask",taskvm);
        }
        // retrive subTask
        //[HttpPost]
        //public IActionResult SubTask(long trid)
        //{
        //    var List = adamUnit.TaskRepository.GetAll(x => x.ParentId == trid).Select(x => x.ToModelJson()).OrderBy(a => a.parentid).ToList();
        //    return Json(List);
        //}
        // Retrive SubTask
       [HttpGet]
        public IActionResult SubTask(long trid)
        {
            var List = adamUnit.TaskRepository.GetAll(x => x.ParentId == trid).Select(x => x.ToModel()).ToList();
            if (User.IsInRole(AppHelper.SuperAdminEndUser))
            {
                return PartialView("_ListTask", List);
            }
            return PartialView("_ListTaskReadOnly", List);
        }

        // Insert data in db
        [HttpPost]
        public async Task<IActionResult> SaveTask(List<IFormFile> files, TaskVM taskVM)
        {
            if (ModelState.IsValid)
            {
                if (!adamUnit.TaskRepository.Exists(x => x.Name == taskVM.Name && x.ProjectId == taskVM.ProjectId))
                {
                    // Current login User Information
                    var CurrentUserEmial = User.Identity.Name;
                    var CurrentUserId = adamUnit.AppUserRepository.SingleOrDefault(x => x.Email == CurrentUserEmial).Id;
                    var CurrentName = adamUnit.AppUserRepository.SingleOrDefault(x => x.Email == CurrentUserEmial).FirstName;

                    var tas = new task();
                    taskVM.Development = Development.init;
                    tas = taskVM.ToEntity();
                    await adamUnit.TaskRepository.InserAsync(tas);
                    string recordId = tas.Id.ToString();
                    string webRootPath = _hostingEnvironment.WebRootPath;
                    var state = adamUnit.AttachmentRepository.OnPostUpload(files, recordId,
                        AttachmentOwner.Tasks, AttachmentType.Photo, webRootPath);

                    var UserId = adamUnit.EmployeeRepository.SingleOrDefault(x => x.Id == taskVM.EmployeeId).UserId;
                    var UserName = adamUnit.AppUserRepository.SingleOrDefault(x => x.Id == UserId).UserName;
                    // Retrive UserId of Admin
                    var UserIdOfAdmin = adamUnit.RoleRepository.GetUserIdOfRole();
                    var attachment = "";
                    if (CurrentUserId == UserIdOfAdmin)
                    {
                        attachment = adamUnit.AttachmentRepository.FirstOrDefault(x => x.OwnerId == CurrentUserId).FileName;
                    }
                    else
                    {
                        var employeeId = adamUnit.EmployeeRepository.SingleOrDefault(x => x.UserId == CurrentUserId).Id;
                        attachment = adamUnit.AttachmentRepository
                           .FirstOrDefault(x => x.OwnerId == employeeId.ToString()).FileName;
                    }
                    var notify = new Notfication();
                    notify.OwnerUserId = CurrentUserId;
                    notify.ToUserId = UserId;
                    notify.TableName = TableName.task;
                    notify.TablePkId = taskVM.Id;
                    notify.Context = "AddYouToTask";
                    await adamUnit.NotificationRepository.InserAsync(notify);
                    var Url = "/Admin/task/Details?Id=" + taskVM.Id;
                    if (adamUnit.TokenFireBaseRepository.Exists(x => x.UserId == UserId))
                    {
                        var tokens = adamUnit.TokenFireBaseRepository.GetAll(x => x.UserId == UserId).Select(x => x.token);
                        foreach (var token in tokens)
                        {
                            NotificationController.SendPushNotification(token, notify.Context, CurrentName, Url, attachment);
                        }
                    }

                    TempData["SuccessMessage"] = "Created Successfully";
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Name = "Name is Exist Already";
                ViewBag.Project = adamUnit.ProjectRepository.GetAll().ToList();
                ViewBag.Employee = adamUnit.EmployeeRepository.GetAllInclude().ToList();
                return PartialView("_PartialTask", taskVM);
            }
            ViewBag.Project = adamUnit.ProjectRepository.GetAll().ToList();
            ViewBag.Employee = adamUnit.EmployeeRepository.GetAllInclude().ToList();
            return PartialView("_PartialTask", taskVM);
        }
        //SearchtProjectList
        public JsonResult SearchtProjectList(string searchTerm)
        {
            var ProjectList = adamUnit.ProjectRepository.GetAll(x => x.IsDeleted == false).ToList();
            if (searchTerm != null)
            {
                ProjectList = adamUnit.ProjectRepository.GetAll(x => x.Name.Contains(searchTerm)).ToList();
            }
            var modifiedData = ProjectList.Select(x => new
            {
                id = x.Id,
                text = x.Name
            });
            return Json(modifiedData);
        }
        // Add comments
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
                comment.TableName = TableName.task;
                comment.context = data;
                comment.UserId = CurrentUserId;
                await adamUnit.CommentRepository.InserAsync(comment);
                var EmployeeId = adamUnit.TaskRepository.SingleOrDefault(x => x.Id == recordId).EmployeeId;
                var UserId = adamUnit.EmployeeRepository.SingleOrDefault(x => x.Id == EmployeeId).UserId;
                var notify = new Notfication();
                notify.OwnerUserId = CurrentUserId;
                notify.ToUserId = UserId;
                notify.TableName = TableName.task;
                notify.TablePkId = recordId;
                notify.Context = "Add comment";
                await adamUnit.NotificationRepository.InserAsync(notify);
                var Url = "/Admin/task/Details?id=" + recordId;
                if (adamUnit.TokenFireBaseRepository.Exists(x => x.UserId == UserId))
                {
                    var tokens = adamUnit.TokenFireBaseRepository.GetAll(x => x.UserId == UserId).Select(x => x.token);
                    foreach (var token in tokens)
                    {
                        NotificationController.SendPushNotification(token, notify.Context, FirstName, Url, Attacment);
                    }
                }
                return Json(new {id=comment.Id, firstName = FirstName, lastName = LastName, attacment = Attacment, date = DateTime.Now.ToShortTimeString() });
            }
            else
            {
                var employeeId = adamUnit.EmployeeRepository.SingleOrDefault(x => x.UserId == CurrentUserId).Id;
                var Attacment = adamUnit.AttachmentRepository.SingleOrDefault(x => x.OwnerId == employeeId.ToString()).FileName;
                // Retrive UserId of Role
                var UserIdOfAdmin = adamUnit.RoleRepository.GetUserIdOfRole();
                var comment = new Comment();
                comment.TableId = recordId;
                comment.TableName = TableName.task;
                comment.context = data;
                comment.UserId = CurrentUserId;
                await adamUnit.CommentRepository.InserAsync(comment);

                var notify = new Notfication();
                notify.OwnerUserId = CurrentUserId;
                notify.ToUserId = UserIdOfAdmin;
                notify.TableName = TableName.task;
                notify.TablePkId = recordId;
                notify.Context = "Add comment";
                await adamUnit.NotificationRepository.InserAsync(notify);
                var Url = "/Admin/task/Details?id=" + recordId;
                if (adamUnit.TokenFireBaseRepository.Exists(x => x.UserId == UserIdOfAdmin))
                {
                    var tokens = adamUnit.TokenFireBaseRepository.GetAll(x => x.UserId == UserIdOfAdmin).Select(x => x.token);
                    foreach (var token in tokens)
                    {
                        NotificationController.SendPushNotification(token, notify.Context, FirstName, Url,Attacment);
                    }
                }
                return Json(new { id = comment.Id, firstName = FirstName, lastName = LastName, attacment = Attacment, date = DateTime.Now.ToShortTimeString() });
            }
        }
        // Retrive All comments
        [HttpGet]
        public JsonResult GetAllcommnet(long recordId)
        {
            var list = adamUnit.CommentRepository.GetAll(x=>x.TableName==TableName.task && x.TableId==recordId).Select(x => x.ToModel(adamUnit)).ToList();
            return Json(list);
        }
        //Get :Details
        public async Task<IActionResult> Details(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var task = await adamUnit.TaskRepository.GetByIdAsync(id);
            if (task == null)
            {
                return NotFound();
            }
            ViewBag.Project = adamUnit.ProjectRepository.GetAll().ToList();
            ViewBag.Employee = adamUnit.EmployeeRepository.GetAllInclude().ToList();
            var taskVm = new TaskVM();
            taskVm.FilePath = adamUnit.AttachmentRepository.GetPhoto(id.ToString(), AttachmentOwner.Tasks);
            return View(task.ToModelDetails(taskVm));
        }
        //create Get
        public IActionResult Create()
        {
            ViewBag.Project = adamUnit.ProjectRepository.GetAll().ToList();
            ViewBag.Employee = adamUnit.EmployeeRepository.GetAllInclude().ToList();
            return View();
        }

        // Create Post
        [HttpPost]
        public async Task<IActionResult> Create(List<IFormFile> files, TaskVM taskVM)
        {
            if (ModelState.IsValid)
            {
                if (!adamUnit.TaskRepository.Exists(x => x.Name == taskVM.Name && x.ProjectId == taskVM.ProjectId))
                {
                    // Current login User Information
                    var CurrentUserEmial = User.Identity.Name;
                    var CurrentUserId = adamUnit.AppUserRepository.SingleOrDefault(x => x.Email == CurrentUserEmial).Id;
                    var CurrentName = adamUnit.AppUserRepository.SingleOrDefault(x => x.Email == CurrentUserEmial).FirstName;

                    var tas = new task();
                    taskVM.Development = Development.init;
                    tas = taskVM.ToEntity();
                    await adamUnit.TaskRepository.InserAsync(tas);
                    string recordId = tas.Id.ToString();
                    string webRootPath = _hostingEnvironment.WebRootPath;
                    var state = adamUnit.AttachmentRepository.OnPostUpload(files, recordId,
                        AttachmentOwner.Tasks, AttachmentType.Photo, webRootPath);

                    var UserId = adamUnit.EmployeeRepository.SingleOrDefault(x => x.Id == taskVM.EmployeeId).UserId;
                    var UserName = adamUnit.AppUserRepository.SingleOrDefault(x => x.Id == UserId).UserName;
                    // Retrive UserId of Admin
                    var UserIdOfAdmin = adamUnit.RoleRepository.GetUserIdOfRole();
                    var attachment = "";
                    if (CurrentUserId == UserIdOfAdmin)
                    {
                        attachment=adamUnit.AttachmentRepository.FirstOrDefault(x=>x.OwnerId==CurrentUserId).FileName;
                    }
                    else
                    {
                        var employeeId = adamUnit.EmployeeRepository.SingleOrDefault(x => x.UserId == CurrentUserId).Id;
                         attachment = adamUnit.AttachmentRepository
                            .FirstOrDefault(x => x.OwnerId == employeeId.ToString()).FileName;
                    }
                    var notify = new Notfication();
                    notify.OwnerUserId = CurrentUserId;
                    notify.ToUserId = UserId;
                    notify.TableName = TableName.task;
                    notify.TablePkId = taskVM.Id;
                    notify.Context = "AddYouToTask";
                    await adamUnit.NotificationRepository.InserAsync(notify);
                    var Url = "/Admin/task/Details?Id=" + taskVM.Id;
                    if (adamUnit.TokenFireBaseRepository.Exists(x => x.UserId == UserId))
                    {
                        var tokens = adamUnit.TokenFireBaseRepository.GetAll(x => x.UserId == UserId).Select(x => x.token);
                        foreach (var token in tokens)
                        {
                            NotificationController.SendPushNotification(token, notify.Context, CurrentName, Url, attachment);
                        }
                    }

                    TempData["SuccessMessage"] = "Created Successfully";
                    return RedirectToAction(nameof(Index));
                }
                ViewBag.Name = "Name is Exist Already";
                ViewBag.Project = adamUnit.ProjectRepository.GetAll().ToList();
                ViewBag.Employee = adamUnit.EmployeeRepository.GetAllInclude().ToList();
                return View(taskVM);
            }
            ViewBag.Project = adamUnit.ProjectRepository.GetAll().ToList();
            ViewBag.Employee = adamUnit.EmployeeRepository.GetAllInclude().ToList();
            return View(taskVM);
        }

        //Get :edit
        public async Task<IActionResult> Edit(long? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var task = await adamUnit.TaskRepository.GetByIdAsync(id);

            if (task == null)
            {
                return NotFound();
            }
            ViewBag.Project = adamUnit.ProjectRepository.GetAll().ToList();
            ViewBag.Employee = adamUnit.EmployeeRepository.GetAllInclude().ToList();
            var taskVm = new TaskVM();
            taskVm.FilePath = adamUnit.AttachmentRepository.GetPhoto(id.ToString(), AttachmentOwner.Tasks);
            return View(task.ToModelDetails(taskVm));
        }
        //Post edit
        [HttpPost]
        public async Task<IActionResult> Edit(List<IFormFile> files, TaskVM taskVm)
        {
            // check for exist file in Db
            var fileNameDb = adamUnit.AttachmentRepository.SingleOrDefault(x =>
                x.OwnerId == taskVm.Id.ToString() && x.AttachmentOwner == AttachmentOwner.Tasks);
            if (files.Count == 0 && fileNameDb == null)
            {

                ViewBag.file = "Please enter file or image";
                ViewBag.Project = adamUnit.ProjectRepository.GetAll().ToList();
                ViewBag.Employee = adamUnit.EmployeeRepository.GetAllInclude().ToList();
                return View(taskVm);
            }
            // check for exist Name
            var taskDb = adamUnit.TaskRepository.Find(taskVm.Id);
            if (taskDb.Name != taskVm.Name)
            {
                if (adamUnit.TaskRepository.Exists(x => x.Name == taskVm.Name && x.ProjectId == taskVm.ProjectId))
                {
                    taskVm.FilePath =
                        adamUnit.AttachmentRepository.GetAll(x => x.OwnerId == taskVm.Id.ToString()).ToList();
                    ViewBag.Name = "Name is Exist Already";
                    ViewBag.Project = adamUnit.ProjectRepository.GetAll().ToList();
                    ViewBag.Employee = adamUnit.EmployeeRepository.GetAllInclude().ToList();
                    return View(taskVm);
                }
            }

            if (ModelState.IsValid)
            {
                var task = adamUnit.TaskRepository.SingleOrDefault(x => x.Id == taskVm.Id && x.IsDeleted == false);
                taskVm.Development = task.Development;
                await adamUnit.TaskRepository.UpdateAsync(taskVm.ToModelEdit(task));
                string recordId = task.Id.ToString();
                if (files != null && files.Count > 0)
                {
                    string webRootPath = _hostingEnvironment.WebRootPath;
                    var state = adamUnit.AttachmentRepository.OnPostUpload(files, recordId,
                        AttachmentOwner.Tasks, AttachmentType.Photo, webRootPath);
                }
                TempData["UpdateMessage"] = "Updated Successfully";
                return RedirectToAction(nameof(Index));
            }
            taskVm.FilePath =
                adamUnit.AttachmentRepository.GetAll(x => x.OwnerId == taskVm.Id.ToString()).ToList();
            ViewBag.Name = "Name is Exist Already";
            ViewBag.Project = adamUnit.ProjectRepository.GetAll().ToList();
            ViewBag.Employee = adamUnit.EmployeeRepository.GetAllInclude().ToList();
            return View(taskVm);
        }

        // delete task from db
        [HttpGet]
        public async Task<IActionResult> DeleteConfrim(long? taskId)
        {
            // delete ParentId

            var taskParnt = adamUnit.TaskRepository.FirstOrDefault(x => x.Id == taskId);
            if (taskParnt!=null)
            {
              await  adamUnit.TaskRepository.DeleteAsync(taskId);
            }
            var AttachId = adamUnit.AttachmentRepository.GetAll(x => x.OwnerId == taskId.ToString() && x.AttachmentOwner==AttachmentOwner.Tasks).Select(x => x.Id);
            
            var list = adamUnit.TaskRepository.GetAll().ToList();
            var list1 = adamUnit.TaskRepository.GetAll().Select(x=>x.Id).ToList();
            var result = false;
            // delete attachment form server and db
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
            // delete  tree of task
            foreach (var  item in list)
            {
                if (item.ParentId == taskId)
                {
                    await adamUnit.TaskRepository.DeleteAsync(item.Id);
                        await DeleteConfrim(item.Id);
                }
            }
            result = true;
            TempData["DeleteMessage"] = "Deleted Successfully";
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
        //change Priority
        [HttpPost]
        public async Task<ActionResult> ChangePriority(Priority sel, long ids)
        {
            var tasks = adamUnit.TaskRepository.SingleOrDefault(x => x.Id == ids);

            tasks.Priority = sel;
            await adamUnit.TaskRepository.UpdateAsync(tasks);
            return Json("ok");
        }

        //change Development
        [HttpPost]
        public async Task<ActionResult> ChangeDevelopment(Development selec, long id)
        {
            var Email = User.Identity.Name;
            // Name of current Login
            var Name = adamUnit.AppUserRepository.SingleOrDefault(x => x.Email == Email).FirstName;
            // UserId of CurrentLogin
            var UserCurrentId = adamUnit.AppUserRepository.SingleOrDefault(x => x.Email == Email).Id;
             //id of Employee
             var EmployeeId = adamUnit.EmployeeRepository.SingleOrDefault(x => x.UserId == UserCurrentId).Id;
            //attachmet of UserCurrentLogin
            var attachment = adamUnit.AttachmentRepository.FirstOrDefault(x => x.OwnerId == EmployeeId.ToString()).FileName;

            // Id of current Login 
            var IdOfCur = adamUnit.AppUserRepository.SingleOrDefault(x => x.Email == Email).Id;
            var task = adamUnit.TaskRepository.SingleOrDefault(x => x.Id == id);
            task.Development = selec;
            await adamUnit.TaskRepository.UpdateAsync(task);
            //retrive UserId of role
            var UserId = adamUnit.RoleRepository.GetUserIdOfRole();
            // retriv UserName of role
            var userName = adamUnit.AppUserRepository.SingleOrDefault(x => x.Id == UserId).UserName;
            var notify = new Notfication();
            notify.ToUserId = UserId;
            notify.OwnerUserId = IdOfCur;
            notify.TableName = TableName.task;
            notify.TablePkId = id;
            notify.IsRead = false;
            notify.Context = "Change status";
            await adamUnit.NotificationRepository.InserAsync(notify);
            var Url = "/Admin/task/Details?id=" + id;
            if (adamUnit.TokenFireBaseRepository.Exists(x => x.UserId == UserId))
            {
                var tokens = adamUnit.TokenFireBaseRepository.GetAll(x => x.UserId == UserId).Select(x => x.token);
                foreach (var token in tokens)
                {
                    NotificationController.SendPushNotification(token,notify.Context , Name, Url,attachment);
                }
            }
            return Json("ok");
        }
        // Delete Comment
        [HttpGet]
        public async Task<IActionResult> DeleteComment(long ComId)
        {
            var result = false;
            var CurrentUserEmial = User.Identity.Name;
            var FirstName = adamUnit.AppUserRepository.SingleOrDefault(x => x.Email == CurrentUserEmial).FirstName;
            var LastName = adamUnit.AppUserRepository.SingleOrDefault(x => x.Email == CurrentUserEmial).LastName;
            var CurrentUserId = adamUnit.AppUserRepository.SingleOrDefault(x => x.Email == CurrentUserEmial).Id;
            var comment = adamUnit.CommentRepository.SingleOrDefault(x => x.Id == ComId && x.UserId == CurrentUserId);
            if (comment != null)
            {
                result = true;
                await adamUnit.CommentRepository.DeleteAsync(ComId);
                TempData["DeleteMessage"] = "Deleted Successfully";
                return Json(result);
            }
            TempData["WarringMessage"] = "Sorry this the comment belong to other User";
            return Json(result);
        }

    }
}