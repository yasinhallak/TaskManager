using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Enums;

namespace TaskManager.Models
{
    public class ApplicationUser:IdentityUser
    {
        [Required]
        public string  FirstName { get; set; }
        [Required]
        public string  LastName { get; set; }
        [Required]
        public Gender? Gender { get; set; }
    }
}
