namespace KalikoCMS.Services.Content {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ContentProviders;
    using Core;
    using Data.Repositories.Interfaces;
    using Infrastructure;
    using Interfaces;
    using Mappers.Interfaces;
    using ServiceLocation;

    public class ContentIndexService : IContentIndexService {
        private readonly IContentMapper _contentMapper;
        private static ContentIndex _index;

        static ContentIndexService() {
            _index = new ContentIndex();
        }

        public ContentIndexService(IContentMapper contentMapper) {
            _contentMapper = contentMapper;
        }

        public void Initialize() {
            var contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();

            var index = new ContentIndex();

            var contentNodes = contentRepository.GetContentNodes();
            foreach (var contentNode in contentNodes) {
                index.AddChild(contentNode);
            }

            _index = index;
            //foreach (var site in GetRootNodes(SiteContentProvider.SiteContentTypeId)) {
            //    foreach (var language in site.Languages) {
            //        BuildUrl(site, language);
            //    }
            //}
        }

        public void AddOrUpdate(Content content) {
            var existingNode = GetNode(content.ContentId);

            var node = _contentMapper.MapFromContent(content);

            if (existingNode == null) {
                _index.AddChild(node);
                return;
            }

            existingNode.Languages.RemoveAll(x => x.LanguageId == content.LanguageId);
            existingNode.Languages.Add(node.Languages.FirstOrDefault());
            _index.GenerateContentUrl(existingNode);
        }

        //private void BuildUrl(ContentNode content, LanguageNode language) {
        //    if (content.ContentTypeId == SiteContentProvider.SiteContentTypeId) {
        //        language.ContentUrl = "/";
        //    }
        //    else {
        //        if (content.TreeLevel > 1) {
        //            var parent = content.Parent.Languages.FirstOrDefault(x => x.LanguageId == language.LanguageId);
        //            // TODO: Add error handling
        //            language.ContentUrl = $"{parent.ContentUrl}{language.UrlSegment}/";
        //        }
        //        else {
        //            language.ContentUrl = $"/{language.UrlSegment}/";
        //        }
        //    }

        //    foreach (var child in content.Children) {
        //        foreach (var childLanguage in child.Languages) {
        //            BuildUrl(child, childLanguage);
        //        }
        //    }
        //}

        public bool ContentExist(Guid contentId) {
            return _index.LookupTable.ContainsKey(contentId);
        }

        public Content GetContent(Guid contentId) {
            var node = GetNode(contentId);
            if (node == null) {
                return null;
            }

            var content = GetContentFromNode(node);
            return content;
        }

        public Content GetContentFromNode(ContentNode node) {
            // TODO add language support
            var languageNode = node.Languages.FirstOrDefault();
            if (languageNode == null) {
                return null;
            }

            var content = _contentMapper.MapToContent(node, languageNode);

            return content;
        }

        public ContentNode GetNode(Guid contentId) {
            if (_index.LookupTable.TryGetValue(contentId, out var node)) {
                return node;
            }

            return null;
        }

        public IEnumerable<ContentNode> GetRootNodes(Guid contentTypeId) {
            return _index.ContentTrees.Where(x => x.Value.ContentTypeId == contentTypeId).Select(x => x.Value.Children.FirstOrDefault());
        }
    }
}