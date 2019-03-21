using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Enums;
using TaskManager.Model;
using TaskManager.Models;

namespace TaskManager.Data.ViewModel
{
    public class ReportVM:BaseEntity
    {
        public IEnumerable<EmployeeVM> ListEmployees { get; set; }
        public IEnumerable<CustomerVM> ListCustomers { get; set; }
        public IEnumerable<ProjectVM> ListProjects { get; set; }
        public IEnumerable<TaskVM> ListTasks { get; set; }

        public long?  EmployeeId { get; set; }

        public  Employee Employee { get; set; }

        public long? CustomerId { get; set; }

        public Customer Customer { get; set; }
        public long? ProjectId { get; set; }

        public Project Project { get; set; }
        public long? TaskId { get; set; }

        public TaskVM Taskvm { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }
        public Development? Development { get; set; }


  
    }
}
