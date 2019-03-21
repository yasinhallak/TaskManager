using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskManager.Data;
using TaskManager.Models;

namespace TaskManager.Repository
{
    public class CityRepository:BaseRepository<City>
    {
        public CityRepository(ApplicationDbContext context):base(context)
        {
                
        }
    }
}
