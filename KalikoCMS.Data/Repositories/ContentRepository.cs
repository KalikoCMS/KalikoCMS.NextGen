namespace KalikoCMS.Data.Repositories {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Core;
    using Entities;
    using Infrastructure;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class ContentRepository : RepositoryBase<ContentEntity, Guid>, IContentRepository {
        private readonly CmsContext _cmsContext;

        public ContentRepository(CmsContext cmsContext) : base(cmsContext) {
            _cmsContext = cmsContext;
        }

        public override async Task<ContentEntity> GetById(Guid id) {
            return await _cmsContext.Set<ContentEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.ContentId == id);
        }

        public IEnumerable<ContentNode> GetContentNodes(Guid contentProviderId) {
            return from content in _cmsContext.Content
                join contentLanguage in _cmsContext.ContentLanguages on content.ContentId equals contentLanguage.ContentId into grouped
                orderby content.TreeLevel
                select new ContentNode() {
                    ContentId = content.ContentId,
                    SortOrder = content.SortOrder,
                    TreeLevel = content.TreeLevel,
                    ContentTypeId = content.ContentTypeId,
                    Languages = from contentLanguage in grouped
                        where contentLanguage.DeletedDate == null && (contentLanguage.Status == ContentStatus.Published || (contentLanguage.Status == ContentStatus.WorkingCopy && contentLanguage.CurrentVersion == 1))
                        select new LanguageNode {
                            ContentLanguageId = contentLanguage.ContentLanguageId,
                            ContentName = contentLanguage.ContentName,
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
                            UrlSegment = contentLanguage.UrlSegment,
                            VisibleInMenu = contentLanguage.VisibleInMenu,
                            LanguageId = contentLanguage.LanguageId
                        }
                };
        }
    }
}