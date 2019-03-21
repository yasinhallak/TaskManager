using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TaskManager.Controllers;
using TaskManager.Data.ViewModel;
using Rotativa;
using Rotativa.AspNetCore;
using TaskManager.Extensions;

namespace TaskManager.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = AppHelper.SuperAdminEndUser)]
     
    public class ReportController : BaseController
    {
        public IActionResult Index()
        {
            var ReportVM = new ReportVM();
            ReportVM.ListEmployees = adamUnit.EmployeeRepository.GetAllInclude().ToList();
            ReportVM.ListCustomers = adamUnit.CustomerRepository.GetAll().Select(x => x.ToModel()).ToList();
            ReportVM.ListProjects = adamUnit.ProjectRepository.GetAll().Select(x => x.ToModelRep()).ToList();
            ReportVM.ListTasks = adamUnit.TaskRepository.GetAll().Select(x => x.ToModel()).ToList();
            return View(ReportVM);
        }
        // Get Employee Info
        public IActionResult EmployeeList(long? EmployeeId)
        {
            if (EmployeeId == null)
            {
                var list = adamUnit.EmployeeRepository.GetAllInclude();
                return new ViewAsPdf("EmployeeList", list);
            }

            var UserId = adamUnit.EmployeeRepository.SingleOrDefault(x => x.Id == EmployeeId).UserId;
            var list1 = adamUnit.EmployeeRepository.GetIncludeReport(EmployeeId);
            var user = adamUnit.AppUserRepository.SingleOrDefault(x => x.Id == UserId);
            list1.FirstName = user.FirstName;
            list1.LastName = user.LastName;
            list1.Email = user.Email;
            list1.PhoneNumber =Convert.ToInt64(user.PhoneNumber);
            list1.Gender = user.Gender;
            return new ViewAsPdf("EmployeeCard", list1);

        }

        //Get Customers
        public async Task<IActionResult> CustomerList(long? CustomerId)
        {
            if (CustomerId == null)
            {
                var list = adamUnit.CustomerRepository.GetAll().Select(x => x.ToModel()).ToList();
                return new ViewAsPdf("CustomerList", list);
            }

            var customer = await adamUnit.CustomerRepository.GetByIdAsync(CustomerId);

            return new ViewAsPdf("CustomerCard", customer.ToModel());

        }
        // Get Projects
        public async Task<IActionResult> ProjectList(long? ProjectId)
        {
            if (ProjectId == null)
            {
                var list = adamUnit.ProjectRepository.GetAllInclude();
                return new ViewAsPdf("ProjectList", list);
            }
            var Pro = await adamUnit.ProjectRepository.GetByIdAsync(ProjectId);
            var CustomerName = adamUnit.CustomerRepository.SingleOrDefault(x => x.Id == Pro.CustomerId).FirstName;
            var ProjectVM = new ProjectVM();
            ProjectVM.EmlpSelected = adamUnit.ProjectUserRepository.GetAll(x => x.ProjectId == Pro.Id).Select(x => x.UserId).ToList();
            ProjectVM.CustomerName = CustomerName;
            return new ViewAsPdf("ProjectCard", Pro.ToModelReport(adamUnit, ProjectVM));
        }

        // Get Tasks
        public async Task<IActionResult> TaskList(ReportVM report)
        {
            var seedtask = new SeededTasks();
            if (report.Taskvm == null)
            {
                 
                 seedtask.tasks= adamUnit.TaskRepository.GetAll().Select(x => x.ToModel()).ToList();
                 seedtask.Seed = null;
                return new ViewAsPdf("TaskList", seedtask);
            }
            seedtask.tasks =  adamUnit.TaskRepository
                .GetAll(x => x.StartDate >= report.StartDate && x.EndDate <= report.EndDate).Select(x => x.ToModel()).ToList();
            seedtask.Seed = null;

            return new ViewAsPdf("TaskList", seedtask);
        }
        // Get Employee Task
        public async Task<IActionResult> EmployeeTask(ReportVM report)
        {
            var seedtask = new SeededTasks();
            if (report.Development == null && report.StartDate == null && report.EndDate == null)
            {
                seedtask.tasks = adamUnit.TaskRepository.GetAll().Select(x => x.ToModel()).ToList();
                seedtask.Seed = null;
                return new ViewAsPdf("TaskList", seedtask);
            }
            else if (report.StartDate == null && report.EndDate == null)
            {
                seedtask.tasks = adamUnit.TaskRepository
                    .GetAll(x => x.Development == report.Development).Select(x => x.ToModel()).ToList();
                seedtask.Seed = null;
                return new ViewAsPdf("TaskList", seedtask);
            }
            seedtask.tasks = adamUnit.TaskRepository
                .GetAll(x => x.StartDate >= report.StartDate && x.EndDate <= report.EndDate && x.Development == report.Development).Select(x => x.ToModel()).ToList();
            seedtask.Seed = null;
            return new ViewAsPdf("TaskList", seedtask);
        }

        // Get  Task Status
        public async Task<IActionResult> TaskStatus(ReportVM report)
        {
            var seedtask = new SeededTasks();
            if (report.EmployeeId == null)
            {
                seedtask.tasks = adamUnit.TaskRepository.GetAll().Select(x => x.ToModel()).ToList();
                seedtask.Seed = null;
                return new ViewAsPdf("TaskList", seedtask);
            }
            seedtask.tasks = adamUnit.TaskRepository
                .GetAll(x => x.StartDate >= report.StartDate && x.EndDate <= report.EndDate && x.EmployeeId == report.EmployeeId).Select(x => x.ToModel()).ToList();
            seedtask.Seed = null;
            return new ViewAsPdf("TaskList", seedtask);
        }

        // Get  Task Status
        public async Task<IActionResult> ProjectTask(ReportVM report)
        {
            var seedtask = new SeededTasks();
            if (report.ProjectId == null)
            {
                seedtask.tasks = adamUnit.TaskRepository.GetAll().Select(x => x.ToModel()).ToList();
                seedtask.Seed = null;
                return new ViewAsPdf("TaskList", seedtask);
            }
            seedtask.tasks = adamUnit.TaskRepository
                .GetAll(x => x.StartDate >= report.StartDate && x.EndDate <= report.EndDate && x.ProjectId == report.ProjectId).Select(x => x.ToModel()).ToList();
            seedtask.Seed = null;
            return new ViewAsPdf("TaskList", seedtask);
        }
    }
}