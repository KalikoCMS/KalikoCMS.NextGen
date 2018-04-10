namespace KalikoCMS.Data.Repositories {
    using System.Linq;
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class RedirectRepository : RepositoryBase<RedirectEntity, int>, IRedirectRepository {
        private readonly CmsContext _cmsContext;

        public RedirectRepository(CmsContext cmsContext) : base(cmsContext) {
            _cmsContext = cmsContext;
        }

        public override RedirectEntity GetById(int id) {
            return _cmsContext.Set<RedirectEntity>()
                .AsNoTracking()
                .FirstOrDefault(e => e.LanguageId == id);
        }
    }
}