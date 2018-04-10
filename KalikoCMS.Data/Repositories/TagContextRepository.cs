namespace KalikoCMS.Data.Repositories {
    using System.Linq;
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class TagContextRepository : RepositoryBase<TagContextEntity, int>, ITagContextRepository {
        private readonly CmsContext _cmsContext;

        public TagContextRepository(CmsContext cmsContext) : base(cmsContext) {
            _cmsContext = cmsContext;
        }

        public override TagContextEntity GetById(int id) {
            return _cmsContext.Set<TagContextEntity>()
                .AsNoTracking()
                .FirstOrDefault(e => e.TagContextId == id);
        }
    }
}