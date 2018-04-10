namespace KalikoCMS.Data.Repositories {
    using System.Linq;
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class ContentAccessRightsRepository : RepositoryBase<ContentAccessRightsEntity, int>, IContentAccessRightsRepository {
        private readonly CmsContext _cmsContext;

        public ContentAccessRightsRepository(CmsContext cmsContext) : base(cmsContext) {
            _cmsContext = cmsContext;
        }

        public override ContentAccessRightsEntity GetById(int id) {
            return _cmsContext.Set<ContentAccessRightsEntity>()
                .AsNoTracking()
                .FirstOrDefault(e => e.Id == id);
        }
    }
}