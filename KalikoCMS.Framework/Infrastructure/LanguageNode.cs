namespace KalikoCMS.Infrastructure {
    using System;
    using Core;
    using Core.Collections;

    public class LanguageNode {
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
        public bool VisibleInMenu { get; set; } //??
        public bool VisibleInSitemap { get; set; } //??
        public int CurrentVersion { get; set; }
        public ContentStatus Status { get; set; }
        public SortDirection ChildSortDirection { get; set; }
        public SortOrder ChildSortOrder { get; set; }
    }
}