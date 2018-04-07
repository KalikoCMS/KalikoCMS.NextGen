namespace KalikoCMS.Data.Repositories {
    using System.Threading.Tasks;
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class RedirectRepository : RepositoryBase<RedirectEntity, int>, IRedirectRepository {
        private readonly CmsContext _cmsContext;

        public RedirectRepository(CmsContext cmsContext) : base(cmsContext) {
            _cmsContext = cmsContext;
        }

        public override async Task<RedirectEntity> GetById(int id) {
            return await _cmsContext.Set<RedirectEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.LanguageId == id);
        }
    }
}