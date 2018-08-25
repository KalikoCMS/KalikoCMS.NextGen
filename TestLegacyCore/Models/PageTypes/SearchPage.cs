namespace TestLegacyCore.Models.PageTypes {
    using KalikoCMS.Attributes;
    using KalikoCMS.Core;

    /// <summary>
    /// This is a page type for the search page. As you can see there's no properties defined, so the page only uses the builtin ones.
    /// </summary>
    [PageType("00000003-0000-0000-0000-000000000000", "Search page", PageTypeDescription = "Used for search page", PreviewImage = "/Assets/Images/searchpage.png")]
    public class SearchPage : CmsPage {
    }
}