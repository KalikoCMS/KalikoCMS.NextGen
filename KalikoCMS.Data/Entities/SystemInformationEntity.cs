namespace KalikoCMS.Data.Entities {
    using System.ComponentModel.DataAnnotations;

    public class SystemInformationEntity {
        [Key]
        public int Id { get; set; }

        public int DatabaseVersion { get; set; }
    }
}