namespace KalikoCMS.Data.Entities {
    using System;

    public class ContentTagEntity {
        public Guid ContentId { get; set; }
        public int TagId { get; set; }

        public TagEntity Tag { get; set; }
        public ContentEntity Content { get; set; }
    }
}