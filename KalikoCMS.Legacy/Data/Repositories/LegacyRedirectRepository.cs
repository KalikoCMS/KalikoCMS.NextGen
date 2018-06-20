namespace KalikoCMS.Legacy.Data.Repositories {
    using System;
    using System.Linq.Expressions;
    using Entities;
    using KalikoCMS.Data.Entities;
    using KalikoCMS.Data.Repositories.Interfaces;

    public class LegacyRedirectRepository : LegacyRepositoryBase<LegacyRedirectEntity, RedirectEntity, int>, IRedirectRepository {
        public LegacyRedirectRepository(LegacyDataContext context) : base(context) { }

        public override RedirectEntity GetById(int id) {
            throw new NotImplementedException();
        }

        public override Expression<Func<LegacyRedirectEntity, RedirectEntity>> Map() {
            throw new NotImplementedException();
        }
    }
}