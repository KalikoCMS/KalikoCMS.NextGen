namespace KalikoCMS.Mappers {
    using Core;
    using Data.Entities;
    using Interfaces;

    public class ContentTypeMapper : IContentTypeMapper {
        public ContentType Map(ContentTypeEntity source) {
            return new ContentType {
                ContentProviderId = source.ContentProviderId,
                ContentTypeId = source.ContentTypeId,
                DefaultChildSortDirection = source.DefaultChildSortDirection,
                DefaultChildSortOrder = source.DefaultChildSortOrder,
                Description = source.Description,
                DisplayName = source.DisplayName,
                Name = source.Name,
                ShowInAdmin = source.ShowInAdmin
            };
        }

        public ContentTypeEntity Map(ContentType source) {
            return new ContentTypeEntity {
                ContentProviderId = source.ContentProviderId,
                ContentTypeId = source.ContentTypeId,
                DefaultChildSortDirection = source.DefaultChildSortDirection,
                DefaultChildSortOrder = source.DefaultChildSortOrder,
                Description = source.Description,
                DisplayName = source.DisplayName,
                Name = source.Name,
                ShowInAdmin = source.ShowInAdmin
            };
        }
    }
}