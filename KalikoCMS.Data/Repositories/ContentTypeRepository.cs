namespace KalikoCMS.Data.Repositories {
    using System.Threading.Tasks;
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class ContentTypeRepository : RepositoryBase<ContentTypeEntity, int>, IContentTypeRepository {
        private readonly CmsContext _cmsContext;

        public ContentTypeRepository(CmsContext cmsContext) : base(cmsContext) {
            _cmsContext = cmsContext;
        }

        public override async Task<ContentTypeEntity> GetById(int id) {
            return await _cmsContext.Set<ContentTypeEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.ContentTypeId == id);
        }
    }
}