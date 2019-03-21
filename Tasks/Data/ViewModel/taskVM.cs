using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using AutoMapper;
using Remotion.Linq.Parsing.Structure.IntermediateModel;
using TaskManager.AdamResurces;
using TaskManager.Enums;
using TaskManager.Models;
using TaskManager.Repository;

namespace TaskManager.Data.ViewModel
{

    public static class TaskVMExtensions
    {

        public static TaskVM ToModel(this task entity)
        {
            return Mapper.Map<TaskVM>(entity);
        }
        public static TaskVM ToModelDetails(this task entity,TaskVM taskVm)
        {
            return Mapper.Map(entity, taskVm);
        }

        public static task ToEntity(this TaskVM model)
        {
            return Mapper.Map<task>(model);
        }
        public static task ToModelEdit(this TaskVM model, task Project)
        {
            return Mapper.Map(model, Project);
        }
    }
    public class TaskVM
    {
        public long  Id { get; set; }
        [Required]
        [Display(ResourceType = typeof(_taskVM), Name = "ProjectId")]
        public long ProjectId { get; set; }
        [Required]
        [Display(ResourceType = typeof(_taskVM), Name = "Name")]
        public string  Name { get; set; }
        [Required]
        [Display(ResourceType = typeof(_taskVM), Name = "Priority")]
        public Priority? Priority { get; set; }
        [Display(ResourceType = typeof(_taskVM), Name = "Development")]
        public Development? Development { get; set; }
        [Required]
        [Display(ResourceType = typeof(_taskVM), Name = "StartDate")]
        public DateTime? StartDate { get; set; }
        [Required]
        [Display(ResourceType = typeof(_taskVM), Name = "EndDate")]
        public DateTime?  EndDate { get; set; }
        [Required]
        [Display(ResourceType = typeof(_taskVM), Name = "EmployeeId")]
        public long  EmployeeId { get; set; }

        [Required]
        [Display(ResourceType = typeof(_taskVM), Name = "Description")]
        public string Description { get; set; }
        [Required]
        [Display(ResourceType = typeof(_taskVM), Name = "RequiredDetail")]
        public string RequiredDetail { get; set; }
        [Required]
        [Display(ResourceType = typeof(_taskVM), Name = "Recommendation")]
        public string Recommendation { get; set; }
        [Required]
        [Display(ResourceType = typeof(_taskVM), Name = "StatusOfAcievement")]
        public string StatusOfAcievement { get; set; }

        public List<EmployeeVM> Employees { get; set; }
        public List<Project> Projects { get; set; }
        public List<Attachment> FilePath { get; set; }

        public long? ParentId { get; set; }
    }

    public class SeededTasks
    {
        public long? Seed { get; set; }
        public IEnumerable<TaskVM> tasks { get; set; }
    }

}
