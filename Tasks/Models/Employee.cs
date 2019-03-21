using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Enums;
using TaskManager.Model;

namespace TaskManager.Models
{
    public class Employee:BaseEntity
    {
        public long  Id { get; set; }

        public DateTime?  BirthDate { get; set; }

        public Country? Country { get; set; }

        public string  Address { get; set; }

        public long  CityId { get; set; }

        public string Notes { get; set; }

        public virtual City City { get; set; }

        public Nationality1? Nationality1 { get; set; }

        public string  UserId { get; set; }

        public virtual  ApplicationUser  User { get; set; }

    }
}
