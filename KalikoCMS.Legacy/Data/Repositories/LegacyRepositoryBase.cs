namespace KalikoCMS.Legacy.Data.Repositories {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Linq;
    using System.Linq.Expressions;
    using KalikoCMS.Data.Repositories.Interfaces;
    using Microsoft.EntityFrameworkCore;

    public abstract class LegacyRepositoryBase<TSource, TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class where TSource : class {
        private readonly LegacyDataContext _context;

        protected LegacyRepositoryBase(LegacyDataContext context) {
            _context = context;
        }

        public virtual IQueryable<TEntity> GetAll() {
            return _context.Set<TSource>().Select(Map()).AsNoTracking();
        }

        public abstract TEntity GetById(TKey id);

        public virtual void Create(TEntity entity) {
            throw new NotImplementedException();
        }

        public virtual void Update(TEntity entity) {
            throw new NotImplementedException();
        }

        public virtual void Delete(TKey id) {
            throw new NotImplementedException();
        }

        public virtual TEntity FirstOrDefault(Func<TEntity, bool> predicate) {
            return _context.Set<TSource>().Select(Map()).FirstOrDefault(predicate);
        }

        public virtual IEnumerable<TEntity> Select(Func<TEntity, bool> predicate) {
            return _context.Set<TSource>().Select(Map()).AsEnumerable().Where(predicate);
        }

        public void Delete(Func<TEntity, bool> predicate) {
            throw new NotImplementedException();
        }

        public virtual TEntity SingleOrDefault(Func<TEntity, bool> predicate) {
            return _context.Set<TSource>().Select(Map()).SingleOrDefault(predicate);
        }

        public abstract Expression<Func<TSource, TEntity>> Map();

        public Guid ToGuid(int value) {
            var bytes = new byte[16];
            BitConverter.GetBytes(value).CopyTo(bytes, 0);
            var guid = new Guid(bytes);
            return guid;
        }

        public int ToInt(Guid value) {
            var bytes = new byte[16];
            value.ToByteArray().CopyTo(bytes, 0);
            return BitConverter.ToInt32(bytes, 0);
        }
    }
}