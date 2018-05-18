namespace KalikoCMS.Data.Repositories {
    using System.Linq;
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class DomainRepository : RepositoryBase<DomainEntity, int>, IDomainRepository {
        private readonly CmsContext _context;

        public DomainRepository(CmsContext context) : base(context) {
            _context = context;
        }

        public override DomainEntity GetById(int id) {
            return _context.Set<DomainEntity>()
                .AsNoTracking()
                .FirstOrDefault(e => e.DomainId == id);
        }
    }
}
