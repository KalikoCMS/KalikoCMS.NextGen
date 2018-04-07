namespace KalikoCMS.Data.Repositories {
    using System;
    using System.Threading.Tasks;
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class PropertyTypeRepository : RepositoryBase<PropertyTypeEntity, Guid>, IPropertyTypeRepository {
        private readonly CmsContext _cmsContext;

        public PropertyTypeRepository(CmsContext cmsContext) : base(cmsContext) {
            _cmsContext = cmsContext;
        }

        public override async Task<PropertyTypeEntity> GetById(Guid id) {
            return await _cmsContext.Set<PropertyTypeEntity>()
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.PropertyTypeId == id);
        }
    }
}