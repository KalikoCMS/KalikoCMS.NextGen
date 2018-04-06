namespace KalikoCMS.Data.Entities {
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class TagEntity {
        [Key]
        public int TagId { get; set; }

        public string TagName { get; set; }
        public int TagContextId { get; set; }

        public TagContextEntity TagContext { get; set; }
        public ICollection<ContentTagEntity> ContentTags { get; set; }
    }
}