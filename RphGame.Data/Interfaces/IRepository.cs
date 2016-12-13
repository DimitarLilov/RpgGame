using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace RpgGame.Data.Interfaces
{
    public interface IRepository<T> where T : class
    {
        T GetById(int id);

        void Add(T entity);

        void AddRange(IEnumerable<T> entities);

        void Delete(T entity);

        void DeleteRange(IEnumerable<T> entities);

        IEnumerable<T> GetAll();

        T GetFirst(Expression<Func<T, bool>> predicate);

        IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate);
    }
}
