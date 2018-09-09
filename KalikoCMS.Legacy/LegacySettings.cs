namespace KalikoCMS.Legacy {
    using System;

    public class LegacySettings {
        public const int LanguageId = 1;
        public static Guid SiteId;

        static LegacySettings() {
            SiteId = new Guid("C541EA37-9B7C-4634-85C3-41DE0BE24F66");
        }
    }
}
