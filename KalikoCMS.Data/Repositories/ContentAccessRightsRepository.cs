namespace KalikoCMS.Data.Repositories {
    using System.Linq;
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class ContentAccessRightsRepository : RepositoryBase<ContentAccessRightsEntity, int>, IContentAccessRightsRepository {
        private readonly CmsContext _context;

        public ContentAccessRightsRepository(CmsContext context) : base(context) {
            _context = context;
        }

        public override ContentAccessRightsEntity GetById(int id) {
            return _context.Set<ContentAccessRightsEntity>()
                .AsNoTracking()
                .FirstOrDefault(e => e.Id == id);
        }
    }
}