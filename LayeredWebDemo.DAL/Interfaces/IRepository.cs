using System;
using System.Linq;
using System.Linq.Expressions;

namespace LayeredWebDemo.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T GetById(object id);
        IQueryable<T> GetAll();
        IQueryable<T> Query(Expression<Func<T, bool>> filter);
        void Add(T entity);
        void Update(T entity);
        void Remove(T entity);
    }
}
