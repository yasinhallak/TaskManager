using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.AdamResurces;

namespace TaskManager.Enums
{
    public enum Gender
    {
        [Display(ResourceType = typeof(_Gender), Name = "Male")]
        Male,
        [Display(ResourceType = typeof(_Gender), Name = "Female")]
        Female
    }
}
