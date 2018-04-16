namespace KalikoCMS.Services.Content {
    using System;
    using System.Linq;
    using ContentProviders;
    using Core;
    using Data.Repositories.Interfaces;
    using Infrastructure;
    using Interfaces;
    using ServiceLocation;

    public class ContentIndexService : IContentIndexService {
        private readonly IContentTypeResolver _contentTypeResolver;
        private static readonly ContentIndex _index;

        static ContentIndexService() {
            _index = new ContentIndex();
        }

        public ContentIndexService(IContentTypeResolver contentTypeResolver) {
            _contentTypeResolver = contentTypeResolver;
        }

        public void Initialize() {
            var contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();

            var contentNodes = contentRepository.GetContentNodes();
            foreach (var contentNode in contentNodes) {
                _index.AddChild(contentNode);
            }

            //var sites = contentRepository.GetContentNodes(SiteContentProvider.UniqueId);
            //foreach (var site in sites) {
            //    var contentTree = _index.AddContentTree(site.ContentId);
            //    contentTree.AddChild(site);
            //}

            //var pages = contentRepository.GetContentNodes(PageContentProvider.UniqueId);
            //foreach (var page in pages) {
            //    _index.AddChild(page);
            //}

            //var sites = contentRepository.GetAll().Include(x => x.ContentLanguages).Where(x => x.ContentType.ContentProviderId == SiteContentProvider.UniqueId);
            //foreach (var site in sites) {
            //    var contentTree = _index.AddContentTree(site.ContentId);
            //    var siteNode = new ContentNode() {};
            //    foreach (var contentLanguage in site.ContentLanguages) {
            //        var languageNode = new LanguageNode {
            //            Author = contentLanguage.Author,
            //            ChildSortDirection = contentLanguage.ChildSortDirection,
            //            ChildSortOrder = contentLanguage.ChildSortOrder,
            //            ContentLanguageId = contentLanguage.ContentLanguageId,
            //            ContentName = contentLanguage.ContentName,
            //            ContentUrl = contentLanguage.ContentName,
            //            CreatedDate = contentLanguage.CreatedDate,
            //            CurrentVersion = contentLanguage.CurrentVersion,
            //            LanguageId = contentLanguage.ContentLanguageId,
            //            StartPublish = contentLanguage.StartPublish,
            //            Status = contentLanguage.Status,
            //            StopPublish = contentLanguage.StopPublish,
            //            UpdateDate = contentLanguage.UpdateDate,
            //            UrlSegment = contentLanguage.UrlSegment,
            //            VisibleInMenu = contentLanguage.VisibleInMenu,
            //            VisibleInSitemap = contentLanguage.VisibleInSitemap
            //        };
            //        siteNode.Languages.Add(languageNode);
            //    }
            //}
        }

        public bool ContentExist(Guid contentId) {
            return _index.LookupTable.ContainsKey(contentId);
        }



        public Content GetContent(Guid contentId) {
            // TODO Handle missing content
            var node = _index.LookupTable[contentId];

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


        public ContentNode GetNode(Guid contentId) {
            // TODO Handle missing content
            var node = _index.LookupTable[contentId];

            return node;
        }
    }
}