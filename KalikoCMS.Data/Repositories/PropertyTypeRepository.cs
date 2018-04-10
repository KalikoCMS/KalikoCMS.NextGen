namespace KalikoCMS.Data.Repositories {
    using System;
    using System.Linq;
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class PropertyTypeRepository : RepositoryBase<PropertyTypeEntity, Guid>, IPropertyTypeRepository {
        private readonly CmsContext _cmsContext;

        public PropertyTypeRepository(CmsContext cmsContext) : base(cmsContext) {
            _cmsContext = cmsContext;
        }

        public override PropertyTypeEntity GetById(Guid id) {
            return _cmsContext.Set<PropertyTypeEntity>()
                .AsNoTracking()
                .FirstOrDefault(e => e.PropertyTypeId == id);
        }
    }
}