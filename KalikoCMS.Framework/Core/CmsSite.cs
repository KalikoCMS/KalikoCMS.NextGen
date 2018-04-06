
namespace KalikoCMS.Core {
    using KalikoCMS.Core.Interfaces;
    using System;
    using Collections;

    public class CmsSite : IContent {
        public Guid SiteId { get; set; }
        public string Author { get; set; }
        public SortDirection ChildSortDirection { get; set; }
        public SortOrder ChildSortOrder { get; set; }
        public string Name { get; set; }
        public DateTime UpdateDate { get; set; }
        public object Property { get; set; }
        public static SortDirection DefaultChildSortDirection { get; set; }
        public static SortOrder DefaultChildSortOrder { get; set; }
    }
}