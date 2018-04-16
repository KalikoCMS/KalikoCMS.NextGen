namespace KalikoCMS.Data.Repositories {
    using System.Linq;
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class ContentLanguageRepository : RepositoryBase<ContentLanguageEntity, int>, IContentLanguageRepository {
        private readonly CmsContext _context;

        public ContentLanguageRepository(CmsContext context) : base(context) {
            _context = context;
        }

        public override ContentLanguageEntity GetById(int id) {
            return _context.Set<ContentLanguageEntity>()
                .AsNoTracking()
                .FirstOrDefault(e => e.ContentLanguageId == id);
        }
    }
}