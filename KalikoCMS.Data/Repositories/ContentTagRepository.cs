namespace KalikoCMS.Data.Repositories {
    using System;
    using Entities;
    using Interfaces;

    public class ContentTagRepository : RepositoryBase<ContentTagEntity, int>, IContentTagRepository {
        private readonly CmsContext _context;

        public ContentTagRepository(CmsContext context) : base(context) {
            _context = context;
        }

        public override ContentTagEntity GetById(int id) {
            throw new NotImplementedException();
        }
    }
}