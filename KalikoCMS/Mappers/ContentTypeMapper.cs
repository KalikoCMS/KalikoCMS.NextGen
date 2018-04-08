namespace KalikoCMS.Mappers {
    using Core;
    using Data.Entities;
    using Interfaces;

    public class ContentTypeMapper : IMapper<ContentTypeEntity, ContentType>, IMapper<ContentType, ContentTypeEntity> {
        public ContentType Map(ContentTypeEntity source) {
            return new ContentType {
                Description = source.Description,
                ContentTypeId = source.ContentTypeId,
                DefaultChildSortDirection = source.DefaultChildSortDirection,
                DefaultChildSortOrder = source.DefaultChildSortOrder,
                DisplayName = source.DisplayName,
                Name = source.Name,
                ContentProviderId = source.ContentProviderId,
                ShowInAdmin = source.ShowInAdmin
            };
        }

        public ContentTypeEntity Map(ContentType source) {
            return new ContentTypeEntity {
                Description = source.Description,
                ContentTypeId = source.ContentTypeId,
                DefaultChildSortDirection = source.DefaultChildSortDirection,
                DefaultChildSortOrder = source.DefaultChildSortOrder,
                DisplayName = source.DisplayName,
                Name = source.Name,
                ContentProviderId = source.ContentProviderId,
                ShowInAdmin = source.ShowInAdmin,
                Class = source.GetType().Name
            };
        }
    }
}