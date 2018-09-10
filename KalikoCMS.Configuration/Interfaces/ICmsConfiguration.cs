namespace KalikoCMS.Configuration.Interfaces {
    public interface ICmsConfiguration {
        string ConnectionString { get; }
        bool IgnoreStartPage { get; }
        bool WarmupProperties { get; }
    }
}