namespace KalikoCMS.Legacy.Data.Repositories {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using KalikoCMS.Data.Entities;
    using KalikoCMS.Data.Repositories.Interfaces;

    public class LegacyDomainRepository : LegacyRepositoryBase<DomainEntity, DomainEntity, int>, IDomainRepository {
        private static readonly DomainEntity Domain;

        static LegacyDomainRepository() {
            Domain = new DomainEntity()
            {
                DomainId = 1,
                DomainName = "*",
                Port = 50176,
                ContentId = new Guid("C541EA37-9B7C-4634-85C3-41DE0BE24F66"),
                IsPrimary = true
            };
        }

        public LegacyDomainRepository(LegacyDataContext context) : base(context) { }

        public override DomainEntity GetById(int id) {
            return Domain;
        }

        public override IQueryable<DomainEntity> GetAll() {
            return new List<DomainEntity> { Domain }.AsQueryable();
        }

        public override Expression<Func<DomainEntity, DomainEntity>> Map() {
            return x => x;
        }
    }
}