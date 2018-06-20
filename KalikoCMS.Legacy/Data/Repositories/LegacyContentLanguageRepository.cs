namespace KalikoCMS.Legacy.Data.Repositories {
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using Entities;
    using KalikoCMS.Data.Entities;
    using KalikoCMS.Data.Repositories.Interfaces;

    public class LegacyContentLanguageRepository : LegacyRepositoryBase<LegacyPageInstanceEntity, ContentLanguageEntity, int>, IContentLanguageRepository {
        private readonly LegacyDataContext _context;

        public LegacyContentLanguageRepository(LegacyDataContext context) : base(context) {
            _context = context;
        }

        public override ContentLanguageEntity GetById(int id) {
            return FirstOrDefault(x => x.ContentLanguageId == id);
        }

        public override Expression<Func<LegacyPageInstanceEntity, ContentLanguageEntity>> Map() {
            return x => new ContentLanguageEntity {
                ContentId = x.PageId,
                Author = x.Author,
                ChildSortDirection = x.ChildSortDirection,
                ChildSortOrder = x.ChildSortOrder,
                ContentLanguageId = x.PageInstanceId,
                ContentName = x.PageName,
                CreatedDate = x.CreatedDate,
                CurrentVersion = x.CurrentVersion,
                DeletedDate = x.DeletedDate,
                IsOriginal = true,
                LanguageId = x.LanguageId,
                StartPublish = x.StartPublish,
                Status = x.Status,
                StopPublish = x.StopPublish,
                UpdateDate = x.UpdateDate,
                UrlSegment = x.PageUrl,
                VisibleInMenu = x.VisibleInMenu,
                VisibleInSitemap = x.VisibleInSitemap
            };
        }
    }
}