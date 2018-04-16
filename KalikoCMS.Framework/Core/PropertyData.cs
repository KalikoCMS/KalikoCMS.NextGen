namespace KalikoCMS.Core {
    using System;

    public class PropertyData {
        public int ContentPropertyId { get; set; }
        public int PropertyId { get; set; }
        public Guid PropertyTypeId { get; internal set; }
        public string PropertyName { get; set; }
        public object Value { get; internal set; }
    }
}