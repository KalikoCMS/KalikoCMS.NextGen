namespace KalikoCMS.ServiceLocator.Interfaces {
    using SimpleInjector;

    public interface ISimpleInjectorRegistrator {
        void Register(Container container);
    }
}
