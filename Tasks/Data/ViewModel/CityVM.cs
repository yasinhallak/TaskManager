using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Remotion.Linq.Parsing.Structure.IntermediateModel;
using TaskManager.AdamResurces;
using TaskManager.Enums;
using TaskManager.Models;

namespace TaskManager.Data.ViewModel
{
    public static class CityVMExtensions
    {
        public static CityVM ToModel(this City entity)
        {
            return Mapper.Map<CityVM>(entity);
        }
        public static City ToEntity(this CityVM model)
        {
            return Mapper.Map<City>(model);
        }
        public static City ToModelEdit(this CityVM model,City city)
        {
            return Mapper.Map(model, city);
        }
    }
    public class CityVM
    {
        public long Id { get; set; }
        [Required]
        [Display(ResourceType = typeof(_CityVM), Name = "Name")]
        public string  Name { get; set; }
        [Required]
        [Display(ResourceType = typeof(_CityVM), Name = "Country")]
        public Country? Country { get; set; }
    }
}
