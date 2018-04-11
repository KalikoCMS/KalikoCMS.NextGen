namespace KalikoCMS.Core.Interfaces {
    using System;
    using Collections;

    public interface IContent {
        // Content
        Guid ContentId { get; set; }
        Guid ContentTypeId { get; set; }
        int SortOrder { get; set; }
        int TreeLevel { get; set; }

        // ContentLanguage
        int ContentLanguageId { get; set; }
        int LanguageId { get; set; }
        string ContentName { get; set; }
        string UrlSegment { get; set; }
        string ContentUrl { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime UpdateDate { get; set; }
        DateTime? StartPublish { get; set; }
        DateTime? StopPublish { get; set; }
        string Author { get; set; }
        bool VisibleInMenu { get; set; } //??
        bool VisibleInSitemap { get; set; } //??
        int CurrentVersion { get; set; }
        ContentStatus Status { get; set; }
        SortDirection ChildSortDirection { get; set; }
        SortOrder ChildSortOrder { get; set; }

        bool IsEditable { get; }
    }
}