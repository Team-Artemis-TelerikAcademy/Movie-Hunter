using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MovieHunter.Services.Contracts
{
    public interface IRepository<T>
    {
        void Add(T entity);
        IQueryable<T> All();
        void Update(T entity);
        void Delete(object id);
        void Delete(T entity);
        T Find(object id);
        int SaveChanges();
    }
}
