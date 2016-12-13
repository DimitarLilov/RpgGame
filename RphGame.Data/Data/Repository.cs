using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using RpgGame.Data.Interfaces;

namespace RpgGame.Data.Data
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly RpgGameContext context;

        public Repository(RpgGameContext context)
        {
            this.context = context;
        }

        public T GetById(int id)
        {
            return this.context.Set<T>().Find(id);
        }

        public void Add(T entity)
        {
            this.context.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            this.context.Set<T>().AddRange(entities);
        }

        public void Delete(T entity)
        {
            this.context.Set<T>().Remove(entity);
        }

        public void DeleteRange(IEnumerable<T> entities)
        {
            this.context.Set<T>().RemoveRange(entities);
        }

        public IEnumerable<T> GetAll()
        {
            return this.context.Set<T>();
        }

        public T GetFirst(Expression<Func<T, bool>> predicate)
        {
            return this.GetAll(predicate).FirstOrDefault();
        }

        public IEnumerable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return this.context.Set<T>().Where(predicate);
        }
    }
}