namespace KalikoCMS.ServiceLocation {
    using System;
    using AssemblyHelpers;
    using Data;
    using Interfaces;
    using SimpleInjector;
#if NETFULL
    using System.Reflection;
    using System.Web.Mvc;
    using SimpleInjector.Integration.Web;
    using SimpleInjector.Integration.Web.Mvc;
#else
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Mvc.ViewComponents;
    using Microsoft.Extensions.DependencyInjection;
    using SimpleInjector.Integration.AspNetCore.Mvc;
    using SimpleInjector.Lifestyles;

#endif

    public class SimpleInjectorProvider : ServiceLocatorProviderBase {
        public static Container Container { get; private set; }

        static SimpleInjectorProvider() {
            Container = new Container();
        }

#if NETFULL
        public void InitializeContainer() {
            Container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();
            Container.Options.AllowOverridingRegistrations = true;

            RegisterCmsServices();

            // This is an extension method from the integration package.
            Container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            Container.Verify();

            ServiceLocator.SetLocatorProvider(() => new SimpleInjectorServiceLocator(Container));
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(Container));
        }
#else
        public static void RegisterServices(IServiceCollection services) {
            Container.Options.DefaultScopedLifestyle = new AsyncScopedLifestyle();
            Container.Options.AllowOverridingRegistrations = true;

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddSingleton<IControllerActivator>(new SimpleInjectorControllerActivator(Container));
            services.AddSingleton<IViewComponentActivator>(new SimpleInjectorViewComponentActivator(Container));

            services.EnableSimpleInjectorCrossWiring(Container);
            services.UseSimpleInjectorAspNetRequestScoping(Container);
        }

        public void InitializeContainer(IApplicationBuilder app) {
            // Obsolete, set through services.AddSimpleInjector(container, options => options.AddAspNetCore().AddControllerActivation())
            //Container.RegisterMvcControllers(app);
            //Container.RegisterMvcViewComponents(app);

            RegisterCmsServices();

            // Allow Simple Injector to resolve services from ASP.NET Core.
            Container.AutoCrossWireAspNetComponents(app);

            Container.Verify();

            ServiceLocator.SetLocatorProvider(() => new SimpleInjectorServiceLocator(Container));
        }
#endif

        public static void RegisterDataProvider<T>() where T : CmsContext {
            Container.Register<CmsContext, T>(Lifestyle.Scoped);
        }

        public override void RegisterSingleton<TService, TImplementation>() {
            Container.Register<TService, TImplementation>(Lifestyle.Singleton);
        }

        public override void RegisterScoped<TService, TImplementation>() {
            Container.Register<TService, TImplementation>(Lifestyle.Scoped);
        }

        public override void RegisterTransient<TService, TImplementation>() {
            Container.Register<TService, TImplementation>(Lifestyle.Transient);
        }

        public override void RegisterUserServices() {
            var types = AssemblyLocator.GetTypesWithInterface<ISimpleInjectorRegistrator>();

            foreach (var type in types) {
                var instance = Activator.CreateInstance(type) as ISimpleInjectorRegistrator;

                instance?.Register(Container);
            }
        }
    }
}