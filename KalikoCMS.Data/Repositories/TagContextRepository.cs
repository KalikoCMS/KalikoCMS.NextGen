namespace KalikoCMS.Data.Repositories {
    using System.Linq;
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class TagContextRepository : RepositoryBase<TagContextEntity, int>, ITagContextRepository {
        private readonly CmsContext _context;

        public TagContextRepository(CmsContext context) : base(context) {
            _context = context;
        }

        public override TagContextEntity GetById(int id) {
            return _context.Set<TagContextEntity>()
                .AsNoTracking()
                .FirstOrDefault(e => e.TagContextId == id);
        }
    }
}