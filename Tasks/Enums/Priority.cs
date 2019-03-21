using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.AdamResurces;

namespace TaskManager.Enums
{
    public enum Priority
    {
        [Display(ResourceType = typeof(_Proiority), Name = "Hight")]
        Hight,
        [Display(ResourceType = typeof(_Proiority), Name = "Medium")]
        Medium,
        [Display(ResourceType = typeof(_Proiority), Name = "Low")]
        Low
    }
}
