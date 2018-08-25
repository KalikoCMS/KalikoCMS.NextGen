namespace KalikoCMS.Services.Content {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core;
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

            propertyCollection = AddToCache(contentId, languageId, properties);

            return propertyCollection;
        }

        public void Preload() {
            var properties = _propertyRepository.LoadAllProperties(CreateProperty);

            var groupedProperties = properties.GroupBy(x => (ContentId: x.ContentId, LanguageId: x.LanguageId));
            foreach (var propertyGroup in groupedProperties) {
                AddToCache(propertyGroup.Key.ContentId, propertyGroup.Key.LanguageId, new List<PropertyData>(propertyGroup.ToList()));
            }
        }

        public object CreateProperty(Guid propertyTypeId, string serializedValue) {
            // TODO: Add error handling
            var contentType = _propertyTypeResolver.GetPropertyType(propertyTypeId);
            var value = JsonSerialization.DeserializeJson(serializedValue, contentType.Type);

            return value;
        }

        private PropertyCollection AddToCache(Guid contentId, int languageId, List<PropertyData> properties) {
            var propertyCollection = new PropertyCollection {
                Properties = properties
            };

            _memoryCache.Set($"properties/{contentId}/{languageId}", propertyCollection);
            return propertyCollection;
        }
    }
}