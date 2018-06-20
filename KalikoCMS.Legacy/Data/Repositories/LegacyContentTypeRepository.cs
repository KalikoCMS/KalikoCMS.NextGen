namespace KalikoCMS.Legacy.Data.Repositories {
    using System;
    using System.Linq.Expressions;
    using Entities;
    using KalikoCMS.Data.Entities;
    using KalikoCMS.Data.Repositories.Interfaces;

    public class LegacyContentTypeRepository : LegacyRepositoryBase<LegacyPageTypeEntity, ContentTypeEntity, Guid>, IContentTypeRepository {
        public LegacyContentTypeRepository(LegacyDataContext context) : base(context) { }

        public override ContentTypeEntity GetById(Guid id) {
            return FirstOrDefault(x => x.ContentTypeId == id);
        }

        public override Expression<Func<LegacyPageTypeEntity, ContentTypeEntity>> Map() {
            return x => new ContentTypeEntity {
                ContentTypeId = ToGuid(x.PageTypeId),
                DefaultChildSortDirection = x.DefaultChildSortDirection,
                ContentProviderId = Guid.Empty,
                DefaultChildSortOrder = x.DefaultChildSortOrder,
                DisplayName = x.DisplayName,
                Name = x.Name,
                ShowInAdmin = true
            };
        }

        public override void Create(ContentTypeEntity entity) {
            // Supress
        }

        public override void Update(ContentTypeEntity entity) {
            // Supress
        }

        public override void Delete(Guid id) {
            // Supress
        }
    }
}