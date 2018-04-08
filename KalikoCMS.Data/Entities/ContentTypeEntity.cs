namespace KalikoCMS.Data.Entities {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Core.Collections;

    public class ContentTypeEntity {
        [Key]
        public int ContentTypeId { get; set; }

        public Guid ContentProviderId { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public bool ShowInAdmin { get; set; }
        public SortDirection DefaultChildSortDirection { get; set; }
        public SortOrder DefaultChildSortOrder { get; set; }

        public virtual ContentProviderEntity ContentProviderEntity { get; set; }
        public virtual ICollection<PropertyEntity> Properties { get; set; }
        public virtual ICollection<ContentEntity> Contents { get; set; }
    }
}