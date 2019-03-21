using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Data.ViewModel
{
    public class TaskViewModel
    {
        public long ParentId { get; set; }
        public IEnumerable<TaskVM> Items { get; set; }
    }
}
