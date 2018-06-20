namespace KalikoCMS.Legacy.Data.Repositories {
    using System;
    using System.Linq.Expressions;
    using Entities;
    using KalikoCMS.Data.Entities;
    using KalikoCMS.Data.Repositories.Interfaces;

    public class LegacyLanguageRepository : LegacyRepositoryBase<LegacySiteLanguageEntity, LanguageEntity, int>, ILanguageRepository {
        public LegacyLanguageRepository(LegacyDataContext context) : base(context) { }

        public override LanguageEntity GetById(int id) {
            throw new NotImplementedException();
        }

        public override Expression<Func<LegacySiteLanguageEntity, LanguageEntity>> Map() {
            throw new NotImplementedException();
        }
    }
}