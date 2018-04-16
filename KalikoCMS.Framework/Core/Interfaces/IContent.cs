namespace KalikoCMS.Core.Interfaces {
    using System;
    using Collections;

    public interface IContent {
        // Content
        Guid ContentId { get; }
        Guid ContentTypeId { get; set; }
        Guid ParentId { get; set; }
        int SortOrder { get; }
        int TreeLevel { get; }

        // ContentLanguage
        int ContentLanguageId { get; }
        int LanguageId { get; set; }
        string ContentName { get; set; }
        string UrlSegment { get; set; }
        string ContentUrl { get; set; }
        DateTime CreatedDate { get; set; }
        DateTime UpdateDate { get; set; }
        DateTime? StartPublish { get; set; }
        DateTime? StopPublish { get; set; }
        string Author { get; }
        bool VisibleInMenu { get; set; } //??
        bool VisibleInSitemap { get; set; } //??
        int CurrentVersion { get; }
        ContentStatus Status { get; set; }
        SortDirection ChildSortDirection { get; set; }
        SortOrder ChildSortOrder { get; set; }

        bool IsEditable { get; }
        PropertyCollection Property { get; }

        void SetDefaults();
    }
}