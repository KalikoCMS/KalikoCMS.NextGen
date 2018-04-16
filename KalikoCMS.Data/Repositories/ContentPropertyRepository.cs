namespace KalikoCMS.Data.Repositories {
    using System.Linq;
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class ContentPropertyRepository : RepositoryBase<ContentPropertyEntity, int>, IContentPropertyRepository {
        private readonly CmsContext _context;

        public ContentPropertyRepository(CmsContext context) : base(context) {
            _context = context;
        }

        public override ContentPropertyEntity GetById(int id) {
            return _context.Set<ContentPropertyEntity>()
                .AsNoTracking()
                .FirstOrDefault(e => e.ContentPropertyId == id);
        }
    }
}