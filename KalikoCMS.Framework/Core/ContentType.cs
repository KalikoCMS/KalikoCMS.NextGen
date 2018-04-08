namespace KalikoCMS.Core {
    using System;
    using System.Collections.Generic;
    using Collections;

    public class ContentType {
        public int ContentTypeId { get; set; }
        public Guid ContentProviderId { get; set; }
        public string Name { get; set; }
        public SortDirection DefaultChildSortDirection { get; set; }
        public SortOrder DefaultChildSortOrder { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public string PreviewImage { get; set; }
        public bool ShowInAdmin { get; set; }
        public Type[] AllowedTypes { get; set; }
        public Type Type { get; set; }

        public List<PropertyDefinition> Properties { get; set; }

        public ContentType() {
            Properties = new List<PropertyDefinition>();
        }
    }
}
