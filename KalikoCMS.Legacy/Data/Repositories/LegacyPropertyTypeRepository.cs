namespace KalikoCMS.Legacy.Data.Repositories {
    using System;
    using System.Linq.Expressions;
    using Entities;
    using KalikoCMS.Data.Entities;
    using KalikoCMS.Data.Repositories.Interfaces;

    public class LegacyPropertyTypeRepository : LegacyRepositoryBase<LegacyPropertyTypeEntity, PropertyTypeEntity, Guid>, IPropertyTypeRepository {
        public LegacyPropertyTypeRepository(LegacyDataContext context) : base(context) { }

        public override PropertyTypeEntity GetById(Guid id) {
            return FirstOrDefault(x => x.PropertyTypeId == id);
        }

        public override Expression<Func<LegacyPropertyTypeEntity, PropertyTypeEntity>> Map() {
            return x => new PropertyTypeEntity {
                PropertyTypeId = x.PropertyTypeId,
                Class = x.Class,
                Name = x.Name
            };
        }

        public override void Create(PropertyTypeEntity entity) {
            // Supress
        }

        public override void Update(PropertyTypeEntity entity) {
            // Supress
        }

        public override void Delete(Guid id) {
            // Supress
        }
    }
}