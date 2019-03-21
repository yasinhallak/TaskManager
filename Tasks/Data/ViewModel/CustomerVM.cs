using AutoMapper;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.UI.Services;
using TaskManager.AdamResurces;
using TaskManager.Enums;
using TaskManager.Models;

namespace TaskManager.Data.ViewModel
{
    public static class CustomerVMExtensions
    {

        public static CustomerVM ToModel(this Customer entity)
        {
            return Mapper.Map<CustomerVM>(entity);
        }
        public static CustomerVM ToModelDetails(this Customer entity,CustomerVM customerVm)
        {
            return Mapper.Map(entity,customerVm);
        }
        public static Customer ToEntity(this CustomerVM model)
        {
            return Mapper.Map<Customer>(model);
        }
        public static Customer ToModelEdit(this CustomerVM model, Customer customer)
        {
            return Mapper.Map(model,customer);
        }
    }
    public class  CustomerVM
    {
        public long Id { get; set; }     
        [Required]
        [Display(ResourceType = typeof(_UserInfo), Name = "FirstName")]
        public string FirstName { get; set; }
        [Required]
        [Display(ResourceType = typeof(_UserInfo), Name = "LastName")]
        public string LastName { get; set; }
        [Required]
        [Display(ResourceType = typeof(_UserInfo), Name = "Gender")]
        public Gender? Gender { get; set; }
        [Required]
        [Display(ResourceType = typeof(_UserInfo), Name = "Nationality1")]
        public Nationality1? Nationality1 { get; set; }

        [Required]
        [Display(ResourceType = typeof(_UserInfo), Name = "Country")]
        public Country? Country { get; set; }

        [Required]
        [Display(ResourceType = typeof(_UserInfo), Name = "CityId")]
        public long CityId { get; set; }

        public virtual City City { get; set; }
        [Required]
        [Display(ResourceType = typeof(_UserInfo), Name = "Email")]
        public string Email { get; set; }
        [Required]
        [Display(ResourceType = typeof(_UserInfo), Name = "PhoneNumber")]
        public long Mobile { get; set; }

        [Display(ResourceType = typeof(_UserInfo), Name = "Address")]
        public string Address { get; set; }
        [Display(ResourceType = typeof(_UserInfo), Name = "Notes")]
        public string Notes { get; set; }


        public List<Attachment> FilePath { get; set; }

    }
}
