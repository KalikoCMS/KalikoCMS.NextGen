namespace TestLegacyCore.Models.PageTypes {
    using KalikoCMS.Attributes;
    using KalikoCMS.Core;
    using KalikoCMS.PropertyType;

    [PageType("00000001-0000-0000-0000-000000000000", "Article Page - Landing", PageTypeDescription = "Create an Article Landing Page", AllowedTypes = new[] {typeof(ArticleLandingPage), typeof(ArticlePage)}, PreviewImage = "Assets/Images/article-landing-page-type.png")]
    public class ArticleLandingPage : CmsPage {
        [Property("Heading")]
        public virtual StringProperty Heading { get; set; }

        [Property("Content")]
        public virtual HtmlProperty Content { get; set; }

        [Property("Excerpt")]
        public virtual TextProperty Excerpt { get; set; }

        [Property("Description")]
        public virtual StringProperty Description { get; set; }

        [Property("Keywords")]
        public virtual StringProperty Keywords { get; set; }

        [TagProperty("Categories")]
        public virtual TagProperty Categories { get; set; }

        [TagProperty("Tags")]
        public virtual TagProperty Tags { get; set; }
    }
}