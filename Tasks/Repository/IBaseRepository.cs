using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace TaskManager.Repository
{
  public interface IBaseRepository<TEntity>
    {
        List<TEntity> GetAll();
        IEnumerable<TEntity> GetAll(Expression<Func<TEntity, bool>> whereCondition);
        Task<TEntity> GetByIdAsync(object id);
        TEntity SingleOrDefault(Expression<Func<TEntity, bool>> whereCondition);
        Task InserAsync(TEntity entity);
        TEntity FirstOrDefault(Expression<Func<TEntity, bool>> whereCondition);
        Task UpdateAsync(TEntity entity);
        Task DeleteAsync(object id);
        bool Exists(Expression<Func<TEntity, bool>> whereCondition);

    }
}
