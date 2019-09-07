﻿namespace KalikoCMS.ServiceLocation {
    using System;
    using AssemblyHelpers;
    using Interfaces;
    using Microsoft.Extensions.DependencyInjection;

    public class DependencyInjectionProvider : ServiceLocatorProviderBase {
        private IServiceCollection _serviceCollection;

        public IServiceProvider Initialize(IServiceCollection services) {
            _serviceCollection = services;

            RegisterCmsServices();

            var serviceProvider = _serviceCollection.BuildServiceProvider();
            ServiceLocator.SetLocatorProvider(() => new DependencyInjectionServiceLocator(serviceProvider));

            return serviceProvider;
        }

        public override void RegisterSingleton<TService, TImplementation>() {
            _serviceCollection.AddSingleton<TService, TImplementation>();
        }

        public override void RegisterScoped<TService, TImplementation>() {
            _serviceCollection.AddScoped<TService, TImplementation>();
        }

        public override void RegisterTransient<TService, TImplementation>() {
            _serviceCollection.AddTransient<TService, TImplementation>();
        }

        public override void RegisterUserServices() {
            var types = AssemblyLocator.GetTypesWithInterface<IDependencyInjectionRegistrator>();

            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type) as IDependencyInjectionRegistrator;

                instance?.Register(_serviceCollection);
            }
        }
    }
}