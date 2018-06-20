namespace KalikoCMS.Legacy.Data.Repositories {
    using System;
    using System.Linq.Expressions;
    using Entities;
    using KalikoCMS.Data;
    using KalikoCMS.Data.Entities;
    using KalikoCMS.Data.Repositories.Interfaces;

    public class LegacyContentPropertyRepository : LegacyRepositoryBase<LegacyPagePropertyEntity, ContentPropertyEntity, int>, IContentPropertyRepository {
        public LegacyContentPropertyRepository(LegacyDataContext context) : base(context) { }

        public override ContentPropertyEntity GetById(int id) {
            throw new NotImplementedException();
        }

        public override Expression<Func<LegacyPagePropertyEntity, ContentPropertyEntity>> Map() {
            throw new NotImplementedException();
        }
    }
}