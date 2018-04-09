namespace KalikoCMS.Services.Content.Interfaces {
    using System;
    using Core;

    public interface IPropertyTypeResolver {
        PropertyType GetOrCreate(Type propertyType);
        PropertyType GetPropertyType(Guid propertyTypeId);
        PropertyType GetPropertyType(Type propertyType);
        PropertyType GetPropertyTypeByClassName(string className);
        void Initialize();
    }
}