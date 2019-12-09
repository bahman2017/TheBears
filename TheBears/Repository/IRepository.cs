using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheBears.Repository
{
    public interface IRepository<T>
    {
        IEnumerable<T> List();
        IEnumerable<T> List(int page,int size,string sort,out int totalrow);
        bool Add(T entity);
        bool Delete(int id);
        bool Update(T entity);
    }
}
