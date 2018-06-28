namespace KalikoCMS.PropertyType {
    using System.Collections.Generic;
    using Attributes;
    using Core;

    [PropertyType("5A382737-2FFD-4AFE-A711-822863AF786C", "Tag", "Tag", "")]
    public class TagProperty : PropertyData {
        public string TagContext { get; set; }
        public List<string> Tags { get; set; }
    }
}
