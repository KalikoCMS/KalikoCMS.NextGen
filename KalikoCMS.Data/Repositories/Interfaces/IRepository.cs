namespace KalikoCMS.Data.Repositories.Interfaces {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IRepository<TEntity, in TKey> where TEntity : class {
        TEntity FirstOrDefault(Func<TEntity, bool> predicate);
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetById(TKey id);
        void Create(TEntity entity);
        Task Delete(TKey id);
        void Delete(Func<TEntity, bool> predicate);
        IEnumerable<TEntity> Select(Func<TEntity, bool> predicate);
        TEntity SingleOrDefault(Func<TEntity, bool> predicate);
        void Update(TEntity entity);
    }
}