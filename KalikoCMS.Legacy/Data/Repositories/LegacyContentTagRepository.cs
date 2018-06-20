namespace KalikoCMS.Legacy.Data.Repositories {
    using System;
    using System.Linq.Expressions;
    using Entities;
    using KalikoCMS.Data.Entities;
    using KalikoCMS.Data.Repositories.Interfaces;

    public class LegacyContentTagRepository : LegacyRepositoryBase<LegacyPageTagEntity, ContentTagEntity, int>, IContentTagRepository {
        public LegacyContentTagRepository(LegacyDataContext context) : base(context) { }

        public override ContentTagEntity GetById(int id) {
            throw new NotImplementedException();
        }

        public override Expression<Func<LegacyPageTagEntity, ContentTagEntity>> Map() {
            throw new NotImplementedException();
        }
    }
}