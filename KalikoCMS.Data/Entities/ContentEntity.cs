namespace KalikoCMS.Data.Entities {
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ContentEntity {
        [Key]
        public Guid ContentId { get; set; }

        public Guid ContentTypeId { get; set; }
        public Guid ParentId { get; set; }

        public int SortOrder { get; set; }
        public int TreeLevel { get; set; }

        public ContentTypeEntity ContentType { get; set; }
        public ICollection<ContentTagEntity> ContentTags { get; set; }
        public ICollection<ContentLanguageEntity> ContentLanguages { get; set; }
        public ICollection<ContentPropertyEntity> ContentProperties { get; set; }

        public ContentEntity() {
            ContentId = Guid.NewGuid();
        }
    }
}