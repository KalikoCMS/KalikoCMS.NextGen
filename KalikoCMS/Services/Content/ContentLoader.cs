namespace KalikoCMS.Services.Content {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core;
    using Core.Interfaces;
    using Interfaces;
    using Mappers.Interfaces;
    using ServiceLocation;

    public class ContentLoader : IContentLoader {
        private readonly IContentIndexService _contentIndexService;
        private readonly IContentMapper _contentMapper;

        public ContentLoader(IContentIndexService contentIndexService, IContentMapper contentMapper) {
            _contentIndexService = contentIndexService;
            _contentMapper = contentMapper;
        }

        public T Get<T>(Guid contentId, int languageId, bool bypassAccessCheck = false) where T : class, IContent {
            var contentNode = _contentIndexService.GetNode(contentId);
            var languageNode = contentNode.Languages.FirstOrDefault(x => x.LanguageId == languageId);

            // TODO: null checks + access checks

            return _contentMapper.MapToContent(contentNode, languageNode) as T;
        }

        public T Get<T>(ContentReference contentReference, bool bypassAccessCheck = false) where T : Content {
            var contentNode = _contentIndexService.GetNode(contentReference.ContentId);
            var languageNode = contentNode.Languages.FirstOrDefault(x => x.LanguageId == contentReference.LanguageId);

            // TODO: null checks + access checks

            return _contentMapper.MapToContent(contentNode, languageNode) as T;
        }

        public IEnumerable<IContent> GetAncestors(ContentReference contentReference, bool bypassAccessCheck = false) {
            var contentNode = _contentIndexService.GetNode(contentReference.ContentId);
            var languageNode = contentNode.Languages.FirstOrDefault(x => x.LanguageId == contentReference.LanguageId);

            throw new NotImplementedException();
        }

        public T GetClosest<T>(ContentReference contentReference, bool bypassAccessCheck = false) where T : Content {
            var contentNode = _contentIndexService.GetNode(contentReference.ContentId);

            var contentTypeResolver = ServiceLocator.Current.GetInstance<ContentTypeResolver>();
            var contentTypes = contentTypeResolver.GetContentTypes<T>();

            while (contentNode.Parent != null) {
                contentNode = contentNode.Parent;

                if (contentTypes.All(x => x.ContentTypeId != contentNode.ContentTypeId)) {
                    continue;
                }

                var languageNode = contentNode.Languages.FirstOrDefault(x => x.LanguageId == contentReference.LanguageId);
                if (languageNode == null) {
                    return null;
                }

                return _contentMapper.MapToContent(contentNode, languageNode) as T;
            }

            return null;
        }

        public IEnumerable<IContent> GetChildren(ContentReference contentReference, bool bypassAccessCheck = false) {
            var contentNode = _contentIndexService.GetNode(contentReference.ContentId);
            var children = new List<IContent>();
            foreach (var node in contentNode.Children) {
                children.Add(_contentIndexService.GetContentFromNode(node));
            }

            return children;
        }

        public IEnumerable<T> GetChildren<T>(ContentReference contentReference, bool bypassAccessCheck = false) where T : Content {
            throw new NotImplementedException();
        }

        public IEnumerable<IContent> GetDescendents(ContentReference contentReference, bool bypassAccessCheck = false) {
            throw new NotImplementedException();
        }


        T IContentLoader.Get<T>(Guid contentId, int languageId, bool bypassAccessCheck) {
            throw new NotImplementedException();
        }
    }
}