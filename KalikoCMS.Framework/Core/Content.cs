namespace KalikoCMS.Core {
    using System;
    using Collections;
    using Interfaces;
    using ServiceLocation;

    public abstract class Content : IContent { //MarshalByRefObject
        public Guid ContentId { get; internal set; }
        public Guid ContentTypeId { get; set; }
        public Guid ParentId { get; set; }
        public int SortOrder { get; internal set; }
        public int TreeLevel { get; internal set; }
        public int ContentLanguageId { get; internal set; }
        public int LanguageId { get; set; }
        public string ContentName { get; set; }
        public string UrlSegment { get; set; }
        public string ContentUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime? StartPublish { get; set; }
        public DateTime? StopPublish { get; set; }
        public string Author { get; internal set; }
        public bool VisibleInMenu { get; set; }
        public bool VisibleInSitemap { get; set; }
        public int CurrentVersion { get; internal set; }
        public ContentStatus Status { get; set; }
        public SortDirection ChildSortDirection { get; set; }
        public SortOrder ChildSortOrder { get; set; }

        public bool IsEditable { get; internal set; }
        private PropertyCollection _propertyCollection;

        public virtual void SetDefaults() {
            // No defaults
        }

        public PropertyCollection Property {
            get => _propertyCollection ?? (_propertyCollection = LoadProperties());
            internal set => _propertyCollection = value;
        }

        private PropertyCollection LoadProperties() {
            var propertyResolver = ServiceLocator.Current.GetInstance<IPropertyResolver>();
            return  propertyResolver.GetProperties(ContentId, LanguageId, ContentTypeId, CurrentVersion, true);
        }
    }
}