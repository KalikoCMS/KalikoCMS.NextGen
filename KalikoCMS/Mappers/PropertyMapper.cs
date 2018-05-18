namespace KalikoCMS.Mappers {
    using Core;
    using Data.Entities;
    using Interfaces;

    public class PropertyMapper : IPropertyMapper {
        public PropertyDefinition Map(PropertyEntity source) {
            return new PropertyDefinition {
                PropertyId = source.PropertyId,
                PropertyTypeId = source.PropertyTypeId,
                ContentTypeId = source.ContentTypeId,
                Name = source.Name,
                Header = source.Header,
                Localize = source.Localize,
                SortOrder = source.SortOrder,
                Parameters = source.Parameters,
                Required = source.Required,
                TabGroup = source.TabGroup
            };
        }

        public PropertyEntity Map(PropertyDefinition source) {
            return new PropertyEntity {
                PropertyId = source.PropertyId,
                PropertyTypeId = source.PropertyTypeId,
                ContentTypeId = source.ContentTypeId,
                Name = source.Name,
                Header = source.Header,
                Localize = source.Localize,
                SortOrder = source.SortOrder,
                Parameters = source.Parameters,
                Required = source.Required,
                TabGroup = source.TabGroup
            };
        }
    }
}