namespace TestSiteCore.Models {
    using System;
    using KalikoCMS.Attributes;
    using KalikoCMS.Core;

    [PageType("262deec0-715e-49fd-989f-7d0b0d34f9f0", "My page")]
    public class MyPage : CmsPage {
        [Property("Test")]
        public virtual string Test { get; set; }
    }
}