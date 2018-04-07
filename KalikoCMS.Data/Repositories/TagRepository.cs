namespace KalikoCMS.Data.Repositories {
    using System.Threading.Tasks;
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class TagRepository : RepositoryBase<TagEntity, int>, ITagRepository {
        private readonly CmsContext _cmsContext;

        public TagRepository(CmsContext cmsContext) : base(cmsContext) {
            _cmsContext = cmsContext;
        }

        public override async Task<TagEntity> GetById(int id) {
            return await _cmsContext.Set<TagEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.TagId == id);
        }
    }
}