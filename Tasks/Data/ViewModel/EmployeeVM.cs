using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.AdamResurces;
using TaskManager.Enums;
using TaskManager.Models;

namespace TaskManager.Data.ViewModel
{
    public static class EmployeeVMExtensions
    {

        public static EmployeeVM ToModel(this Employee entity)
        {
            return Mapper.Map<EmployeeVM>(entity);
        }
        public static EmployeeVM ToModelDetails(this Employee entity,EmployeeVM Employeevm)
        {
            return Mapper.Map(entity, Employeevm);
        }
        public static Employee ToEntity(this EmployeeVM model)
        {
            return Mapper.Map<Employee>(model);
        }
        
        public static Employee ToModelEdit(this EmployeeVM model, Employee employee)
        {
            return Mapper.Map(model, employee);
        }
    }
    public class EmployeeVM
    {
        public long Id { get; set; }
        [Required]
        [Display(ResourceType = typeof(_UserInfo), Name = "FirstName")]
        public string  FirstName { get; set; }
        [Required]
        [Display(ResourceType = typeof(_UserInfo), Name = "LastName")]
        public string  LastName { get; set; }
        [Required]
        [Display(ResourceType = typeof(_UserInfo), Name = "Gender")]
        [EnumDataType(typeof(Gender))]
        public Gender? Gender { get; set; }
        [Required]
        [Display(ResourceType = typeof(_UserInfo), Name = "Email")]
        public string  Email { get; set; }
        [Display(ResourceType = typeof(_UserInfo), Name = "BirthDate")]
        public DateTime? BirthDate { get; set; }
        [Required]
        [Display(ResourceType = typeof(_UserInfo), Name = "Country")]
        public Country? Country { get; set; }
        [Required]
        [Display(ResourceType = typeof(_UserInfo), Name = "PhoneNumber")]
        public long PhoneNumber { get; set; }
        [Required]
        [Display(ResourceType = typeof(_UserInfo), Name = "Address")]
        public string Address { get; set; }
        [Required]
        [Display(ResourceType = typeof(_UserInfo), Name = "CityId")]
        public long? CityId { get; set; }

       
        [Display(ResourceType = typeof(_UserInfo), Name = "Notes")]
        public string Notes { get; set; }

        public string  UserId { get; set; }
        [Required]
        [Display(ResourceType = typeof(_UserInfo), Name = "Nationality1")]
        public Nationality1? Nationality1 { get; set; }

        public List<Attachment> FilePath { get; set; }

    }

   
}
