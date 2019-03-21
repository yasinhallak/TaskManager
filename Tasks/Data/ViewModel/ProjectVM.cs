using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.AdamResurces;
using TaskManager.Models;
using TaskManager.Repository;

namespace TaskManager.Data.ViewModel
{
    public static class ProjectVMExtensions
    {

        public static ProjectVM ToModelRep(this Project entity)
        {
            return Mapper.Map<ProjectVM>(entity);
        }
        public static ProjectVM ToModel(this Project entity,AdamUnit adamUnit)
        {
            var project = new ProjectVM();
            project.Id = entity.Id;
            project.Name = entity.Name;
            project.CustomerName = entity.Customer.FirstName;
            
                var IdsProjects = adamUnit.ProjectUserRepository.GetAll(x => x.ProjectId == entity.Id).Select(x => x.UserId);
                var listName = " ";
                foreach (var id in IdsProjects)
                {
                    var UserId = adamUnit.EmployeeRepository.SingleOrDefault(x => x.Id == id).UserId;
                    var Name = adamUnit.AppUserRepository.SingleOrDefault(x => x.Id == UserId).FirstName;
                    listName += " " + Name ;
                }
                project.EmplyeeName = listName;
            return project;
        }
        public static ProjectVM ToModelEmp(this Project entity, AdamUnit adamUnit,string CurrentUserId)
        {
            var EmployeeId = adamUnit.EmployeeRepository.SingleOrDefault(x => x.UserId == CurrentUserId).Id;
            var project = new ProjectVM();
            var ProjectUser = adamUnit.ProjectUserRepository.SingleOrDefault(x => x.ProjectId == entity.Id && x.UserId== EmployeeId);
            var listName = " ";
            if (ProjectUser != null)
            {
                var UserId = adamUnit.EmployeeRepository.SingleOrDefault(x => x.Id == ProjectUser.UserId).UserId;
                if (UserId == CurrentUserId)
                {
                    var Name = adamUnit.AppUserRepository.SingleOrDefault(x => x.Id == UserId).FirstName;
                    listName += " " + Name;
                    project.Id = entity.Id;
                    project.Name = entity.Name;
                    project.EmplyeeName = listName;
                }
            }
           
            return project;
        }

        public static ProjectVM ToModelReport(this Project entity, AdamUnit adamUnit,ProjectVM model)
        {

            model.Id = entity.Id;
            model.Name = entity.Name;
            var listName = " ";
            foreach (var id in model.EmlpSelected)
            {
                var UserId = adamUnit.EmployeeRepository.SingleOrDefault(x => x.Id == id).UserId;
                var Name = adamUnit.AppUserRepository.SingleOrDefault(x => x.Id == UserId).FirstName;
                listName += " " + Name;
            }
            model.EmplyeeName = listName;


            return model;
        }
        public static ProjectVM ToModelDetails(this Project entity,ProjectVM model)
        {
            return Mapper.Map(entity, model);
        }
        public static Project ToEntity(this ProjectVM model)
        {
            return Mapper.Map<Project>(model);
        }
        public static Project ToModelEdit(this ProjectVM model, Project Project)
        {
            return Mapper.Map(model, Project);
        }
    }

    public class ProjectVM
    {
        public long Id { get; set; }
        [Required]
        [Display(ResourceType = typeof(_ProjectVM), Name = "Name")]
        public string Name { get; set; }
        [Required]
        [Display(ResourceType = typeof(_ProjectVM), Name = "CustomerId")]
        public long CustomerId { get; set; }
        [Required]
        [Display(ResourceType = typeof(_ProjectVM), Name = "EmployeeId")]
        public long EmployeeId { get; set; }

        public string EmpIds { get; set; }
        [Display(ResourceType = typeof(_ProjectVM), Name = "EmplyeeName")]
        public string  EmplyeeName { get; set; }

        public List<long> EmlpSelected { get; set; }
        [Display(ResourceType = typeof(_ProjectVM), Name = "CustomerName")]
        public string  CustomerName { get; set; }

        public List<Attachment>  FilePath { get; set; }
    }
}
