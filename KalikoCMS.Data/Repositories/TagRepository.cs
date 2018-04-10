namespace KalikoCMS.Data.Repositories {
    using System.Linq;
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class TagRepository : RepositoryBase<TagEntity, int>, ITagRepository {
        private readonly CmsContext _cmsContext;

        public TagRepository(CmsContext cmsContext) : base(cmsContext) {
            _cmsContext = cmsContext;
        }

        public override TagEntity GetById(int id) {
            return _cmsContext.Set<TagEntity>()
                .AsNoTracking()
                .FirstOrDefault(e => e.TagId == id);
        }
    }
}