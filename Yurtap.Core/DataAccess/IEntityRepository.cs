using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Yurtap.Core.Entity;

namespace Yurtap.Core.DataAccess
{
    public interface IEntityRepository<T> where T : class, new()
    {
        IQueryable<T> GetList(Expression<Func<T, bool>> filter = null);
        T Get(Expression<Func<T, bool>> filter);
        T Add(T entity);
        Task<T> AddAsync(T entity);
        T Update(T entity);
        int Delete(T entity);
        int DeleteAll(List<T> entities);
        bool Any(Expression<Func<T, bool>> filter);
    }
}
