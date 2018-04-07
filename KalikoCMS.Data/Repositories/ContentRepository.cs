namespace KalikoCMS.Data.Repositories {
    using System;
    using System.Threading.Tasks;
    using Entities;
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
    }
}