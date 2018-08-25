namespace KalikoCMS.Data.Repositories {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Core;
    using Entities;
    using Interfaces;
    using Microsoft.EntityFrameworkCore;

    public class PropertyRepository : RepositoryBase<PropertyEntity, int>, IPropertyRepository {
        private readonly CmsContext _context;

        public PropertyRepository(CmsContext context) : base(context) {
            _context = context;
        }

        public override PropertyEntity GetById(int id) {
            return _context.Set<PropertyEntity>()
                .AsNoTracking()
                .FirstOrDefault(e => e.PropertyId == id);
        }

        public List<ExtendedPropertyData> LoadAllProperties(Func<Guid, string, object> creator) {
            var properties = from p in _context.Properties
                join cp in _context.ContentProperties on p.PropertyId equals cp.PropertyId into merge
                from m in merge.DefaultIfEmpty(new ContentPropertyEntity())
                join pi in _context.ContentLanguages on new { m.ContentId, m.LanguageId, m.Version } equals new { pi.ContentId, pi.LanguageId, Version = pi.CurrentVersion }
                where pi.Status == ContentStatus.Published
                select new ExtendedPropertyData
                {
                    ContentId = pi.ContentId,
                    LanguageId = pi.LanguageId,
                    ContentPropertyId = m != null ? m.ContentPropertyId : 0,
                    PropertyName = p.Name.ToLowerInvariant(),
                    Value = creator(p.PropertyTypeId, m == null ? string.Empty : m.ContentData),
                    PropertyId = p.PropertyId,
                    PropertyTypeId = p.PropertyTypeId
                };

            return properties.ToList();
        }

        public List<PropertyData> LoadProperties(Guid contentId, int languageId, Guid contentTypeId, int version, Func<Guid, string, object> creator) {
            var properties = from p in _context.Properties
                join cp in _context.ContentProperties on new { PropertyId = p.PropertyId, ContentId = contentId, LanguageId = languageId, Version = version } equals new { cp.PropertyId, cp.ContentId, cp.LanguageId, cp.Version } into merge
                from m in merge.DefaultIfEmpty(new ContentPropertyEntity())
                where p.ContentTypeId == contentTypeId
                orderby p.SortOrder
                select new PropertyData
                {
                    ContentPropertyId = m == null ? 0 : m.ContentPropertyId,
                    PropertyName = p.Name.ToLowerInvariant(),
                    Value = creator(p.PropertyTypeId, m == null ? string.Empty : m.ContentData),
                    PropertyId = p.PropertyId,
                    PropertyTypeId = p.PropertyTypeId
                };

            return properties.ToList();
        }
    }
}