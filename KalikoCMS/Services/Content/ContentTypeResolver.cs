namespace KalikoCMS.Services.Content {
    using System;
    using System.Collections.Generic;
    using Interfaces;
    using Core;
    using Initialization;
    using Logging;

    public class ContentTypeResolver : IContentTypeResolver {
        private static readonly ILog Logger = LogProvider.For<ContentTypeResolver>();

        private List<ContentType> _contentTypes;

        public ContentTypeResolver() {
            _contentTypes = new List<ContentType>();
        }

        public ContentType GetContentType(int contentTypeId) {
            return _contentTypes.Find(pt => pt.ContentTypeId == contentTypeId);
        }

        public ContentType GetContentType<T>() where T : class {
            return GetContentType(typeof(T));
        }

        public ContentType GetContentType(Type type) {
            return _contentTypes.Find(pt => pt.Type == type);
        }

        public void Initialize() {
            Logger.Info("ContentTypeResolver.Initialize");

            var contentTypeSynchronizer = new ContentTypeSynchronizer();
            _contentTypes = contentTypeSynchronizer.SynchronizeContentTypes();
        }

        public List<PropertyDefinition> GetPropertyDefinitions(int contentTypeId) {
            var contentType = GetContentType(contentTypeId);

            if (contentType == null) {
                throw new Exception($"ContentType {contentTypeId} was not found!");
            }

            return contentType.Properties;
        }
    }
}