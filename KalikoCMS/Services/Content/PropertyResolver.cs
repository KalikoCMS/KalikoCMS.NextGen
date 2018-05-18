namespace KalikoCMS.Services.Content {
    using System;
    using Core.Collections;
    using Core.Interfaces;
    using Data.Repositories.Interfaces;
    using Interfaces;
    using Microsoft.Extensions.Caching.Memory;
    using Serialization;

    public class PropertyResolver : IPropertyResolver {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IPropertyTypeResolver _propertyTypeResolver;
        private readonly IMemoryCache _memoryCache;

        public PropertyResolver(IPropertyRepository propertyRepository, IPropertyTypeResolver propertyTypeResolver, IMemoryCache memoryCache) {
            _propertyRepository = propertyRepository;
            _propertyTypeResolver = propertyTypeResolver;
            _memoryCache = memoryCache;
        }

        public PropertyCollection GetProperties(Guid contentId, int languageId, Guid contentTypeId, int version, bool useCache) {
            // TODO: Implement cache using a version key and remove from cache when page is made editable (to prevent writeback to cache)
            if (_memoryCache.TryGetValue($"properties/{contentId}/{languageId}", out PropertyCollection propertyCollection)) {
                return propertyCollection;
            }

            var properties = _propertyRepository.LoadProperties(contentId, languageId, contentTypeId, version, CreateProperty);

            propertyCollection = new PropertyCollection {
                Properties = properties
            };

            _memoryCache.Set($"properties/{contentId}/{languageId}", propertyCollection);

            return propertyCollection;
        }

        public object CreateProperty(Guid propertyTypeId, string serializedValue) {
            // TODO: Add error handling
            var contentType = _propertyTypeResolver.GetPropertyType(propertyTypeId);
            var value = JsonSerialization.DeserializeJson(serializedValue, contentType.Type);

            return value;
        }
    }
}
