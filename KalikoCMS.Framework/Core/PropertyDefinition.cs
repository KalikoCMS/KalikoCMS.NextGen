namespace KalikoCMS.Core {
    using System;

    public class PropertyDefinition {
        public int PropertyId { get; set; }
        public Guid PropertyTypeId { get; set; }
        public Guid ContentTypeId { get; set; }
        public string Name { get; set; }
        public string Header { get; set; }
        public bool ShowInAdmin { get; set; }
        public int SortOrder { get; set; }
        public string Parameters { get; set; }
        public bool Required { get; set; }
        public string TabGroup { get; set; }
    }
}