namespace KalikoCMS.Data.Repositories {
    using System.Threading.Tasks;
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class SystemInformationRepository : RepositoryBase<SystemInformationEntity, int>, ISystemInformationRepository {
        private readonly CmsContext _cmsContext;

        public SystemInformationRepository(CmsContext cmsContext) : base(cmsContext) {
            _cmsContext = cmsContext;
        }

        public override async Task<SystemInformationEntity> GetById(int id) {
            return await _cmsContext.Set<SystemInformationEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.Id == id);
        }
    }
}