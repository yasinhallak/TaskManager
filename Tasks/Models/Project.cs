using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Model;

namespace TaskManager.Models
{
    public class Project:BaseEntity
    {
        public long  Id { get; set; }
     
        public string  Name { get; set; }

        public long  CustomerId { get; set; }

        public  virtual Customer Customer { get; set; }          
    }
}
