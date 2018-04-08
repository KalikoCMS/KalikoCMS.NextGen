namespace KalikoCMS.ServiceLocator.Interfaces {
    using SimpleInjector;

    public interface ISimpleInjectorRegistration {
        void Register(Container container);
    }
}
