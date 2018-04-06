namespace KalikoCMS.Data.Entities {
    using System;
    using System.ComponentModel.DataAnnotations;

    public class RedirectEntity {
        [Key]
        public int RedirectId { get; set; }

        public int UrlHash { get; set; }
        public string Url { get; set; }
        public Guid ContentId { get; set; }
        public int LanguageId { get; set; }
    }
}