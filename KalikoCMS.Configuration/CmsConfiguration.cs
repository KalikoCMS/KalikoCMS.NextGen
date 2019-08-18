namespace KalikoCMS.Configuration {
    using System;
    using Interfaces;

    public class CmsConfiguration : ICmsConfiguration {
        public string ConnectionString => throw new NotImplementedException();
        public bool IgnoreStartPage { get; set; }
        public bool SkipEndingSlash { get; set; }
        public Guid StartPageId { get; set; }
        public bool WarmupProperties { get; set; }
    }
}