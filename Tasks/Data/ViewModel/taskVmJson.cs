using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using TaskManager.AdamResurces;
using TaskManager.Enums;
using TaskManager.Models;

namespace TaskManager.Data.ViewModel
{
    public static class TaskVMJsonExtensions
    {

        public static taskVmJson ToModelJson(this task entity)
        {
            var taskJson=new taskVmJson();
            taskJson.id = entity.Id;
            taskJson.name = entity.Name;
            taskJson.development = entity.Development.ToString();
            taskJson.priority = entity.Priority.ToString();
            taskJson.startdate = entity.StartDate.ToString();
            taskJson.enddate = entity.EndDate.ToString();
            taskJson.parentid = entity.ParentId;
            return taskJson;
        }

    }
    public class taskVmJson
    {
        public long id { get; set; }
        public string name { get; set; }
        [Display(ResourceType = typeof(_taskVM), Name = "Priority")]
        public string priority { get; set; }
        [Display(ResourceType = typeof(_taskVM), Name = "Development")]
        public string development { get; set; }
        [DataType(DataType.Date)]

        public string startdate { get; set; }
        [DataType(DataType.Date)]
   
        public string enddate { get; set; }
        public long? parentid { get; set; }

    }
}
