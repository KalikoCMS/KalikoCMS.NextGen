namespace KalikoCMS.Legacy.Data.Repositories {
    using System;
    using System.Linq.Expressions;
    using KalikoCMS.Data.Entities;
    using KalikoCMS.Data.Repositories.Interfaces;

    public class LegacyContentAccessRightsRepository : LegacyRepositoryBase<ContentAccessRightsEntity, ContentAccessRightsEntity, int>, IContentAccessRightsRepository {
        public LegacyContentAccessRightsRepository(LegacyDataContext context) : base(context) { }

        public override ContentAccessRightsEntity GetById(int id) {
            throw new System.NotImplementedException();
        }

        public override Expression<Func<ContentAccessRightsEntity, ContentAccessRightsEntity>> Map() {
            return x => x;
        }
    }
}