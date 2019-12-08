using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheBears.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> List();
        bool Add(T entity);
        bool Delete(T entity);
        bool Update(T entity);
    }
}
