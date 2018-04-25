namespace KalikoCMS.Data.Entities {
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ContentPropertyEntity {
        [Key]
        public int ContentPropertyId { get; set; }

        public Guid ContentId { get; set; }
        public int PropertyId { get; set; }
        public int LanguageId { get; set; }
        public string ContentData { get; set; }
        public int Version { get; set; }
        public bool IsGlobal { get; set; }

        public virtual ContentEntity Content { get; set; }
        public virtual PropertyEntity Property { get; set; }
    }
}