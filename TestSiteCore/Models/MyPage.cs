namespace TestSiteCore.Models {
    using KalikoCMS.Attributes;
    using KalikoCMS.Core;

    [PageType("MyPage", "My page")]
    public class MyPage : CmsPage {
        [Property("Test")]
        public virtual string Test { get; set; }
    }
}