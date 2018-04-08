namespace KalikoCMS.Data.Repositories {
    using System;
    using System.Threading.Tasks;
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class ContentProviderRepository : RepositoryBase<ContentProviderEntity, Guid>, IContentProviderRepository {
        private readonly CmsContext _cmsContext;

        public ContentProviderRepository(CmsContext cmsContext) : base(cmsContext) {
            _cmsContext = cmsContext;
        }

        public override async Task<ContentProviderEntity> GetById(Guid id) {
            return await _cmsContext.Set<ContentProviderEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.ContentProviderId == id);
        }
    }
}