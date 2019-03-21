using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;
using TaskManager.Enums;
using TaskManager.Model;

namespace TaskManager.Models
{
    public class City:BaseEntity
    {
       public long Id { get; set; }
        [Required]
       public  string Name { get; set; }
        [Required]
        public Country?  Country { get; set; }
    }
}
