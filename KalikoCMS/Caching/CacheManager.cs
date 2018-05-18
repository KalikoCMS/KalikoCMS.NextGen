namespace KalikoCMS.Caching {
    using System;
    using Interfaces;

    public static class CacheManager {
        private static readonly ICacheProvider CacheProvider = GetCacheProviderTypeFromConfig();

        public static void Add<T>(string key, T value, CachePriority priority = CachePriority.Medium, int timeout = 30, bool slidingExpiration = true, bool addRefreshDependency = false) {
            CacheProvider.Add(key, value, priority, timeout, slidingExpiration, addRefreshDependency);
        }

        public static bool Exists(string key) {
            return CacheProvider.Exists(key);
        }

        public static T Get<T>(string key) {
            return CacheProvider.Get<T>(key);
        }

        public static void Remove(string key) {
            CacheProvider.Remove(key);
        }

        public static void RemoveRelated(Guid pageId) {
            CacheProvider.RemoveRelated(pageId);
        }

        private static ICacheProvider GetCacheProviderTypeFromConfig() {
            var cacheProvider = string.Empty; // TODO: SiteSettings.Instance.CacheProvider;

            if (string.IsNullOrEmpty(cacheProvider)) {
                //TODO: return new WebCache();
            }

            var cacheProviderType = Type.GetType(cacheProvider);
            if (cacheProviderType == null) {
                throw new NullReferenceException("Type.GetType(" + cacheProvider + ") returned null.");
            }

            return (ICacheProvider)Activator.CreateInstance(cacheProviderType);
        }
    }
}