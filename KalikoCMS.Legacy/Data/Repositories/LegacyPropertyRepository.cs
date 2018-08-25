namespace KalikoCMS.Legacy.Data.Repositories {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using Core;
    using Entities;
    using KalikoCMS.Data.Entities;
    using KalikoCMS.Data.Repositories.Interfaces;

    public class LegacyPropertyRepository : LegacyRepositoryBase<LegacyPropertyEntity, PropertyEntity, int>, IPropertyRepository {
        private readonly LegacyDataContext _context;

        public LegacyPropertyRepository(LegacyDataContext context) : base(context) {
            _context = context;
        }

        public override PropertyEntity GetById(int id) {
            return FirstOrDefault(x => x.PropertyId == id);
        }

        public override Expression<Func<LegacyPropertyEntity, PropertyEntity>> Map() {
            return x => new PropertyEntity {
                ContentTypeId = ToGuid(x.PageTypeId),
                SortOrder = x.SortOrder,
                Name = x.Name,
                PropertyTypeId = x.PropertyTypeId,
                PropertyId = x.PropertyId,
                Header = x.Header,
                Localize = false,
                Parameters = x.Parameters,
                Required = x.Required
            };
        }

        public List<PropertyData> LoadProperties(Guid contentId, int languageId, Guid contentTypeId, int version, Func<Guid, string, object> creator) {
            var properties = from p in _context.Properties
                join cp in _context.PageProperties on new {PropertyId = p.PropertyId, PageId = contentId, LanguageId = languageId, Version = version} equals new {cp.PropertyId, cp.PageId, cp.LanguageId, cp.Version} into merge
                from m in merge.DefaultIfEmpty(new LegacyPagePropertyEntity())
                where p.PageTypeId == ToInt(contentTypeId)
                orderby p.SortOrder
                select new PropertyData {
                    ContentPropertyId = m != null ? m.PagePropertyId : 0,
                    PropertyName = p.Name.ToLowerInvariant(),
                    Value = creator(p.PropertyTypeId, m == null ? string.Empty : m.PageData),
                    PropertyId = p.PropertyId,
                    PropertyTypeId = p.PropertyTypeId
                };

            return properties.ToList();
        }

        public List<ExtendedPropertyData> LoadAllProperties(Func<Guid, string, object> creator) {
            var properties = from p in _context.Properties
                join cp in _context.PageProperties on p.PropertyId equals cp.PropertyId into merge
                from m in merge.DefaultIfEmpty(new LegacyPagePropertyEntity())
                join pi in _context.PageInstances on new { m.PageId, m.LanguageId, m.Version } equals new { pi.PageId, pi.LanguageId, Version = pi.CurrentVersion }
                where pi.Status == ContentStatus.Published
                select new ExtendedPropertyData
                {
                    ContentId = pi.PageId,
                    LanguageId = pi.LanguageId,
                    ContentPropertyId = m != null ? m.PagePropertyId : 0,
                    PropertyName = p.Name.ToLowerInvariant(),
                    Value = creator(p.PropertyTypeId, m == null ? string.Empty : m.PageData),
                    PropertyId = p.PropertyId,
                    PropertyTypeId = p.PropertyTypeId
                };

            return properties.ToList();
        }

        public override void Create(PropertyEntity entity) {
            // Supress
        }

        public override void Update(PropertyEntity entity) {
            // Supress
        }

        public override void Delete(int id) {
            // Supress
        }
    }
}