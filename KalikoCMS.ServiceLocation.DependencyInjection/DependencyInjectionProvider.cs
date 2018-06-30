namespace KalikoCMS.ServiceLocation {
    using Microsoft.Extensions.DependencyInjection;

    public class DependencyInjectionProvider : ServiceLocatorProviderBase {
        private IServiceCollection _serviceCollection;

        public void Initialize(IServiceCollection services) {
            _serviceCollection = services;

            RegisterCmsServices();

            var serviceProvider = _serviceCollection.BuildServiceProvider();
            ServiceLocator.SetLocatorProvider(() => new DependencyInjectionServiceLocator(serviceProvider));
        }

        public override void RegisterSingelton<TService, TImplementation>() {
            _serviceCollection.AddSingleton<TService, TImplementation>();
        }

        public override void RegisterScoped<TService, TImplementation>() {
            _serviceCollection.AddScoped<TService, TImplementation>();
        }

        public override void RegisterTransient<TService, TImplementation>() {
            _serviceCollection.AddTransient<TService, TImplementation>();
        }

        public override void RegisterUserServices() {
            throw new System.NotImplementedException();
        }
    }
}