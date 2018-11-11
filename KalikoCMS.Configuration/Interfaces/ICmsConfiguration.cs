namespace KalikoCMS.Configuration.Interfaces {
    public interface ICmsConfiguration {
        string ConnectionString { get; }
        bool IgnoreStartPage { get; }
        bool SkipEndingSlash { get; }
        bool WarmupProperties { get; }
    }
}