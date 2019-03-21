using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Security.Cryptography;
using System.Threading.Tasks;
using TaskManager.Enums;
using TaskManager.Model;

namespace TaskManager.Models
{
    public class task:BaseEntity
    {
        public long  Id { get; set; }
        [Required]
        public long  ProjectId { get; set; }

        [Required]
        public string  Name { get; set; }

        [Required]
        public Priority? Priority { get; set; }

        [Required]
        public Development? Development { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime?  EndDate { get; set; }
        [Required]
        public long  EmployeeId { get; set; }
        [Required]
        public string  Description { get; set; }
        [Required]
        public string RequiredDetail { get; set; }
        [Required]
        public string  Recommendation { get; set; }
        
        public string  StatusOfAcievement { get; set; }
        
        public long?  ParentId { get; set; }      

    }
}
