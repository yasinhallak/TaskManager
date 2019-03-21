using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.AdamResurces;

namespace TaskManager.Enums
{
    public enum Development
    {
        [Display(ResourceType = typeof(_Development), Name = "Done")]
        Done,
        [Display(ResourceType = typeof(_Development), Name = "WorkingOn")]
        WorkingOn,
        [Display(ResourceType = typeof(_Development), Name = "Stuck")]
        Stuck,
        [Display(ResourceType = typeof(_Development), Name = "init")]
        init,
    }
}
