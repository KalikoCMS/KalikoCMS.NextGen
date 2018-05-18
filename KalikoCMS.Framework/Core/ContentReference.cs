namespace KalikoCMS.Core {
    using System;

    public class ContentReference {
        public Guid ContentId { get; }
        public int LanguageId { get; }

        public ContentReference(Guid contentId, int languageId) {
            ContentId = contentId;
            LanguageId = languageId;
        }
    }
}