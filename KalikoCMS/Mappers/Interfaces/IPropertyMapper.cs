namespace KalikoCMS.Mappers.Interfaces {
    using Core;
    using Data.Entities;

    public interface IPropertyMapper : IMapper<PropertyEntity, PropertyDefinition>, IMapper<PropertyDefinition, PropertyEntity> { }
}