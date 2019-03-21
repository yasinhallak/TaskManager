using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using TaskManager.AdamResurces;

namespace TaskManager.Enums
{
    public enum Language
    {
        [Display(ResourceType = typeof(_Languages), Name = "Arabic")]
        ar_SY,
        [Display(ResourceType = typeof(_Languages), Name = "English")]
        en_US

    }
}