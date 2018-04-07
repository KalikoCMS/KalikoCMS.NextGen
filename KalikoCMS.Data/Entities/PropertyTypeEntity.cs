namespace KalikoCMS.Data.Entities {
    using System;
    using System.ComponentModel.DataAnnotations;

    public class PropertyTypeEntity {
        [Key]
        public Guid PropertyTypeId { get; set; }

        public string Name { get; set; }
        public string Class { get; set; }

        public PropertyTypeEntity() {
            PropertyTypeId = Guid.NewGuid();
        }
    }
}