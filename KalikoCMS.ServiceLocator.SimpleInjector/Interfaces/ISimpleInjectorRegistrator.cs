namespace KalikoCMS.ServiceLocation.Interfaces {
    using SimpleInjector;

    public interface ISimpleInjectorRegistrator {
        void Register(Container container);
    }
}
