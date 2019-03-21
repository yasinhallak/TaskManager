using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Repository
{
    public class TaskRepository:BaseRepository<task>
    {
        public TaskRepository(ApplicationDbContext context):base(context)
        {
            
        }
    }
}
