namespace KalikoCMS.Configuration.Interfaces {
    public interface ICmsConfigurataion {
        string ConnectionString { get; }
        bool IgnoreStartPage { get; }
        bool WarmupProperties { get; }
    }
}