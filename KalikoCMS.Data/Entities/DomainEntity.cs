namespace KalikoCMS.Data.Entities {
    using System;
    using System.ComponentModel.DataAnnotations;

    public class DomainEntity {
        [Key]
        public int DomainId { get; set; }

        public Guid ContentId { get; set; }
        public int LanguageId { get; set; }
        [Required]
        public string DomainName { get; set; }
        public int? Port { get; set; }
        public string ApplicationName { get; set; }
        public bool IsPrimary { get; set; }
        public bool EnforceHttps { get; set; }
        public bool UseLanguagePrefix { get; set; }

        public virtual ContentEntity Content { get; set; }
        public virtual LanguageEntity Language { get; set; }
    }
}