namespace KalikoCMS.Data.Repositories {
    using System.Threading.Tasks;
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class ContentLanguageRepository : RepositoryBase<ContentLanguageEntity, int>, IContentLanguageRepository {
        private readonly CmsContext _cmsContext;

        public ContentLanguageRepository(CmsContext cmsContext) : base(cmsContext) {
            _cmsContext = cmsContext;
        }

        public override async Task<ContentLanguageEntity> GetById(int id) {
            return await _cmsContext.Set<ContentLanguageEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.ContentLanguageId == id);
        }
    }
}