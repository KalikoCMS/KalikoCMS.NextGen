namespace KalikoCMS.Services.Content {
    using System;
    using System.Collections.Generic;
    using Core;
    using Core.Collections;
    using Core.Interfaces;
    using Data.Repositories.Interfaces;
    using Interfaces;
    using Serialization;

    public class PropertyResolver : IPropertyResolver {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IPropertyTypeResolver _propertyTypeResolver;

        public PropertyResolver(IPropertyRepository propertyRepository, IPropertyTypeResolver propertyTypeResolver) {
            _propertyRepository = propertyRepository;
            _propertyTypeResolver = propertyTypeResolver;
        }

        public PropertyCollection GetProperties(Guid contentId, int languageId, Guid contentTypeId, int version, bool useCache) {
            // TODO: Implement cache
            var properties = _propertyRepository.LoadProperties(contentId, languageId, contentTypeId, version, CreateProperty);

            return new PropertyCollection {
                Properties = properties
            };
        }

        public object CreateProperty(Guid propertyTypeId, string serializedValue) {
            // TODO: Add error handling
            var contentType = _propertyTypeResolver.GetPropertyType(propertyTypeId);
            var value = JsonSerialization.DeserializeJson(serializedValue, contentType.Type);

            return value;
        }
    }
}
