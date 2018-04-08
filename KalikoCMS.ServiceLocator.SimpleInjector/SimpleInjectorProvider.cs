using SimpleInjector;
#if NETFULL
using System;
using System.Reflection;
using System.Web.Mvc;
using SimpleInjector.Integration.Web;
using SimpleInjector.Integration.Web.Mvc;
#else
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.ViewComponents;
using Microsoft.Extensions.DependencyInjection;
using SimpleInjector.Integration.AspNetCore.Mvc;
using SimpleInjector.Lifestyles;
#endif

namespace KalikoCMS.ServiceLocator {
    using System;
    using AssemblyHelpers;
    using Data;
    using Data.Repositories;
    using Data.Repositories.Interfaces;
    using Interfaces;
    using Services.Content;
    using Services.Content.Interfaces;
    using KalikoCMS.Services.Resolvers;
    using KalikoCMS.Services.Resolvers.Interfaces;
    using Services.Initialization.Interfaces;

    public class SimpleInjectorProvider {
        public static Container Container { get; private set; }

        static SimpleInjectorProvider() {
            Container = new Container();
        }

#if NETFULL
        public static void InitializeContainer() {
            Container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            RegisterCmsServices();

            // This is an extension method from the integration package.
            Container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            Container.Verify();

            Services.ServiceLocator.SetLocatorProvider(() => new SimpleInjectorServiceLocator(Container));
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(Container));
        }
#else
        public static void RegisterServices(IServiceCollection services) {
            Container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(Container));
            services.AddSingleton<IViewComponentActivator>(new SimpleInjectorViewComponentActivator(Container));

            services.EnableSimpleInjectorCrossWiring(Container);
            services.UseSimpleInjectorAspNetRequestScoping(Container);
        }

        public static void InitializeContainer(IApplicationBuilder app) {
            // Add application presentation components:
            Container.RegisterMvcControllers(app);
            Container.RegisterMvcViewComponents(app);

            RegisterCmsServices();

            // Allow Simple Injector to resolve services from ASP.NET Core.
            Container.AutoCrossWireAspNetComponents(app);

            Container.Verify();

            Services.ServiceLocator.SetLocatorProvider(() => new SimpleInjectorServiceLocator(Container));
        }
#endif

        public static void RegisterDataProvider<T>() where T : CmsContext {
            Container.Register<CmsContext, T>(Lifestyle.Scoped);
        }

        private static void RegisterCmsServices() {
            Container.Register<IContentCreator, ContentCreator>(Lifestyle.Singleton);
            Container.Register<IContentIndexService, ContentIndexService>(Lifestyle.Singleton);
            Container.Register<IContentLoader, ContentLoader>(Lifestyle.Singleton);
            Container.Register<IContentRepository, ContentRepository>();
            Container.Register<IHttpContextResolver, HttpContextResolver>();
            Container.Register<IInitializationService, IInitializationService>(Lifestyle.Singleton);

            RegisterUserServices();
        }

        private static void RegisterUserServices() {
            var types = AssemblyLocator.GetTypesWithInterface<ISimpleInjectorRegistration>();

            foreach (var type in types) {
                var instance = Activator.CreateInstance(type) as ISimpleInjectorRegistration;

                instance?.Register(Container);
            }
        }
    }
}