namespace KalikoCMS.Data.Repositories {
    using System;
    using System.Linq;
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class ContentTypeRepository : RepositoryBase<ContentTypeEntity, Guid>, IContentTypeRepository {
        private readonly CmsContext _context;

        public ContentTypeRepository(CmsContext context) : base(context) {
            _context = context;
        }

        public override ContentTypeEntity GetById(Guid id) {
            return _context.Set<ContentTypeEntity>()
                .AsNoTracking()
                .FirstOrDefault(e => e.ContentTypeId == id);
        }
    }
}