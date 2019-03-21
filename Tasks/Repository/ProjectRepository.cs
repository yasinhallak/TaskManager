using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using TaskManager.Data;
using TaskManager.Data.ViewModel;
using Project = TaskManager.Models.Project;

namespace TaskManager.Repository
{
    public class ProjectRepository:BaseRepository<Project>
    {
        private AdamUnit adamUnit = new AdamUnit();
        private readonly ApplicationDbContext _context;
        public ProjectRepository(ApplicationDbContext context):base(context)
        {
            _context = context;
        }

        public IEnumerable<ProjectVM> GetAllInclude()
        {

            return _context.Projects.Where(x => x.IsDeleted == false).Include(x => x.Customer).Select(x => x.ToModel(adamUnit)).ToList();
        }

        //public async Task<ProjectVM> GetIncludeReport(long? id)
        //{
        //    return await _context.Projects.Where(x => x.Id == id).Include(x => x.Customer).Select(x => x.ToModel()).ToList();
        //}
    }
}
