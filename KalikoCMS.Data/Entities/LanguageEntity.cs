namespace KalikoCMS.Data.Entities {
    using System.ComponentModel.DataAnnotations;

    public class LanguageEntity {
        [Key]
        public int LanguageId { get; set; }

        public string Culture { get; set; }
        public string DisplayName { get; set; }
        public string UrlSegment { get; set; }
    }
}