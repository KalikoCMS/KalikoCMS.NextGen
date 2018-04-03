using KalikoCMS.Services;
using KalikoCMS.Services.Interfaces;
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
    public class SimpleInjectorProvider {
        public static Container Container { get; private set; }

#if NETFULL
        public static void Registere()
        {
            // Create the container as usual.
            var container = new Container();
            container.Options.DefaultScopedLifestyle = new WebRequestLifestyle();

            // Register your types, for instance:
            container.Register<IDemoService, DemoService>(Lifestyle.Scoped);

            // This is an extension method from the integration package.
            container.RegisterMvcControllers(Assembly.GetExecutingAssembly());

            container.Verify();

            Services.ServiceLocator.SetLocatorProvider(() => new SimpleInjectorServiceLocator(container));
            DependencyResolver.SetResolver(new SimpleInjectorDependencyResolver(container));
        }
#else
        public static void Registere(IServiceCollection services) {
            Container = new Container();
            Services.ServiceLocator.SetLocatorProvider(() => new SimpleInjectorServiceLocator(Container));

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

            // Register your types, for instance:
            Container.Register<IDemoService, DemoService>(Lifestyle.Scoped);

            // Allow Simple Injector to resolve services from ASP.NET Core.
            Container.AutoCrossWireAspNetComponents(app);

            Container.Verify();
        }
#endif
    }
}