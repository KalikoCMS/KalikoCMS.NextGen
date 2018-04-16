namespace KalikoCMS.Data.Repositories {
    using System.Linq;
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class RedirectRepository : RepositoryBase<RedirectEntity, int>, IRedirectRepository {
        private readonly CmsContext _context;

        public RedirectRepository(CmsContext context) : base(context) {
            _context = context;
        }

        public override RedirectEntity GetById(int id) {
            return _context.Set<RedirectEntity>()
                .AsNoTracking()
                .FirstOrDefault(e => e.LanguageId == id);
        }
    }
}