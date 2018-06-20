namespace KalikoCMS.Legacy.Data.Repositories {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using ContentProviders;
    using Core;
    using Entities;
    using Infrastructure;
    using KalikoCMS.Data.Entities;
    using KalikoCMS.Data.Repositories.Interfaces;

    public class LegacyContentRepository : LegacyRepositoryBase<LegacyPageEntity, ContentEntity, Guid>, IContentRepository {
        private readonly LegacyDataContext _context;
        public LegacyContentRepository(LegacyDataContext context) : base(context) {
            _context = context;
        }

        public override ContentEntity GetById(Guid id) {
            throw new NotImplementedException();
        }

        public override Expression<Func<LegacyPageEntity, ContentEntity>> Map() {
            return x => new ContentEntity {
                ContentId = x.PageId,
                ContentTypeId = ToGuid(x.PageTypeId),
                ParentId = x.ParentId,
                SortOrder = x.SortOrder,
                TreeLevel = x.TreeLevel
            };
        }

        public IEnumerable<ContentNode> GetContentNodes() {
            var nodes = (from content in _context.Pages
                join contentLanguage in
                    _context.PageInstances.Where(x => x.DeletedDate == null && (x.Status == ContentStatus.Published || (x.Status == ContentStatus.WorkingCopy && x.CurrentVersion == 0)))
                    on content.PageId equals contentLanguage.PageId into grouped
                orderby content.TreeLevel
                select new ContentNode
                {
                    ContentId = content.PageId,
                    SortOrder = content.SortOrder,
                    TreeLevel = content.TreeLevel,
                    ParentId = content.ParentId,
                    ContentTypeId = ToGuid(content.PageTypeId),
                    ContentProviderId = PageContentProvider.UniqueId,
                    Languages = (from contentLanguage in grouped
                        select new LanguageNode
                        {
                            ContentLanguageId = contentLanguage.PageInstanceId,
                            ContentName = contentLanguage.PageName,
                            ChildSortOrder = contentLanguage.ChildSortOrder,
                            StopPublish = contentLanguage.StopPublish,
                            UpdateDate = contentLanguage.UpdateDate,
                            ChildSortDirection = contentLanguage.ChildSortDirection,
                            Status = contentLanguage.Status,
                            Author = contentLanguage.Author,
                            StartPublish = contentLanguage.StartPublish,
                            CreatedDate = contentLanguage.CreatedDate,
                            VisibleInSitemap = contentLanguage.VisibleInSitemap,
                            CurrentVersion = contentLanguage.CurrentVersion,
                            UrlSegment = contentLanguage.PageUrl,
                            VisibleInMenu = contentLanguage.VisibleInMenu,
                            LanguageId = contentLanguage.LanguageId
                        }).ToList()
                }).ToList();

            var siteId = new Guid("C541EA37-9B7C-4634-85C3-41DE0BE24F66");

            foreach (var contentNode in nodes.Where(x => x.ParentId == Guid.Empty)) {
                contentNode.ParentId = siteId;
            }

            nodes.Insert(0, new ContentNode {
                ContentProviderId = SiteContentProvider.UniqueId,
                ContentTypeId = SiteContentProvider.SiteContentTypeId,
                Languages = new List<LanguageNode> { new LanguageNode { LanguageId = 1 } },
                ContentId = siteId,
            });

            return nodes;
        }

        public void SaveContent(Content content) {
            throw new NotImplementedException();
        }

        public void PublishContent(Content content) {
            throw new NotImplementedException();
        }
    }
}