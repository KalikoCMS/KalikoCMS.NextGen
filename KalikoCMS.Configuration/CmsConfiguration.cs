namespace KalikoCMS.Configuration {
    using System;
    using Interfaces;

    public class CmsConfiguration : ICmsConfigurataion {
        public string ConnectionString => throw new NotImplementedException();
        public bool IgnoreStartPage { get; set; }
        public bool WarmupProperties { get; set; }
    }
}