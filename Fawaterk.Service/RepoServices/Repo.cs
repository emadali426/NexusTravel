using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using System.Web;


namespace Fawaterk.Service.RepoServices
{
    public class Repo<TEntity, TKey> : IRepo<TEntity, TKey>
        where TEntity : class
        where TKey : IComparable
    {
        protected readonly DbContext Context;
        public Repo(DbContext context)
        {
            Context = context;
        }
        public virtual void Add(TEntity entity)
        {
            Context.Set<TEntity>().Add(entity);
        }

        public virtual void AddRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().AddRange(entities);
        }

        public virtual IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return Context.Set<TEntity>().Where(predicate).ToList();
        }

        public virtual TEntity Get(TKey id)
        {
            return Context.Set<TEntity>().Find(id);
        }

        public virtual IEnumerable<TEntity> GetAll()
        {
            return Context.Set<TEntity>().ToList();
        }

        public virtual void Remove(TEntity entity)
        {
            Context.Set<TEntity>().Remove(entity);
        }

        public virtual void RemoveRange(IEnumerable<TEntity> entities)
        {
            Context.Set<TEntity>().RemoveRange(entities);
        }

        public virtual int Count(Func<TEntity,bool> predicate)
        {
            return Context.Set<TEntity>().Count(predicate);
        }

        public virtual int Count()
        {
            return Context.Set<TEntity>().Count();
        }
    }
}