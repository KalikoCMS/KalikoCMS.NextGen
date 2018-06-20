namespace KalikoCMS.Legacy.Data.Repositories {
    using System;
    using System.Linq.Expressions;
    using Entities;
    using KalikoCMS.Data.Entities;
    using KalikoCMS.Data.Repositories.Interfaces;

    public class LegacyTagContextRepository : LegacyRepositoryBase<LegacyTagContextEntity, TagContextEntity, int>, ITagContextRepository {
        public LegacyTagContextRepository(LegacyDataContext context) : base(context) { }

        public override TagContextEntity GetById(int id) {
            throw new NotImplementedException();
        }

        public override Expression<Func<LegacyTagContextEntity, TagContextEntity>> Map() {
            throw new NotImplementedException();
        }
    }
}