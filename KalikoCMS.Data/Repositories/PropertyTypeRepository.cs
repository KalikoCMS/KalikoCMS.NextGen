namespace KalikoCMS.Data.Repositories {
    using System;
    using System.Linq;
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class PropertyTypeRepository : RepositoryBase<PropertyTypeEntity, Guid>, IPropertyTypeRepository {
        private readonly CmsContext _context;

        public PropertyTypeRepository(CmsContext context) : base(context) {
            _context = context;
        }

        public override PropertyTypeEntity GetById(Guid id) {
            return _context.Set<PropertyTypeEntity>()
                .AsNoTracking()
                .FirstOrDefault(e => e.PropertyTypeId == id);
        }
    }
}