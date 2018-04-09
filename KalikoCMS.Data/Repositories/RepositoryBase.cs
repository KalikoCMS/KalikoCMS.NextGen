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

        public void Create(TEntity entity) {
            _cmsContext.Set<TEntity>().Add(entity);
            _cmsContext.SaveChanges();
        }

        public void Update(TEntity entity) {
            _cmsContext.Set<TEntity>().Update(entity);
            _cmsContext.SaveChangesAsync();
        }

        public async Task Delete(TKey id) {
            var entity = await GetById(id);
            _cmsContext.Set<TEntity>().Remove(entity);
            await _cmsContext.SaveChangesAsync();
        }

        public TEntity FirstOrDefault(Func<TEntity, bool> predicate) {
            return _cmsContext.Set<TEntity>().FirstOrDefault(predicate);
        }

        public IEnumerable<TEntity> Select(Func<TEntity, bool> predicate) {
            return _cmsContext.Set<TEntity>().Where(predicate);
        }

        public void Delete(Func<TEntity, bool> predicate) {
            var set = _cmsContext.Set<TEntity>();
            var entities = set.Where(predicate);
            set.RemoveRange(entities);
            _cmsContext.SaveChanges();
        }

        public TEntity SingleOrDefault(Func<TEntity, bool> predicate) {
            return _cmsContext.Set<TEntity>().SingleOrDefault(predicate);
        }
    }
}
