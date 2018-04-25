namespace KalikoCMS.Services.Content {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using ContentProviders;
    using Core;
    using Data.Repositories.Interfaces;
    using Infrastructure;
    using Interfaces;
    using ServiceLocation;

    public class ContentIndexService : IContentIndexService {
        private readonly IContentTypeResolver _contentTypeResolver;
        private static readonly ContentIndex Index;

        static ContentIndexService() {
            Index = new ContentIndex();
        }

        public ContentIndexService(IContentTypeResolver contentTypeResolver) {
            _contentTypeResolver = contentTypeResolver;
        }

        public void Initialize() {
            var contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();

            var contentNodes = contentRepository.GetContentNodes();
            foreach (var contentNode in contentNodes) {
                Index.AddChild(contentNode);
            }

            foreach (var site in GetRootNodes(SiteContentProvider.SiteContentTypeId)) {
                foreach (var language in site.Languages) {
                    BuildUrl(site, language);
                }
            }
        }

        private void BuildUrl(ContentNode content, LanguageNode language) {
            if (content.ContentTypeId == SiteContentProvider.SiteContentTypeId) {
                language.ContentUrl = "/";
            }
            else {
                if (content.TreeLevel > 1) {
                    var parent = content.Parent.Languages.FirstOrDefault(x => x.LanguageId == language.LanguageId);
                    // TODO: Add error handling
                    language.ContentUrl = $"{parent.ContentUrl}{language.UrlSegment}/";
                }
                else {
                    language.ContentUrl = $"/{language.UrlSegment}/";
                }
            }

            foreach (var child in content.Children) {
                foreach (var childLanguage in child.Languages) {
                    BuildUrl(child, childLanguage);
                }
            }
        }

        public bool ContentExist(Guid contentId) {
            return Index.LookupTable.ContainsKey(contentId);
        }

        public Content GetContent(Guid contentId) {
            // TODO Handle missing content
            var node = Index.LookupTable[contentId];

            var content = GetContentFromNode(node);

            return content;
        }

        public ContentNode GetNode(Guid contentId) {
            // TODO Handle missing content
            var node = Index.LookupTable[contentId];

            return node;
        }

        public IEnumerable<ContentNode> GetRootNodes(Guid contentTypeId) {
            return Index.ContentTrees.Where(x => x.Value.ContentTypeId == contentTypeId).Select(x => x.Value.Children.FirstOrDefault());
        }

        public Content GetContentFromNode(ContentNode node) {

            // TODO add language support
            var languageNode = node.Languages.FirstOrDefault();

            var contentType = _contentTypeResolver.GetContentType(node.ContentTypeId);
            // TODO Handle null

            var content = ContentProxy.CreateProxy(contentType.Type);
            content.Author = languageNode.Author;
            content.ChildSortDirection = languageNode.ChildSortDirection;
            content.ChildSortOrder = languageNode.ChildSortOrder;
            content.ContentId = node.ContentId;
            content.ContentLanguageId = languageNode.ContentLanguageId;
            content.ContentName = languageNode.ContentName;
            content.ContentTypeId = node.ContentTypeId;
            content.ContentUrl = languageNode.ContentUrl;
            content.CreatedDate = languageNode.CreatedDate;
            content.CurrentVersion = languageNode.CurrentVersion;
            content.LanguageId = languageNode.LanguageId;
            content.ParentId = node.ParentId;
            content.SortOrder = node.SortOrder;
            content.StartPublish = languageNode.StartPublish;
            content.Status = languageNode.Status;
            content.StopPublish = languageNode.StopPublish;
            content.TreeLevel = node.TreeLevel;
            content.UpdateDate = languageNode.UpdateDate;
            content.UrlSegment = languageNode.UrlSegment;
            content.VisibleInMenu = languageNode.VisibleInMenu;
            content.VisibleInSitemap = languageNode.VisibleInSitemap;

            return content;
        }
    }
}