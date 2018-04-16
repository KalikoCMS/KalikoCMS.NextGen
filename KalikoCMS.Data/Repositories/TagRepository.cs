namespace KalikoCMS.Data.Repositories {
    using System.Linq;
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class TagRepository : RepositoryBase<TagEntity, int>, ITagRepository {
        private readonly CmsContext _context;

        public TagRepository(CmsContext context) : base(context) {
            _context = context;
        }

        public override TagEntity GetById(int id) {
            return _context.Set<TagEntity>()
                .AsNoTracking()
                .FirstOrDefault(e => e.TagId == id);
        }
    }
}