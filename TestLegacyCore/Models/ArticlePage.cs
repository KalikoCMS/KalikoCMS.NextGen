namespace TestLegacyCore.Models {
    using System.ComponentModel.DataAnnotations;
    using KalikoCMS.Attributes;
    using KalikoCMS.Core;
    using KalikoCMS.PropertyType;

    [PageType("00000001-0000-0000-0000-000000000000", "Article page", PageTypeDescription = "Used for articles", PreviewImage = "/Assets/Images/articlepage.png")]
    public class ArticlePage : CmsPage {
        /// <summary>
        /// Simple string headline to use instead of pagename in the articles.
        /// </summary>
        [Property("Headline")]
        public virtual StringProperty Headline { get; set; }

        /// <summary>
        /// To set a required width and/or height for images use the [ImageProperty] 
        /// attribute instead of the standard [Property]
        /// </summary>
        //[ImageProperty("Top image", Width = 848, Height = 180)]
        [Property("Top image")]
        public virtual ImageProperty TopImage { get; set; }

        [Property("Preamble")]
        public virtual TextProperty Preamble { get; set; }

        [Property("Main body")]
        public virtual HtmlProperty MainBody { get; set; }

        /// <summary>
        /// The tag property enable tags for a particular page type. Notice that you can 
        /// use multiple tag spaces for the same page by setting different tag contexts.
        /// Be sure to use [TagProperty] to define the TagContext, otherwise it will 
        /// fallback to the standard tag space.
        /// </summary>
        //[TagProperty("Tags", TagContext = "article")]
        //public virtual TagProperty Tags { get; set; }
    }
}