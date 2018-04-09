namespace KalikoCMS.Services.Initialization {
    using System.Collections.Generic;
    using System.Linq;
    using Core;
    using Data.Entities;
    using Data.Repositories.Interfaces;

    internal class PropertyTypeSynchronizer {
        private readonly IPropertyTypeRepository _propertyTypeRepository;

        public PropertyTypeSynchronizer() {
            _propertyTypeRepository = ServiceLocator.Current.GetInstance<IPropertyTypeRepository>();
        }

        public void Synchronize(List<PropertyType> propertyTypes) {
            var storedTypes = _propertyTypeRepository.GetAll().ToList();

            foreach (var propertyType in propertyTypes) {
                if (storedTypes.All(x => x.PropertyTypeId != propertyType.PropertyTypeId)) {
                    AddPropertyType(propertyType);
                }
            }

            foreach (var propertyType in storedTypes) {
                if (propertyTypes.All(x => x.PropertyTypeId != propertyType.PropertyTypeId)) {
                    propertyTypes.Add(new PropertyType {
                        PropertyTypeId = propertyType.PropertyTypeId,
                        Name = propertyType.Name,
                        Class = propertyType.Class
                    });
                }
            }
        }

        public void AddPropertyType(PropertyType propertyType) {
            var propertyTypeEntity = new PropertyTypeEntity {
                Name = propertyType.Name,
                Class = propertyType.Class,
                PropertyTypeId = propertyType.PropertyTypeId
            };

            _propertyTypeRepository.Create(propertyTypeEntity);
        }
    }
}