namespace KalikoCMS.Configuration.Interfaces {
    using System;

    public interface ICmsConfiguration {
        string ConnectionString { get; }
        bool IgnoreStartPage { get; }
        bool SkipEndingSlash { get; }
        Guid StartPageId { get; }
        bool WarmupProperties { get; }
    }
}