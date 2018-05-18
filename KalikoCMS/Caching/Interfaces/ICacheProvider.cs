namespace KalikoCMS.Caching.Interfaces {
    using System;

    public interface ICacheProvider {
        void Add<T>(string key, T value, CachePriority priority, int timeout, bool slidingExpiration, bool addRefreshDependency);
        bool Exists(string key);
        T Get<T>(string key);
        void Remove(string key);
        void RemoveRelated(Guid pageId);
    }
}
