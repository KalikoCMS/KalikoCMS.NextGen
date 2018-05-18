namespace KalikoCMS.Core {
    using System;

    public class DomainInformation {
        public int DomainId { get; set; }

        public Guid ContentId { get; set; }
        public int LanguageId { get; set; }
        public string DomainName { get; set; }
        public int? Port { get; set; }
        public string ApplicationName { get; set; }
        public bool IsPrimary { get; set; }
        public bool EnforceHttps { get; set; }
        public bool UseLanguagePrefix { get; set; }
    }
}
