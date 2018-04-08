namespace KalikoCMS.Data.Entities {
    using System;
    using System.ComponentModel.DataAnnotations;

    public class ContentProviderEntity {
        [Key]
        public Guid ContentProviderId { get; set; }

        public string Name { get; set; }
        public string Class { get; set; }

        public ContentProviderEntity() {
            ContentProviderId = Guid.NewGuid();
        }
    }
}