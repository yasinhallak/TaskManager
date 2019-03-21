using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.AdamResurces;

namespace TaskManager.Enums
{
    public enum SideBar
    {
        [Display(ResourceType = typeof(_SideBar), Name = "Employee")]
        Employee,
        [Display(ResourceType = typeof(_SideBar), Name = "Customer")]
        Customer,
        [Display(ResourceType = typeof(_SideBar), Name = "Project")]
        Project,
        [Display(ResourceType = typeof(_SideBar), Name = "Company")]
        Company,
        [Display(ResourceType = typeof(_SideBar), Name = "task")]
        task,
        [Display(ResourceType = typeof(_SideBar), Name = "City")]
        City,
        [Display(ResourceType = typeof(_SideBar), Name = "Reports")]
        Reports,

    }
}
