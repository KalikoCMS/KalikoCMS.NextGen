namespace KalikoCMS.Data.Repositories.Interfaces {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    public interface IRepository<TEntity, in TKey> where TEntity : class {
        TEntity FirstOrDefault(Func<TEntity, bool> predicate);
        IQueryable<TEntity> GetAll();
        Task<TEntity> GetById(TKey id);
        Task Create(TEntity entity);
        Task Delete(TKey id);
        void Delete<T>(Func<T, bool> predicate) where T : class;
        IEnumerable<T> Select<T>(Func<T, bool> predicate) where T : class;
        T SingleOrDefault<T>(Func<T, bool> predicate) where T : class;
        Task Update(TKey id, TEntity entity);
    }
}