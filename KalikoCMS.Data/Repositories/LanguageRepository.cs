namespace KalikoCMS.Data.Repositories {
    using System.Threading.Tasks;
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class LanguageRepository : RepositoryBase<LanguageEntity, int>, ILanguageRepository {
        private readonly CmsContext _cmsContext;

        public LanguageRepository(CmsContext cmsContext) : base(cmsContext) {
            _cmsContext = cmsContext;
        }

        public override async Task<LanguageEntity> GetById(int id) {
            return await _cmsContext.Set<LanguageEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.LanguageId == id);
        }
    }
}