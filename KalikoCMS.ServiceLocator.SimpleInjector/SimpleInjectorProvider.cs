namespace KalikoCMS.ServiceLocation {
    using System;
    using AssemblyHelpers;
    using Core.Interfaces;
    using Data;
    using Data.Repositories;
    using Data.Repositories.Interfaces;
    using Interfaces;
    using Services.Content;
    using Services.Content.Interfaces;
    using KalikoCMS.Services.Resolvers;
    using KalikoCMS.Services.Resolvers.Interfaces;
    using Mappers;
    using Mappers.Interfaces;
    using Services.Initialization;
    using Services.Initialization.Interfaces;
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

            ServiceLocator.SetLocatorProvider(() => new SimpleInjectorServiceLocator(Container));
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

            ServiceLocator.SetLocatorProvider(() => new SimpleInjectorServiceLocator(Container));
        }
#endif

        public static void RegisterDataProvider<T>() where T : CmsContext {
            Container.Register<CmsContext, T>(Lifestyle.Scoped);
        }

        private static void RegisterCmsServices() {
            // Services
            Container.Register<IContentCreator, ContentCreator>(Lifestyle.Singleton);
            Container.Register<IContentIndexService, ContentIndexService>(Lifestyle.Singleton);
            Container.Register<IContentLoader, ContentLoader>(Lifestyle.Singleton);
            Container.Register<IContentTypeResolver, ContentTypeResolver>(Lifestyle.Singleton);
            Container.Register<IDomainResolver, DomainResolver>(Lifestyle.Singleton);
            Container.Register<IHttpContextResolver, HttpContextResolver>();
            Container.Register<IInitializationService, InitializationService>(Lifestyle.Singleton);
            Container.Register<ILanguageResolver, LanguageResolver>(Lifestyle.Singleton);
            Container.Register<IPropertyResolver, PropertyResolver>();
            Container.Register<IPropertyTypeResolver, PropertyTypeResolver>(Lifestyle.Singleton);
            Container.Register<IUrlResolver, UrlResolver>(Lifestyle.Singleton);

            // Data repositories
            Container.Register<IContentAccessRightsRepository, ContentAccessRightsRepository>();
            Container.Register<IContentLanguageRepository, ContentLanguageRepository>();
            Container.Register<IContentPropertyRepository, ContentPropertyRepository>();
            Container.Register<IContentRepository, ContentRepository>();
            Container.Register<IContentTagRepository, ContentTagRepository>();
            Container.Register<IContentTypeRepository, ContentTypeRepository>();
            Container.Register<IDomainRepository, DomainRepository>();
            Container.Register<ILanguageRepository, LanguageRepository>();
            Container.Register<IPropertyRepository, PropertyRepository>();
            Container.Register<IPropertyTypeRepository, PropertyTypeRepository>();
            Container.Register<IRedirectRepository, RedirectRepository>();
            Container.Register<ISystemInformationRepository, SystemInformationRepository>();
            Container.Register<ITagContextRepository, TagContextRepository>();
            Container.Register<ITagRepository, TagRepository>();

            // Mappers
            Container.Register<IContentMapper, ContentMapper>(Lifestyle.Singleton);
            Container.Register<IContentTypeMapper, ContentTypeMapper>(Lifestyle.Singleton);
            Container.Register<IDomainMapper, DomainMapper>(Lifestyle.Singleton);
            Container.Register<IPropertyMapper, PropertyMapper>(Lifestyle.Singleton);

#if NETCORE
            Container.Register<IActionContextAccessor, ActionContextAccessor>();
#endif

            RegisterUserServices();
        }

        private static void RegisterUserServices() {
            var types = AssemblyLocator.GetTypesWithInterface<ISimpleInjectorRegistrator>();

            foreach (var type in types) {
                var instance = Activator.CreateInstance(type) as ISimpleInjectorRegistrator;

                instance?.Register(Container);
            }
        }
    }
}