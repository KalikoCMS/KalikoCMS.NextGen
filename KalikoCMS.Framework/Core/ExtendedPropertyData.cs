namespace KalikoCMS.Core {
    using System;

    public class ExtendedPropertyData : PropertyData {
        public Guid ContentId { get; set; }
        public int LanguageId { get; set; }
    }
}