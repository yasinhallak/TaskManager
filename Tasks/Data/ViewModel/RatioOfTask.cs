using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskManager.Data.ViewModel
{
    public class RatioOfTask
    {
        public int  Done { get; set; }
        public int  WorkingOn { get; set; }

        public int  Stuck { get; set; }
    }
}
