namespace KalikoCMS.PropertyType {
    using System;
    using System.IO;
    using System.Text.Encodings.Web;
    using Attributes;
    using Microsoft.AspNetCore.Html;

    [Obsolete("Use the HtmlString data type instead in your model.")]
    [PropertyType("18873bf3-d3a4-4389-bef1-0949664ee09c", "HTML", "HTML String", "%AdminPath%Content/PropertyType/HtmlPropertyEditor.ascx")]
    public class HtmlProperty : IHtmlContent {
        public string Value { get; set; }

        public void WriteTo(TextWriter writer, HtmlEncoder encoder) {
            writer.Write(Value);
        }
    }
}