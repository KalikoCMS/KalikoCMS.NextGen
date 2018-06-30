namespace KalikoCMS.ServiceLocation {
    using System;
    using System.Collections.Generic;
    using Microsoft.Extensions.DependencyInjection;

    public class DependencyInjectionServiceLocator : ServiceLocatorImplBase {
        private readonly ServiceProvider _serviceProvider;

        public DependencyInjectionServiceLocator(ServiceProvider serviceProvider) {
            _serviceProvider = serviceProvider;
        }

        protected override object DoGetInstance(Type serviceType, string key) {
            return _serviceProvider.GetService(serviceType);
        }

        protected override IEnumerable<object> DoGetAllInstances(Type serviceType) {
            return _serviceProvider.GetServices(serviceType);
        }
    }
}
