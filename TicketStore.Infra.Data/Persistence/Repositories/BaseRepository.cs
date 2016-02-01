using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using TicketStore.Domain.Common;

namespace TicketStore.Infra.Data.Persistence.Repositories
{
    public abstract class BaseRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DbContext _dbContext;
        private readonly IDbSet<TEntity> _dbSet;

        protected BaseRepository(DbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }

        public void Add(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            _dbSet.Add(entity);
        }

        public void Attach(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            _dbSet.Attach(entity);
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException("entity");

            _dbSet.Remove(entity);
        }
        public void Delete(Expression<Func<TEntity, bool>> predicate)
        {
            var records = Find(predicate);
            foreach (var record in records)
                Delete(record);
        }
        public IEnumerable<TEntity> GetAll()
        {
            return _dbSet.AsEnumerable();
        }

        public IEnumerable<TEntity> GetAllPaged(int page, int pageSize)
        {
            return _dbSet.AsEnumerable().Skip(pageSize).Take(page);
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public IEnumerable<TEntity> FindPaged(int page, int pageSize, Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Where(predicate).Skip(pageSize).Take(page);
        }

        public TEntity FirstOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.Single(predicate);
        }

        public TEntity First(Expression<Func<TEntity, bool>> predicate)
        {
            return _dbSet.First(predicate);
        }

        public int Count()
        {
            return _dbSet.Count();
        }

        public int Count(Expression<Func<TEntity, bool>> criteria)
        {
            return _dbSet.Where(criteria).Count();
        }

        public virtual bool Any()
        {
            return _dbSet.Any();
        }

        public virtual bool Any(Expression<Func<TEntity, bool>> criteria)
        {
            return _dbSet.Any(criteria);
        }

        public virtual bool All(Expression<Func<TEntity, bool>> criteria)
        {
            return _dbSet.All(criteria);
        }

        #region IDisposable

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposing) return;
            if (_dbContext == null) return;
            _dbContext.Dispose();
        }

        #endregion
    }
}
