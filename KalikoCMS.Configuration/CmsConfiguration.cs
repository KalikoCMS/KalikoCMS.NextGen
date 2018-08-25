namespace KalikoCMS.Configuration {
    using System;
    using Interfaces;

    public class CmsConfiguration : ICmsConfigurataion {
        public string ConnectionString => throw new NotImplementedException();
        public bool WarmupProperties { get; set; }
    }
}