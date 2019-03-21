using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data;
using TaskManager.Data.ViewModel;
using TaskManager.Models;

namespace TaskManager.Repository
{
    public class EmployeeRepository : BaseRepository<Employee>
    {
        private readonly ApplicationDbContext _context;
        public EmployeeRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }

        public List<EmployeeVM> GetAllInclude()
        {
            return _context.Employees.Where(x => x.IsDeleted == false).Include(x => x.User).Select(x => x.ToModel()).ToList();
        }

        public EmployeeVM GetIncludeReport(long? id)
        {
            return _context.Employees.Where(x=>x.Id==id).Select(x => x.ToModel()).SingleOrDefault();
        }


    }

       
}
