using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Threading.Tasks;

namespace TaskManager.Models
{
    public class ProjectUser
    {
        public  long Id { get; set; }

        public  long ProjectId { get; set; }

        public  long UserId { get; set; }
    }
}
