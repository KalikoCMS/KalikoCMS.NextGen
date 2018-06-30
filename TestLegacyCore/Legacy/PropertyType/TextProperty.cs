namespace KalikoCMS.PropertyType {
    using System;
    using System.ComponentModel.DataAnnotations;
    using Attributes;

    [Obsolete("Use the string data type instead in your model.")]
    [PropertyType("DA31814B-99D9-4459-92C3-12DFEEEE9449", "Text", "Text", "%AdminPath%Content/PropertyType/TextPropertyEditor.ascx")]
    public class TextProperty {
        public string Value { get; set; }

        public override string ToString() {
            return Value;
        }
    }
}