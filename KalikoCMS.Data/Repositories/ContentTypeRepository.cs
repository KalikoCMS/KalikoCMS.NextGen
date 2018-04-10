namespace KalikoCMS.Data.Repositories {
    using System;
    using System.Linq;
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class ContentTypeRepository : RepositoryBase<ContentTypeEntity, Guid>, IContentTypeRepository {
        private readonly CmsContext _cmsContext;

        public ContentTypeRepository(CmsContext cmsContext) : base(cmsContext) {
            _cmsContext = cmsContext;
        }

        public override ContentTypeEntity GetById(Guid id) {
            return _cmsContext.Set<ContentTypeEntity>()
                .AsNoTracking()
                .FirstOrDefault(e => e.ContentTypeId == id);
        }
    }
}