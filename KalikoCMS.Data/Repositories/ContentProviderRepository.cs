namespace KalikoCMS.Data.Repositories {
    using System.Threading.Tasks;
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class ContentProviderRepository : RepositoryBase<ContentProviderEntity, int>, IContentProviderRepository {
        private readonly CmsContext _cmsContext;

        public ContentProviderRepository(CmsContext cmsContext) : base(cmsContext) {
            _cmsContext = cmsContext;
        }

        public override async Task<ContentProviderEntity> GetById(int id) {
            return await _cmsContext.Set<ContentProviderEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.ContentProviderId == id);
        }
    }
}