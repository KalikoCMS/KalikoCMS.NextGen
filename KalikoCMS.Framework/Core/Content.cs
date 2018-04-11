namespace KalikoCMS.Core {
    using System;
    using Collections;
    using Interfaces;

    public abstract class Content : IContent { //MarshalByRefObject
        public Guid ContentId { get; set; }
        public Guid ContentTypeId { get; set; }
        public int SortOrder { get; set; }
        public int TreeLevel { get; set; }
        public int ContentLanguageId { get; set; }
        public int LanguageId { get; set; }
        public string ContentName { get; set; }
        public string UrlSegment { get; set; }
        public string ContentUrl { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime? StartPublish { get; set; }
        public DateTime? StopPublish { get; set; }
        public string Author { get; set; }
        public bool VisibleInMenu { get; set; }
        public bool VisibleInSitemap { get; set; }
        public int CurrentVersion { get; set; }
        public ContentStatus Status { get; set; }
        public SortDirection ChildSortDirection { get; set; }
        public SortOrder ChildSortOrder { get; set; }

        public bool IsEditable { get; internal set; }
        private PropertyCollection _propertyCollection;

        public PropertyCollection Property {
            get => _propertyCollection ?? (_propertyCollection = LoadProperties());
            internal set => _propertyCollection = value;
        }

        private PropertyCollection LoadProperties() {
            throw new NotImplementedException(); // TODO: !!!
            //Data.PropertyData.GetPropertiesForPage(PageId, LanguageId, PageTypeId, CurrentVersion, true)
        }
    }
}