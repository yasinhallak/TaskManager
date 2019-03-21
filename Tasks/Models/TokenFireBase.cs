using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Model;

namespace TaskManager.Models
{
    public class TokenFireBase:BaseEntity
    {
        public long  Id { get; set; }
        [Required]
        public string  token { get; set; }
        [Required]
        public string UserId { get; set; }
    }
}
