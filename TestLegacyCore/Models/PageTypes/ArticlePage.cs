namespace TestLegacyCore.Models.PageTypes {
    using System;
    using KalikoCMS.Attributes;
    using KalikoCMS.Core;
    using KalikoCMS.PropertyType;

    [PageType("00000002-0000-0000-0000-000000000000", "Article Page", PageTypeDescription = "Create an Article Page", AllowedTypes = new Type[] { }, PreviewImage = "Assets/Images/article-page-type.png")]
    public class ArticlePage : CmsPage {
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

        [Property("Series")]
        public virtual StringProperty Series { get; set; }
    }
}