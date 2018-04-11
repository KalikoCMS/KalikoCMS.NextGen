namespace KalikoCMS.Services.Initialization {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AssemblyHelpers;
    using Attributes;
    using ContentProviders;
    using Core;
    using Core.Collections;
    using Data.Entities;
    using Data.Repositories.Interfaces;
    using Logging;
    using Mappers;
    using Microsoft.EntityFrameworkCore;
    using ServiceLocation;

    internal class ContentTypeSynchronizer {
        private static readonly ILog Logger = LogProvider.For<ContentTypeSynchronizer>();

        private readonly IContentTypeRepository _contentTypeRepository;
        private readonly PropertySynchronizer _propertySynchronizer;
        private readonly ContentTypeMapper _contentTypeMapper;

        public ContentTypeSynchronizer() {
            _contentTypeRepository = ServiceLocator.Current.GetInstance<IContentTypeRepository>();
            _propertySynchronizer = new PropertySynchronizer();
            _contentTypeMapper = new ContentTypeMapper();
        }

        #region Synchronize content types

        internal List<ContentType> SynchronizeContentTypes() {
            var contentTypeEntities = _contentTypeRepository.GetAll().Include(x => x.Properties).ToList();
            var contentTypes = new List<ContentType>();

            SynchronizePageTypes(contentTypeEntities, contentTypes);
            SynchronizeSiteType(contentTypeEntities, contentTypes);

            return contentTypes;
        }

        #region Synchronize page types

        private void SynchronizePageTypes(ICollection<ContentTypeEntity> contentTypeEntities, ICollection<ContentType> contentTypes) {
            var typesWithAttribute = AttributeReader.GetTypesWithAttribute(typeof(PageTypeAttribute)).ToList();

            foreach (var type in typesWithAttribute) {
                if (!typeof(CmsPage).IsAssignableFrom(type)) {
                    var message = $"Type '{type}' is decorated by PageTypeAttribute but doesn't inherit the CmsPage class.";
                    Logger.Error(message);
                    throw new Exception(message);
                }

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
                    _contentTypeRepository.Create(contentTypeEntity);
                }
                else {
                    _contentTypeRepository.Update(contentTypeEntity);
                }

                var contentType = _contentTypeMapper.Map(contentTypeEntity);
                contentType.Type = type;
                contentType.AllowedTypes = attribute.AllowedTypes;
                contentType.PreviewImage = attribute.PreviewImage;

                contentTypes.Add(contentType);

                Logger.Info(" - sync page type " + contentType.Name);

                _propertySynchronizer.SynchronizeProperties(contentType, type, contentTypeEntity.Properties);
            }
        }

        #endregion Synchronize page types


        #region Synchronize site type

        private void SynchronizeSiteType(ICollection<ContentTypeEntity> contentTypeEntities, ICollection<ContentType> contentTypes) {
            var typesWithAttribute = AttributeReader.GetTypesWithAttribute(typeof(SiteSettingsAttribute)).ToList();

            if (typesWithAttribute.Count > 1) {
                const string message = "More than one class decorated with SiteSettingsAttribute was found, only one allowed";
                Logger.Error(message);
                throw new Exception(message);
            }

            var type = typesWithAttribute.FirstOrDefault();
            if (type == null) {
                type = typeof(CmsSite);
            }

            if (!typeof(CmsSite).IsAssignableFrom(type)) {
                var message = $"Type '{type}' is decorated by SiteSettingsAttribute but doesn't inherit the CmsSite class.";
                Logger.Error(message);
                throw new Exception(message);
            }

            var attribute = AttributeReader.GetAttribute<SiteSettingsAttribute>(type);

            var contentTypeEntity = contentTypeEntities.SingleOrDefault(x => x.ContentTypeId == SiteContentProvider.SiteContentTypeId);

            var isNew = false;
            if (contentTypeEntity == null) {
                contentTypeEntity = new ContentTypeEntity {
                    ContentTypeId = SiteContentProvider.SiteContentTypeId,
                    ContentProviderId = SiteContentProvider.UniqueId
                };
                contentTypeEntities.Add(contentTypeEntity);
                isNew = true;
            }

            if (attribute != null) {
                contentTypeEntity.DefaultChildSortDirection = attribute.DefaultChildSortDirection;
                contentTypeEntity.DefaultChildSortOrder = attribute.DefaultChildSortOrder;
            }
            else {
                contentTypeEntity.DefaultChildSortDirection = SortDirection.Ascending;
                contentTypeEntity.DefaultChildSortOrder = SortOrder.SortIndex;
            }
            contentTypeEntity.DisplayName = "Site";
            contentTypeEntity.Description = "CMS driven website";
            contentTypeEntity.Name = type.Name;

            if (isNew) {
                _contentTypeRepository.Create(contentTypeEntity);
            }
            else {
                _contentTypeRepository.Update(contentTypeEntity);
            }

            var contentType = _contentTypeMapper.Map(contentTypeEntity);
            contentType.Type = type;

            if (attribute != null) {
                contentType.AllowedTypes = attribute.AllowedTypes;
            }

            contentTypes.Add(contentType);

            Logger.Info(" - sync site type " + contentType.Name);

            _propertySynchronizer.SynchronizeProperties(contentType, type, contentTypeEntity.Properties);

        }

        #endregion Synchronize site type

        #endregion
    }
}
