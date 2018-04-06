namespace KalikoCMS.Data.Entities {
    using System.ComponentModel.DataAnnotations;

    public class ContentProviderEntity {
        [Key]
        public int ContentProviderId { get; set; }

        public string Name { get; set; }
        public string Class { get; set; }
    }
}