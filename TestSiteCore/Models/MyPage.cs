namespace TestSiteCore.Models {
    using KalikoCMS.Attributes;
    using KalikoCMS.Core;
    using Microsoft.AspNetCore.Html;

    [PageType("262deec0-715e-49fd-989f-7d0b0d34f9f0", "My page")]
    public class MyPage : CmsPage {
        [Property("String property")]
        public virtual string TestString { get; set; }

        [Property("HtmlString property")]
        public virtual HtmlString TestHtmlString { get; set; }
    }

}