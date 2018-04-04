namespace KalikoCMS.Core.Interfaces {
    public interface IStartupSequence {
        void Startup();
        int StartupOrder { get; }
    }
}
