namespace TestSiteCore.Services {
    using KalikoCMS.ServiceLocator.Interfaces;
    using SimpleInjector;

    public class ServiceRegistrator : ISimpleInjectorRegistrator {
        public void Register(Container container) {
            container.Register<ILocalService, LocalServices>();
        }
    }
}