namespace KalikoCMS.Data.Repositories {
    using System.Threading.Tasks;
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class ContentAccessRightsRepository : RepositoryBase<ContentAccessRightsEntity, int>, IContentAccessRightsRepository {
        private readonly CmsContext _cmsContext;

        public ContentAccessRightsRepository(CmsContext cmsContext) : base(cmsContext) {
            _cmsContext = cmsContext;
        }

        public override async Task<ContentAccessRightsEntity> GetById(int id) {
            return await _cmsContext.Set<ContentAccessRightsEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}