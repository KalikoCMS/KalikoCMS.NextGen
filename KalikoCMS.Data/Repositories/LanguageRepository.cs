namespace KalikoCMS.Data.Repositories {
    using System.Linq;
    using System.Threading.Tasks;
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class LanguageRepository : RepositoryBase<LanguageEntity, int>, ILanguageRepository {
        private readonly CmsContext _context;

        public LanguageRepository(CmsContext context) : base(context) {
            _context = context;
        }

        public override LanguageEntity GetById(int id) {
            return _context.Set<LanguageEntity>()
                .AsNoTracking()
                .FirstOrDefault(e => e.LanguageId == id);
        }
    }
}