namespace KalikoCMS.Services.Initialization {
    using System.Collections.Generic;
    using System.Linq;
    using AssemblyHelpers;
    using Attributes;
    using ContentProviders;
    using Core;
    using Data.Entities;
    using Data.Repositories.Interfaces;
    using Logging;
    using Mappers;
    using Microsoft.EntityFrameworkCore;

    internal class ContentTypeSynchronizer {
        private static readonly ILog Logger = LogProvider.For<ContentTypeSynchronizer>();

        #region Synchronize page types

        internal List<ContentType> SynchronizeContentTypes() {
            var propertySynchronizer = new PropertySynchronizer();

            var contentTypeRepository = ServiceLocator.Current.GetInstance<IContentTypeRepository>();
            var contentTypeMapper = new ContentTypeMapper();

            var contentTypeEntities = contentTypeRepository.GetAll().Include(x => x.Properties).ToList();
            var contentTypes = new List<ContentType>();
            var typesWithAttribute = AttributeReader.GetTypesWithAttribute(typeof(PageTypeAttribute)).ToList();

            foreach (var type in typesWithAttribute) {
                var attribute = AttributeReader.GetAttribute<PageTypeAttribute>(type);

                var contentTypeEntity = contentTypeEntities.SingleOrDefault(x => x.ContentTypeId == attribute.UniqueId);

                var isNew = false;
                if (contentTypeEntity == null) {
                    contentTypeEntity = new ContentTypeEntity {
                        ContentTypeId = attribute.UniqueId,
                        ContentProviderId = PageContentProvider.UniqueId
                    };
                    contentTypeEntities.Add(contentTypeEntity);
                    isNew = true;
                }

                contentTypeEntity.DefaultChildSortDirection = attribute.DefaultChildSortDirection;
                contentTypeEntity.DefaultChildSortOrder = attribute.DefaultChildSortOrder;
                contentTypeEntity.DisplayName = attribute.DisplayName;
                contentTypeEntity.Description = attribute.PageTypeDescription;
                contentTypeEntity.Name = type.Name;

                if (isNew) {
                    contentTypeRepository.Create(contentTypeEntity);
                }
                else {
                    contentTypeRepository.Update(contentTypeEntity);
                }

                var pageType = contentTypeMapper.Map(contentTypeEntity);
                pageType.Type = type;
                pageType.AllowedTypes = attribute.AllowedTypes;
                pageType.PreviewImage = attribute.PreviewImage;

                contentTypes.Add(pageType);

                Logger.Info(" - sync " + pageType.Name);

                propertySynchronizer.SynchronizeProperties(pageType, type, contentTypeEntity.Properties);
            }

            return contentTypes;
        }

        #endregion
    }
}
