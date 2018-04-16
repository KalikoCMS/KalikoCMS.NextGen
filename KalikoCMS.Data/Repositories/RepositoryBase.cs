namespace KalikoCMS.Data.Repositories {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    // TODO: Add error handling
    public abstract class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class {
        private readonly CmsContext _context;

        protected RepositoryBase(CmsContext context) {
            _context = context;
        }

        public IQueryable<TEntity> GetAll() {
            return _context.Set<TEntity>().AsNoTracking();
        }

        public abstract TEntity GetById(TKey id);

        public void Create(TEntity entity) {
            _context.Set<TEntity>().Add(entity);
            _context.SaveChanges();
        }

        public void Update(TEntity entity) {
            _context.Set<TEntity>().Update(entity);
            _context.SaveChanges();
        }

        public void Delete(TKey id) {
            var entity = GetById(id);
            _context.Set<TEntity>().Remove(entity);
            _context.SaveChanges();
        }

        public TEntity FirstOrDefault(Func<TEntity, bool> predicate) {
            return _context.Set<TEntity>().FirstOrDefault(predicate);
        }

        public IEnumerable<TEntity> Select(Func<TEntity, bool> predicate) {
            return _context.Set<TEntity>().Where(predicate);
        }

        public void Delete(Func<TEntity, bool> predicate) {
            var set = _context.Set<TEntity>();
            var entities = set.Where(predicate);
            set.RemoveRange(entities);
            _context.SaveChanges();
        }

        public TEntity SingleOrDefault(Func<TEntity, bool> predicate) {
            return _context.Set<TEntity>().SingleOrDefault(predicate);
        }
    }
}
