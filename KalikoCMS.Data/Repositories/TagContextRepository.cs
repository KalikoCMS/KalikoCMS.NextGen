namespace KalikoCMS.Data.Repositories {
    using System.Threading.Tasks;
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class TagContextRepository : RepositoryBase<TagContextEntity, int>, ITagContextRepository {
        private readonly CmsContext _cmsContext;

        public TagContextRepository(CmsContext cmsContext) : base(cmsContext) {
            _cmsContext = cmsContext;
        }

        public override async Task<TagContextEntity> GetById(int id) {
            return await _cmsContext.Set<TagContextEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.TagContextId == id);
        }
    }
}