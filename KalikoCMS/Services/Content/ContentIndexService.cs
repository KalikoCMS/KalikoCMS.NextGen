namespace KalikoCMS.Services.Content {
    using System.Linq;
    using ContentProviders;
    using Data.Entities;
    using Data.Repositories.Interfaces;
    using Infrastructure;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;
    using ServiceLocation;

    public class ContentIndexService : IContentIndexService {
        private static ContentIndex _index;

        static ContentIndexService() {
            _index = new ContentIndex();
        }

        public void Initialize() {
            var contentRepository = ServiceLocator.Current.GetInstance<IContentRepository>();

            var sites = contentRepository.GetContentNodes(SiteContentProvider.UniqueId);
            foreach (var site in sites) {
                var contentTree = _index.AddContentTree(site.ContentId);
                contentTree.Children.Add(site);
            }

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
    }
}