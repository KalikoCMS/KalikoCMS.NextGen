namespace KalikoCMS.ServiceLocation {
    using Configuration;
    using Configuration.Interfaces;
    using Core.Interfaces;
    using Data.Repositories;
    using Data.Repositories.Interfaces;
    using Mappers;
    using Mappers.Interfaces;
    using Services.Content;
    using Services.Content.Interfaces;
    using Services.Initialization;
    using Services.Initialization.Interfaces;
    using Services.Resolvers;
    using Services.Resolvers.Interfaces;
#if NETCORE
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using KalikoCMS.Mvc.Framework;
#endif

    public abstract class ServiceLocatorProviderBase {
        
        public abstract void RegisterSingleton<TService, TImplementation>() where TService : class where TImplementation : class, TService;
        public abstract void RegisterScoped<TService, TImplementation>() where TService : class where TImplementation : class, TService;
        public abstract void RegisterTransient<TService, TImplementation>() where TService : class where TImplementation : class, TService;
        public abstract void RegisterUserServices();

        protected void RegisterCmsServices() {
            // Services
            RegisterSingleton<ICmsConfiguration, CmsConfiguration>();
            RegisterSingleton<IContentCreator, ContentCreator>();
            RegisterSingleton<IContentIndexService, ContentIndexService>();
            RegisterSingleton<IContentLoader, ContentLoader>();
            RegisterSingleton<IContentTypeResolver, ContentTypeResolver>();
            RegisterSingleton<IDomainResolver, DomainResolver>();
            RegisterScoped<IHttpContextResolver, HttpContextResolver>();
            RegisterScoped<IInitializationService, InitializationService>();
            RegisterSingleton<ILanguageResolver, LanguageResolver>();
            RegisterScoped<IPropertyResolver, PropertyResolver>();
            RegisterSingleton<IPropertyTypeResolver, PropertyTypeResolver>();
            RegisterScoped<IUrlResolver, UrlResolver>();

            // Data repositories
            RegisterScoped<IContentAccessRightsRepository, ContentAccessRightsRepository>();
            RegisterScoped<IContentLanguageRepository, ContentLanguageRepository>();
            RegisterScoped<IContentPropertyRepository, ContentPropertyRepository>();
            RegisterScoped<IContentRepository, ContentRepository>();
            RegisterScoped<IContentTagRepository, ContentTagRepository>();
            RegisterScoped<IContentTypeRepository, ContentTypeRepository>();
            RegisterScoped<IDomainRepository, DomainRepository>();
            RegisterScoped<ILanguageRepository, LanguageRepository>();
            RegisterScoped<IPropertyRepository, PropertyRepository>();
            RegisterScoped<IPropertyTypeRepository, PropertyTypeRepository>();
            RegisterScoped<IRedirectRepository, RedirectRepository>();
            RegisterScoped<ISystemInformationRepository, SystemInformationRepository>();
            RegisterScoped<ITagContextRepository, TagContextRepository>();
            RegisterScoped<ITagRepository, TagRepository>();

            // Mappers
            RegisterSingleton<IContentMapper, ContentMapper>();
            RegisterSingleton<IContentTypeMapper, ContentTypeMapper>();
            RegisterSingleton<IDomainMapper, DomainMapper>();
            RegisterSingleton<IPropertyMapper, PropertyMapper>();

#if NETCORE
            RegisterTransient<IActionContextAccessor, ActionContextAccessor>();
#endif

            RegisterUserServices();
        }
    }
}
