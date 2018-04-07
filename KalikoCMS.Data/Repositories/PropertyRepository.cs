namespace KalikoCMS.Data.Repositories {
    using System.Threading.Tasks;
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class PropertyRepository : RepositoryBase<PropertyEntity, int>, IPropertyRepository {
        private readonly CmsContext _cmsContext;

        public PropertyRepository(CmsContext cmsContext) : base(cmsContext) {
            _cmsContext = cmsContext;
        }

        public override async Task<PropertyEntity> GetById(int id) {
            return await _cmsContext.Set<PropertyEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.PropertyId == id);
        }
    }
}