namespace KalikoCMS.Services.Content {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AssemblyHelpers;
    using Attributes;
    using Core;
    using Initialization;
    using Interfaces;
    using Logging;

    public class PropertyTypeResolver : IPropertyTypeResolver {
        private static readonly List<PropertyType> _propertyTypes;
        private static readonly ILog Logger = LogProvider.For<PropertyTypeResolver>();

        static PropertyTypeResolver() {
            _propertyTypes = new List<PropertyType>();
            RegisterKnownTypes();
        }

        private static void RegisterKnownTypes() {
            RegisterKnownType<string>(new Guid("db6e2d18-ffd3-4377-8ced-11a4d34646ef"));

            RegisterLegacyTypes();
        }

        private static void RegisterLegacyTypes() {
            var typesWithAttribute = AttributeReader.GetTypesWithAttribute(typeof(PropertyTypeAttribute));
            foreach (var type in typesWithAttribute) {
                var customAttributes = type.GetCustomAttributes(typeof(PropertyTypeAttribute), false);
                if (!customAttributes.Any()) {
                    throw new Exception($"Property type '{type.Name}' is missing the Property attribute!");
                }

                var customAttribute = (PropertyTypeAttribute)customAttributes[0];
                RegisterKnownType(type, customAttribute);
            }
        }

        private static void RegisterKnownType(Type type, PropertyTypeAttribute attribute) {
            var propertyType = new PropertyType {
                PropertyTypeId = new Guid(attribute.PropertyTypeId),
                Name = attribute.Name,
                Class = type.FullName,
                Type = type
            };
            _propertyTypes.Add(propertyType);
        }

        private static void RegisterKnownType<T>(Guid propertyTypeId) {
            var type = typeof(T);
            var propertyType = new PropertyType {
                PropertyTypeId = propertyTypeId,
                Name = type.Name,
                Type = type
            };
            _propertyTypes.Add(propertyType);
        }

        public PropertyType GetOrCreate(Type type) {
            var propertyType = _propertyTypes.FirstOrDefault(x => x.Name == type.Name);

            if (propertyType == null) {
                propertyType = new PropertyType {
                    Name = type.Name,
                    PropertyTypeId = Guid.NewGuid(),
                    Type = type
                };
                var propertyTypeSynchronizer = new PropertyTypeSynchronizer();
                propertyTypeSynchronizer.AddPropertyType(propertyType);
            }
            else if (propertyType.Type == null) {
                propertyType.Type = type;
            }

            return propertyType;
        }

        public PropertyType GetPropertyType(Guid propertyTypeId) {
            return _propertyTypes.FirstOrDefault(pt => pt.PropertyTypeId == propertyTypeId);
        }

        public PropertyType GetPropertyType(Type propertyType) {
            return _propertyTypes.FirstOrDefault(pt => pt.Type == propertyType);
        }

        public PropertyType GetPropertyTypeByClassName(string className) {
            return _propertyTypes.FirstOrDefault(pt => pt.Class == className);
        }

        public void Initialize() {
            var propertyTypeSynchronizer = new PropertyTypeSynchronizer();
            propertyTypeSynchronizer.Synchronize(_propertyTypes);
        }
    }
}