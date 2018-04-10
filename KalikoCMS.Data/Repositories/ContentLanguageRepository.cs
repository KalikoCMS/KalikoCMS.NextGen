namespace KalikoCMS.Data.Repositories {
    using System.Linq;
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class ContentLanguageRepository : RepositoryBase<ContentLanguageEntity, int>, IContentLanguageRepository {
        private readonly CmsContext _cmsContext;

        public ContentLanguageRepository(CmsContext cmsContext) : base(cmsContext) {
            _cmsContext = cmsContext;
        }

        public override ContentLanguageEntity GetById(int id) {
            return _cmsContext.Set<ContentLanguageEntity>()
                .AsNoTracking()
                .FirstOrDefault(e => e.ContentLanguageId == id);
        }
    }
}