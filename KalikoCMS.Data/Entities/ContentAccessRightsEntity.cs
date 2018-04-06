namespace KalikoCMS.Data.Entities {
    using System;
    using System.ComponentModel.DataAnnotations;
    using Security;

    public class ContentAccessRightsEntity {
        [Key]
        public int Id { get; set; }

        public Guid ContentId { get; set; }
        public string Name { get; set; }
        public bool IsUser { get; set; }
        public AccessRights AccessRights { get; set; }

        public virtual ContentEntity Content { get; set; }
    }
}
