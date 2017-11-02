using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Validation;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using VRS.Model;

namespace VRS.Model.Repository
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        internal VRSModel dbContext;
        internal DbSet<T> dbSet;

        public static Repository<T> GetInstance()
        {
            return new Repository<T>(new VRSModel());
        }

        public Repository(VRSModel dataContext)
        {
            this.dbContext = dataContext;
            this.dbSet = dbContext.Set<T>();
        }

        public int Insert(T entity)
        {
            dbSet.Add(entity);
            return SaveChanges(dbContext);
        }

        public int Insert(ICollection<T> entities)
        {
            entities.ToList().ForEach(e => dbSet.Add(e));
            return SaveChanges(dbContext);
        }

        public int Delete(T entity)
        {
            dbSet.Remove(entity);
            return SaveChanges(dbContext);
        }

        public int Delete(ICollection<T> entities)
        {
            entities.ToList().ForEach(e => dbSet.Remove(e));
            return SaveChanges(dbContext);
        }

        public IQueryable<T> SearchFor(Expression<Func<T, bool>> predicate)
        {
            return dbSet.Where(predicate);
        }

        public IQueryable<T> GetAll()
        {
            return dbSet;
        }

        public T GetById(int id)
        {
            return dbSet.Single(e => e.Id.Equals(id));
        }

        public int Update(T entity)
        {
            dbContext.Entry(entity).State = EntityState.Modified;
            return SaveChanges(dbContext);
        }

        public void Dispose()
        {
            dbContext.Dispose();
        }

        public IQueryable<T> GetItems(Expression<Func<T, bool>> predicate, params string[] navigationProperties)
        {
            var query = dbSet.AsQueryable();

            foreach (string navigationProperty in navigationProperties)
                query = query.Include(navigationProperty);//got to reaffect it.
            return query.Where(predicate);
        }

        public IQueryable<T> GetItems(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] navigationProperties)
        {
            var query = dbSet.AsQueryable();
            foreach (var navigationProperty in navigationProperties)
                query = query.Include(navigationProperty);
            return query.Where(predicate);
        }

        public IQueryable<T> GetAll(params Expression<Func<T, object>>[] navigationProperties)
        {
            var query = dbSet.AsQueryable();

            foreach (var navigationProperty in navigationProperties)
                query = query.Include(navigationProperty);

            return query;
        }

        public IQueryable<T> Include(Expression<Func<T, object>> predicate)
        {
            
            var query = dbSet.AsQueryable();
            return  query.Include(predicate);
        }

        private int SaveChanges(VRSModel db)
        {
            try
            {
                return db.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entidade do tipo \"{0}\" no estado \"{1}\" tem os seguintes erros de validação:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Erro: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }
    }
}
