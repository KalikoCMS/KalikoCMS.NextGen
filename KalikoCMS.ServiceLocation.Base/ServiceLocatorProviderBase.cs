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
#endif

    public abstract class ServiceLocatorProviderBase {
        
        public abstract void RegisterSingelton<TService, TImplementation>() where TService : class where TImplementation : class, TService;
        public abstract void RegisterScoped<TService, TImplementation>() where TService : class where TImplementation : class, TService;
        public abstract void RegisterTransient<TService, TImplementation>() where TService : class where TImplementation : class, TService;
        public abstract void RegisterUserServices();

        protected void RegisterCmsServices() {
            // Services
            RegisterSingelton<ICmsConfigurataion, CmsConfiguration>();
            RegisterSingelton<IContentCreator, ContentCreator>();
            RegisterSingelton<IContentIndexService, ContentIndexService>();
            RegisterSingelton<IContentLoader, ContentLoader>();
            RegisterSingelton<IContentTypeResolver, ContentTypeResolver>();
            RegisterSingelton<IDomainResolver, DomainResolver>();
            RegisterScoped<IHttpContextResolver, HttpContextResolver>();
            RegisterSingelton<IInitializationService, InitializationService>();
            RegisterSingelton<ILanguageResolver, LanguageResolver>();
            RegisterScoped<IPropertyResolver, PropertyResolver>();
            RegisterSingelton<IPropertyTypeResolver, PropertyTypeResolver>();
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
            RegisterSingelton<IContentMapper, ContentMapper>();
            RegisterSingelton<IContentTypeMapper, ContentTypeMapper>();
            RegisterSingelton<IDomainMapper, DomainMapper>();
            RegisterSingelton<IPropertyMapper, PropertyMapper>();

#if NETCORE
            RegisterTransient<IActionContextAccessor, ActionContextAccessor>();
#endif

            RegisterUserServices();
        }
    }
}
