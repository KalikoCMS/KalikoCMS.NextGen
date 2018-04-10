namespace KalikoCMS.Data.Repositories {
    using System.Linq;
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class PropertyRepository : RepositoryBase<PropertyEntity, int>, IPropertyRepository {
        private readonly CmsContext _cmsContext;

        public PropertyRepository(CmsContext cmsContext) : base(cmsContext) {
            _cmsContext = cmsContext;
        }

        public override PropertyEntity GetById(int id) {
            return _cmsContext.Set<PropertyEntity>()
                .AsNoTracking()
                .FirstOrDefault(e => e.PropertyId == id);
        }
    }
}