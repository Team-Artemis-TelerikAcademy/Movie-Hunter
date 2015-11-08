using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MovieHunter.Services.Contracts;

namespace MovieHunter.Services
{
    public class EfRepository<T> : IRepository<T>
        where T: class 
    {
        private DbContext dbContext;
        private DbSet<T> dbSet;

        public EfRepository(DbContext dbContext)
        {
            this.dbContext = dbContext;
            this.dbSet = dbContext.Set<T>();
        }


        public void Add(T entity)
        {
            var entry = this.dbContext.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                entry.State = EntityState.Added;
            }
            else
            {
                this.dbSet.Add(entity);
            }
        }

        public IQueryable<T> All()
        {
            return this.dbSet.AsQueryable();
        }

        public void Update(T entity)
        {
            var entry = this.dbContext.Entry(entity);
            if (entry.State != EntityState.Detached)
            {
                this.dbSet.Attach(entity);
            }
            entry.State = EntityState.Modified;
        }

        public void Delete(object id)
        {
            var entity = this.Find(id);
            this.Delete(entity);
        }

        public void Delete(T entity)
        {
            var entry = this.dbContext.Entry(entity);
            if (entry.State != EntityState.Deleted)
            {
                entry.State = EntityState.Deleted;
            }
            else
            {
                this.dbSet.Attach(entity);
                this.dbSet.Remove(entity);
            }
        }

        public T Find(object id)
        {
            return this.dbSet.Find(id);
        }

        public int SaveChanges()
        {
            return this.dbContext.SaveChanges();
        }
    }
}
