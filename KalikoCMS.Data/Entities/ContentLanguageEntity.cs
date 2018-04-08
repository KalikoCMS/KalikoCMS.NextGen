namespace KalikoCMS.Data.Entities {
    using System;
    using System.ComponentModel.DataAnnotations;
    using Core;
    using Core.Collections;

    public class ContentLanguageEntity {
        [Key]
        public int ContentLanguageId { get; set; }

        public Guid ContentId { get; set; }
        public int LanguageId { get; set; }
        public bool IsOriginal { get; set; }
        public string ContentName { get; set; }
        public string UrlSegment { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public DateTime? DeletedDate { get; set; }
        public DateTime? StartPublish { get; set; }
        public DateTime? StopPublish { get; set; }
        public string Author { get; set; }
        public bool VisibleInMenu { get; set; } //??
        public bool VisibleInSitemap { get; set; } //??
        public int CurrentVersion { get; set; }
        public ContentStatus Status { get; set; }
        public SortDirection ChildSortDirection { get; set; }
        public SortOrder ChildSortOrder { get; set; }

        public virtual ContentEntity Content { get; set; }
        public virtual LanguageEntity Language { get; set; }
    }
}