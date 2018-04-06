namespace KalikoCMS.Data.Entities {
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class TagContextEntity {
        [Key]
        public int TagContextId { get; set; }

        public string ContextName { get; set; }
        public ICollection<TagEntity> Tags { get; set; }
    }
}