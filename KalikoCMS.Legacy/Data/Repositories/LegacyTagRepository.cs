namespace KalikoCMS.Legacy.Data.Repositories {
    using System;
    using System.Linq.Expressions;
    using Entities;
    using KalikoCMS.Data.Entities;
    using KalikoCMS.Data.Repositories.Interfaces;

    public class LegacyTagRepository : LegacyRepositoryBase<LegacyTagEntity, TagEntity, int>, ITagRepository {
        public LegacyTagRepository(LegacyDataContext context) : base(context) { }

        public override TagEntity GetById(int id) {
            throw new NotImplementedException();
        }

        public override Expression<Func<LegacyTagEntity, TagEntity>> Map() {
            throw new NotImplementedException();
        }
    }
}