namespace KalikoCMS.Data.Repositories {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    // TODO: Add error handling
    public abstract class RepositoryBase<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class {
        private readonly CmsContext _cmsContext;

        protected RepositoryBase(CmsContext cmsContext) {
            _cmsContext = cmsContext;
        }

        public IQueryable<TEntity> GetAll() {
            return _cmsContext.Set<TEntity>().AsNoTracking();
        }

        public abstract Task<TEntity> GetById(TKey id);

        public async Task Create(TEntity entity) {
            await _cmsContext.Set<TEntity>().AddAsync(entity);
            await _cmsContext.SaveChangesAsync();
        }

        public async Task Update(TKey id, TEntity entity) {
            _cmsContext.Set<TEntity>().Update(entity);
            await _cmsContext.SaveChangesAsync();
        }

        public async Task Delete(TKey id) {
            var entity = await GetById(id);
            _cmsContext.Set<TEntity>().Remove(entity);
            await _cmsContext.SaveChangesAsync();
        }

        public TEntity FirstOrDefault(Func<TEntity, bool> predicate) {
            return _cmsContext.Set<TEntity>().FirstOrDefault(predicate);
        }

        public IEnumerable<T> Select<T>(Func<T, bool> predicate) where T : class {
            return _cmsContext.Set<T>().Where(predicate);
        }

        public void Delete<T>(Func<T, bool> predicate) where T : class {
            var set = _cmsContext.Set<T>();
            var entities = set.Where(predicate);
            set.RemoveRange(entities);
            _cmsContext.SaveChanges();
        }

        public T SingleOrDefault<T>(Func<T, bool> predicate) where T : class {
            return _cmsContext.Set<T>().SingleOrDefault(predicate);
        }
    }
}
