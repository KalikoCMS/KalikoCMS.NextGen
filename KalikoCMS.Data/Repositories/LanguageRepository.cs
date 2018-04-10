namespace KalikoCMS.Data.Repositories {
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class LanguageRepository : RepositoryBase<LanguageEntity, int>, ILanguageRepository {
        private readonly CmsContext _cmsContext;

        public LanguageRepository(CmsContext cmsContext) : base(cmsContext) {
            _cmsContext = cmsContext;
        }

        public override LanguageEntity GetById(int id) {
            return _cmsContext.Set<LanguageEntity>()
                .AsNoTracking()
                .FirstOrDefault(e => e.LanguageId == id);
        }
    }
}