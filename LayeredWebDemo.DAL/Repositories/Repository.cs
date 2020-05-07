using LayeredWebDemo.DAL.Entities;
using LayeredWebDemo.DAL.Interfaces;
using System;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Data.Entity.Migrations;

namespace LayeredWebDemo.DAL.Repositories
{
    public abstract class Repository<T> : IRepository<T>
       where T : class
    {
        protected DbSet<T> _objectSet;

        public Repository(ApplicationDbContext context)
        {
            _objectSet = context.Set<T>();
        }

        #region IRepository<T> Members

        public abstract T GetById(object id);

        public IQueryable<T> GetAll()
        {
            return _objectSet;
        }

        public IQueryable<T> Query(Expression<Func<T, bool>> filter)
        {
            return _objectSet.Where(filter);
        }

        public void Add(T entity)
        {
            _objectSet.Add(entity);
        }

        public void Update(T entity)
        {
            _objectSet.AddOrUpdate(entity);
        }

        public void Remove(T entity)
        {
            _objectSet.Remove(entity);
        }
        #endregion
    }
}