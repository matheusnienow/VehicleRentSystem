using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace VRS.Model.Repository
{
    public interface IRepository<T>
    {
        int Insert(T entity);
        int Insert(ICollection<T> entities);
        int Delete(T entity);
        int Delete(ICollection<T> entities);
        int Update(T entity);

        IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate);
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(params Expression<Func<T, object>>[] navigationProperties);        
        T GetById(int id);
        void Dispose();

        IQueryable<T> GetItems(Expression<Func<T, bool>> predicate, params string[] navigationProperties);
        IQueryable<T> GetItems(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] navigationProperties);
        IQueryable<T> Include(Expression<Func<T, object>> predicate);
    }
}
