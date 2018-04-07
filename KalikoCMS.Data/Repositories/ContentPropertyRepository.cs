namespace KalikoCMS.Data.Repositories {
    using System.Threading.Tasks;
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class ContentPropertyRepository : RepositoryBase<ContentPropertyEntity, int>, IContentPropertyRepository {
        private readonly CmsContext _cmsContext;

        public ContentPropertyRepository(CmsContext cmsContext) : base(cmsContext) {
            _cmsContext = cmsContext;
        }

        public override async Task<ContentPropertyEntity> GetById(int id) {
            return await _cmsContext.Set<ContentPropertyEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.ContentPropertyId == id);
        }
    }
}