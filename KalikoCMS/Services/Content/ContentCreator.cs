namespace KalikoCMS.Services.Content {
    using System;
    using System.Linq;
    using Core;
    using Data.Repositories.Interfaces;
    using Extensions;
    using Interfaces;
    using Resolvers.Interfaces;
    using ServiceLocation;

    public class ContentCreator : IContentCreator {
        private readonly IContentTypeResolver _contentTypeResolver;
        private readonly IContentIndexService _contentIndexService;

        public ContentCreator(IContentTypeResolver contentTypeResolver, IContentIndexService contentIndexService) {
            _contentTypeResolver = contentTypeResolver;
            _contentIndexService = contentIndexService;
        }

        public T CreateNew<T>(ContentReference parent, bool bypassAccessCheck = false) where T : Content {
            var contentType = _contentTypeResolver.GetContentType<T>();
            if (contentType == null) {
                throw new ArgumentException($"'{typeof(T).FullName}' is not a registered content type. Missing a decorating attribute?");
            }

            var proxy = (T)ContentProxy.CreateProxy(typeof(T), true);
            proxy.ContentProviderId = contentType.ContentProviderId;
            proxy.ContentTypeId = contentType.ContentTypeId;
            proxy.CurrentVersion = -1;
            proxy.LanguageId = parent.LanguageId;
            proxy.ParentId = parent.ContentId;
            proxy.Status = ContentStatus.WorkingCopy;
            proxy.SetDefaults();

            return proxy;
        }

        public void Save(Content content, bool bypassAccessCheck = false) {
            if (content is null) { throw new ArgumentException("Content can't be null"); }
            if (content.ParentId == Guid.Empty && !(content is CmsSite)) { throw new Exception("Content has no parent"); }
            if (content.Status != ContentStatus.WorkingCopy) { throw new Exception("Status may not be manually changed"); }
            if (content.CurrentVersion == -1 && _contentIndexService.ContentExist(content.ContentId)) { throw new Exception($"Content with id '{content.ContentId}' already exists"); }

            var httpContextResolver = ServiceLocator.Current.GetInstance<IHttpContextResolver>();

            content.Author = httpContextResolver.Current.User.Identity.Name;
            content.UpdateDate = DateTime.UtcNow;

            if (content is CmsSite) {
                content.TreeLevel = 0;
                content.SortOrder = 0;
            }
            else {
                var parentNode = _contentIndexService.GetNode(content.ParentId);
                if (parentNode == null) { throw new Exception($"Parent with id '{content.ParentId}' was not found!"); }

                content.TreeLevel = parentNode.TreeLevel + 1;
                content.SortOrder = parentNode.Children.Any() ? parentNode.Children.Max(x => x.SortOrder) + 1 : 1;

                content.UrlSegment = content.ContentName.UrlFriendly();
                // TODO: Check for empty segments and duplicates under parent
            }

            if (content.CurrentVersion == -1) {
                content.CreatedDate = DateTime.UtcNow;
            }

            var contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();
            contentRepository.SaveContent(content);

            if (content.CurrentVersion == 0) {
                _contentIndexService.AddOrUpdate(content);
            }
        }

        public void Publish(Content content, bool bypassAccessCheck = false) {
            // TODO: Check access rights
            var contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();
            contentRepository.PublishContent(content);

            _contentIndexService.AddOrUpdate(content);
        }
    }
}
