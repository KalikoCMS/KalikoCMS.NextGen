namespace KalikoCMS.Configuration.Interfaces {
    public interface ICmsConfigurataion {
        string ConnectionString { get; }
        bool WarmupProperties { get; }
    }
}