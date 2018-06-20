namespace KalikoCMS.Legacy.Data.Repositories {
    using System;
    using System.Linq.Expressions;
    using Entities;
    using KalikoCMS.Data.Entities;
    using KalikoCMS.Data.Repositories.Interfaces;

    public class LegacySystemInformationRepository : LegacyRepositoryBase<LegacySystemInfoEntity, SystemInformationEntity, int>, ISystemInformationRepository {
        public LegacySystemInformationRepository(LegacyDataContext context) : base(context) { }

        public override SystemInformationEntity GetById(int id) {
            throw new NotImplementedException();
        }

        public override Expression<Func<LegacySystemInfoEntity, SystemInformationEntity>> Map() {
            throw new NotImplementedException();
        }
    }
}