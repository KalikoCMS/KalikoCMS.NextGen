namespace KalikoCMS.Services.Content {
    using System;
    using Core;
    using Interfaces;

    public class ContentCreator : IContentCreator {
        private readonly IContentTypeResolver _contentTypeResolver;

        public ContentCreator(IContentTypeResolver contentTypeResolver) {
            _contentTypeResolver = contentTypeResolver;
        }

        public T Create<T>() where T : Content {
            var contentType = _contentTypeResolver.GetContentType<T>();
            if (contentType == null) {
                throw new ArgumentException($"'{typeof(T).FullName}' is not a registered content type. Missing a decorating attribute?");
            }

            var proxy = (T)ContentProxy.CreateProxy(typeof(T), true);
            proxy.ContentTypeId = contentType.ContentTypeId;
            proxy.Status = ContentStatus.WorkingCopy;

            return proxy;
        }
    }
}
