namespace KalikoCMS.ServiceLocation {
    using System;
    using Microsoft.Extensions.DependencyInjection;

    public class DependencyInjectionProviderFactory : IServiceProviderFactory<IServiceCollection> {
        private readonly ServiceProviderOptions _options;

        public DependencyInjectionProviderFactory() : this(new ServiceProviderOptions()) { }

        public DependencyInjectionProviderFactory(ServiceProviderOptions options) {
            if (options == null) {
                throw new ArgumentNullException(nameof(options));
            }

            _options = options;
        }

        /// <inheritdoc />
        public IServiceCollection CreateBuilder(IServiceCollection services) {
            return services;
        }

        /// <inheritdoc />
        public IServiceProvider CreateServiceProvider(IServiceCollection containerBuilder) {
            var dependencyInjectionProvider = new DependencyInjectionProvider();
            return dependencyInjectionProvider.Initialize(containerBuilder);
        }
    }
}