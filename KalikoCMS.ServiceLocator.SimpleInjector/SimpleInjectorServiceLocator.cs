﻿using System;
using System.Collections.Generic;
using System.Linq;
using KalikoCMS.Services.Interfaces;
using SimpleInjector;
using ActivationException = KalikoCMS.Services.ActivationException;

namespace KalikoCMS.ServiceLocator {
    public class SimpleInjectorServiceLocator : IServiceLocator {
        private const string NotSupportedMessage = "Keyed registration is not supported by the Simple Injector.";

        private readonly Container _container;

        /// <summary>
        /// Initializes a new instance of the <see cref="SimpleInjectorServiceLocator"/> class.
        /// </summary>
        /// <param name="container">The <see cref="Container"/> to adapt.</param>
        public SimpleInjectorServiceLocator(Container container) {
            if (container == null) {
                throw new ArgumentNullException("container");
            }

            this._container = container;
        }

        /// <summary>
        /// Get all instances of the given <typeparamref name="TService" /> currently
        /// registered in the container.
        /// </summary>
        /// <typeparam name="TService">Type of object requested.</typeparam>
        /// <exception cref="T:Microsoft.Practices.ServiceLocation.ActivationException">Thrown when there is are errors resolving
        /// the service instance.</exception>
        /// <returns>A sequence of instances of the requested <typeparamref name="TService" />.</returns>
        public virtual IEnumerable<TService> GetAllInstances<TService>() {
            try {
                return _container.GetAllInstances(typeof(TService)).Cast<TService>();
            }
            catch (SimpleInjector.ActivationException ex) {
                throw new ActivationException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Get all instances of the given <paramref name="serviceType" /> currently
        /// registered in the container.
        /// </summary>
        /// <param name="serviceType">Type of object requested.</param>
        /// <exception cref="T:Microsoft.Practices.ServiceLocation.ActivationException">Thrown when there is are errors resolving
        /// the service instance.</exception>
        /// <returns>A sequence of instances of the requested <paramref name="serviceType" />.</returns>
        public virtual IEnumerable<object> GetAllInstances(Type serviceType) {
            try {
                return this._container.GetAllInstances(serviceType);
            }
            catch (SimpleInjector.ActivationException ex) {
                throw new ActivationException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Get an instance of the given named <typeparamref name="TService" />.
        /// </summary>
        /// <typeparam name="TService">Type of object requested.</typeparam>
        /// <param name="key">Name the object was registered with.</param>
        /// <exception cref="T:Microsoft.Practices.ServiceLocation.ActivationException">Thrown when there is are errors resolving
        /// the service instance.</exception>
        /// <exception cref="NotSupportedException">Thrown when a non-null key is requested. Keyed 
        /// registration is not supported by the Simple Injector.</exception>
        /// <returns>The requested service instance.</returns>
        public virtual TService GetInstance<TService>(string key) {
            if (key == null) {
                return (TService) this.GetInstance(typeof(TService));
            }
            else {
                throw new NotSupportedException(NotSupportedMessage);
            }
        }

        /// <summary>
        /// Get an instance of the given <typeparamref name="TService" />.
        /// </summary>
        /// <typeparam name="TService">Type of object requested.</typeparam>
        /// <exception cref="T:Microsoft.Practices.ServiceLocation.ActivationException">Thrown when there is are errors resolving
        /// the service instance.</exception>
        /// <returns>The requested service instance.</returns>
        public virtual TService GetInstance<TService>() {
            try {
                return (TService) this._container.GetInstance(typeof(TService));
            }
            catch (SimpleInjector.ActivationException ex) {
                throw new ActivationException(ex.Message, ex);
            }
        }

        /// <summary>
        /// Get an instance of the given named <paramref name="serviceType" />.
        /// </summary>
        /// <param name="serviceType">Type of object requested.</param>
        /// <param name="key">Name the object was registered with.</param>
        /// <exception cref="T:Microsoft.Practices.ServiceLocation.ActivationException">Thrown when there is an error resolving
        /// the service instance.</exception>
        /// <exception cref="NotSupportedException">Thrown when a non-null key is requested. Keyed 
        /// registration is not supported by the Simple Injector.</exception>
        /// <returns>The requested service instance.</returns>
        public virtual object GetInstance(Type serviceType, string key) {
            if (key == null) {
                return this.GetInstance(serviceType);
            }
            else {
                throw new NotSupportedException(NotSupportedMessage);
            }
        }

        /// <summary>
        /// Get an instance of the given <paramref name="serviceType" />.
        /// </summary>
        /// <param name="serviceType">Type of object requested.</param>
        /// <exception cref="T:Microsoft.Practices.ServiceLocation.ActivationException">Thrown when there is an error resolving
        /// the service instance.</exception>
        /// <returns>The requested service instance.</returns>
        public virtual object GetInstance(Type serviceType) {
            try {
                return this._container.GetInstance(serviceType);
            }
            catch (SimpleInjector.ActivationException ex) {
                throw new ActivationException(ex.Message, ex);
            }
        }

        /// <summary>Gets the service object of the specified type.</summary>
        /// <returns>A service object of type serviceType.-or- null if there is no service object of type serviceType.</returns>
        /// <param name="serviceType">An object that specifies the type of service object to get. </param>
        public virtual object GetService(Type serviceType) {
            try {
                IServiceProvider serviceProvider = this._container;

                return serviceProvider.GetService(serviceType);
            }
            catch (SimpleInjector.ActivationException ex) {
                throw new ActivationException(ex.Message, ex);
            }
        }
    }
}