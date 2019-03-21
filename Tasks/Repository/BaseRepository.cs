using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using TaskManager.Data;

namespace TaskManager.Repository
{

    public class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : class
    {
        protected DbSet<TEntity> dbset;

        private readonly ApplicationDbContext _context;

        public BaseRepository(ApplicationDbContext context)
        {
            _context = context;
            dbset = _context.Set<TEntity>();
        }
        public BaseRepository()
        {
        }
        public virtual TEntity AddToDb(TEntity entity)
        {
            var addedEntity = Add(entity);

            _context.SaveChanges();

            return addedEntity;
        }
        public virtual TEntity Add(TEntity entity)
        {
            return dbset.Add(entity)?.Entity;
        }
        public List<TEntity> GetAll()
        {
            return dbset.ToList();
        }

        public IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> whereCondition)
        {
            return dbset.Where(whereCondition).AsEnumerable();
        }
        public async Task<TEntity> GetByIdAsync(object id)
        {
            return await dbset.FindAsync(id);
        }

        public virtual TEntity Find(params object[] id)
        {
            return _context.Set<TEntity>().Find(id);
        }
   
        public async Task InserAsync(TEntity entity)
        {
            dbset.Add(entity);
            await _context.SaveChangesAsync();
        }
        
        public TEntity SingleOrDefault(Expression<Func<TEntity, bool>> whereCondition)
        {
            var dbResult = dbset.Where(whereCondition).FirstOrDefault();
            return dbResult;
        }

        public async Task UpdateAsync(TEntity entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }
        public async Task DeleteAsync(object id)
        {
            var cours = dbset.Find(id);
            dbset.Remove(cours);
            await _context.SaveChangesAsync();
        }
      
        public async Task TrushAsync()
        {
            await _context.SaveChangesAsync();
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> whereCondition)
        {
            var dbResult = dbset.FirstOrDefault(whereCondition);
            return dbResult;
        }

        public bool Exists(Expression<Func<TEntity, bool>> whereCondition)
        {
            return dbset.Any(whereCondition);
        }
    }

}
