namespace KalikoCMS.Data.Entities {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class PropertyEntity {
        [Key]
        public int PropertyId { get; set; }

        public Guid PropertyTypeId { get; set; }
        public Guid ContentTypeId { get; set; }
        public string Name { get; set; }
        public string Header { get; set; }
        public bool Localize { get; set; }
        public int SortOrder { get; set; }
        public string Parameters { get; set; }
        public bool Required { get; set; }
        public string TabGroup { get; set; }

        public ContentTypeEntity ContentType { get; set; }
        public PropertyTypeEntity PropertyType { get; set; }
        public ICollection<ContentPropertyEntity> ContentProperties { get; set; }
    }
}