namespace KalikoCMS.Data.Repositories {
    using System.Linq;
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class ContentPropertyRepository : RepositoryBase<ContentPropertyEntity, int>, IContentPropertyRepository {
        private readonly CmsContext _cmsContext;

        public ContentPropertyRepository(CmsContext cmsContext) : base(cmsContext) {
            _cmsContext = cmsContext;
        }

        public override ContentPropertyEntity GetById(int id) {
            return _cmsContext.Set<ContentPropertyEntity>()
                .AsNoTracking()
                .FirstOrDefault(e => e.ContentPropertyId == id);
        }
    }
}