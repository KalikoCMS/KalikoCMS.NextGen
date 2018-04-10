namespace KalikoCMS.Data.Repositories {
    using System.Linq;
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class SystemInformationRepository : RepositoryBase<SystemInformationEntity, int>, ISystemInformationRepository {
        private readonly CmsContext _cmsContext;

        public SystemInformationRepository(CmsContext cmsContext) : base(cmsContext) {
            _cmsContext = cmsContext;
        }

        public override SystemInformationEntity GetById(int id) {
            return _cmsContext.Set<SystemInformationEntity>()
                .AsNoTracking()
                .FirstOrDefault(e => e.Id == id);
        }
    }
}