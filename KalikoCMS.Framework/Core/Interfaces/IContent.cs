namespace KalikoCMS.Core.Interfaces {
    using System;
    using Collections;

    public interface IContent {
        // Content
        Guid ContentId { get; }
        Guid ContentProviderId { get; }
        Guid ContentTypeId { get; }
        Guid ParentId { get; }
        int SortOrder { get; }
        int TreeLevel { get; }

        // ContentLanguage
        int ContentLanguageId { get; }
        int LanguageId { get; }
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

        ContentReference ContentReference { get; }
        bool IsEditable { get; }
        PropertyCollection Property { get; }

        void SetDefaults();
    }
}