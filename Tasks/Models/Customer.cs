using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Enums;
using TaskManager.Model;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace TaskManager.Models
{
    public class Customer:BaseEntity
    {
        public long  Id { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public Gender? Gender { get; set; }

        public Nationality1? Nationality1 { get; set; }

        [Required]
        public Country?  Country { get; set; }

        [Required]
        public long  CityId { get; set; }

        public virtual City City { get; set; }
        [Required]
        public string  Email { get; set; }
        [Required]
        public string Mobile { get; set; }

        public string  Address { get; set; }

        public string  Notes { get; set; }

    }
}
