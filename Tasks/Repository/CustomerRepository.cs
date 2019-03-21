using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data;
using TaskManager.Data.ViewModel;
using TaskManager.Models;

namespace TaskManager.Repository
{
    public class CustomerRepository:BaseRepository<Customer>
    {
        private ApplicationDbContext _context;
        public CustomerRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }

        public CustomerVM GetIncludeReport(long? id)
        {
            return _context.Customers.Where(x => x.Id == id).Select(x => x.ToModel()).SingleOrDefault();
        }

        public bool IsRegisteredEmail(string email)
        {
            return _context.Customers.Any(x => x.Email == email);
        }
    }
}
