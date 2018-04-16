namespace KalikoCMS.Data.Repositories {
    using System.Linq;
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class SystemInformationRepository : RepositoryBase<SystemInformationEntity, int>, ISystemInformationRepository {
        private readonly CmsContext _context;

        public SystemInformationRepository(CmsContext context) : base(context) {
            _context = context;
        }

        public override SystemInformationEntity GetById(int id) {
            return _context.Set<SystemInformationEntity>()
                .AsNoTracking()
                .FirstOrDefault(e => e.Id == id);
        }
    }
}