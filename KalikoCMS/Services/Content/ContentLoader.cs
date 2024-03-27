namespace KalikoCMS.Services.Content {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core;
    using Core.Interfaces;
    using Infrastructure;
    using Interfaces;
    using KalikoCMS.Extensions;
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
            var ancestors = new List<IContent>();
            var contentId = contentReference.ContentId;

            // TODO: null checks + access checks

            do {
                var contentNode = _contentIndexService.GetNode(contentId);
                var languageNode = contentNode?.Languages.FirstOrDefault(x => x.LanguageId == contentReference.LanguageId);

                if (contentNode == null || languageNode == null) {
                    break;
                }

                var content = _contentMapper.MapToContent(contentNode, languageNode);
                ancestors.Add(content);

                if (content.TreeLevel < 0) {
                    break;
                }

                contentId = content.ParentId;

            } while (contentId != Guid.Empty);

            return ancestors;
        }

        public T GetClosest<T>(ContentReference contentReference, bool bypassAccessCheck = false) where T : Content {
            var contentNode = _contentIndexService.GetNode(contentReference.ContentId);

            var contentTypeResolver = ServiceLocator.Current.GetInstance<IContentTypeResolver>();
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
                var content = _contentIndexService.GetContentFromNode(node);

                if (content != null && content.IsAvailable()) {
                    children.Add(content);
                }
            }

            // TODO: null checks + access checks

            return children;
        }

        public IEnumerable<T> GetChildren<T>(ContentReference contentReference, bool bypassAccessCheck = false) where T : Content {
            var contentNode = _contentIndexService.GetNode(contentReference.ContentId);
            var children = new List<T>();
            foreach (var node in contentNode.Children) {
                var content = _contentIndexService.GetContentFromNode(node);

                if (content is T variable) {
                    children.Add(variable);
                }
            }

            // TODO: null checks + access checks

            return children;
        }

        public IEnumerable<IContent> GetDescendants(ContentReference contentReference, bool bypassAccessCheck = false) {
            var contentNode = _contentIndexService.GetNode(contentReference.ContentId);
            var descendants = new List<IContent>();

            foreach (var node in contentNode.Children)
            {
                var content = _contentIndexService.GetContentFromNode(node);

                descendants.Add(content);

                if (!node.Children.Any())
                {
                    continue;
                }

                descendants.AddRange(GetDescendants(content.ContentReference, bypassAccessCheck));
            }

            // TODO: null checks + access checks

            return descendants;
        }

        public IEnumerable<T> GetDescendants<T>(ContentReference contentReference, bool bypassAccessCheck = false) where T : Content {
            var contentNode = _contentIndexService.GetNode(contentReference.ContentId);
            var descendants = new List<T>();

            foreach (var node in contentNode.Children) {
                var content = _contentIndexService.GetContentFromNode(node) as IContent;

                if (content is T typedContent) {
                    descendants.Add(typedContent);
                }

                if (!node.Children.Any()) {
                    continue;
                }

                if (content != null)
                {
                    descendants.AddRange(GetDescendants<T>(content.ContentReference, bypassAccessCheck));
                }
                
            }

            return descendants;
        }


        T IContentLoader.Get<T>(Guid contentId, int languageId, bool bypassAccessCheck) {
            throw new NotImplementedException();
        }
    }
}