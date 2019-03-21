using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Enums;
using TaskManager.Model;

namespace TaskManager.Models
{
    public class Comment:BaseEntity
    {
        public long  Id { get; set; }

        public TableName  TableName { get; set; }
        [Required]
        public long  TableId { get; set; }
        [Required]
        public string  context { get; set; }

        public string  UserId { get; set; }

    }
}
