using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Enums;
using TaskManager.Model;

namespace TaskManager.Models
{
    public class Notfication:BaseEntity
    {
        public long  Id { get; set; }
        public TableName TableName { get; set; }
        [Required]
        public long TablePkId { get; set; }

        public string Context { get; set; }

        public string OwnerUserId { get; set; }

        public string ToUserId { get; set; }

        public bool IsRead { get; set; }
    }
}
