namespace TestLegacyCore.Services
{
    using KalikoCMS.Configuration.Interfaces;
    using KalikoCMS.Data;
    using KalikoCMS.Data.InMemory;
    using KalikoCMS.Data.Repositories.Interfaces;
    using KalikoCMS.Legacy.Data;
    using KalikoCMS.Legacy.Data.Repositories;
    using KalikoCMS.ServiceLocation;
    using KalikoCMS.ServiceLocation.Interfaces;
    using Microsoft.Extensions.DependencyInjection;
    using TestSiteCore.Services;

    public class ServiceRegistrator : IDependencyInjectionRegistrator
    {
        public void Register(IServiceCollection services)
        {
            services.AddScoped<ILocalService, LocalServices>();

            //services.AddSingleton<IServiceScopeFactory, DependencyInjectionServiceLocator>();

            // Override with legacy repositories
            services.AddScoped<LegacyDataContext, LegacyDataContext>();
            services.AddSingleton<ICmsConfigurataion, CustomConfiguration>();
            services.AddScoped<IContentAccessRightsRepository, LegacyContentAccessRightsRepository>();
            services.AddScoped<IContentLanguageRepository, LegacyContentLanguageRepository>();
            services.AddScoped<IContentPropertyRepository, LegacyContentPropertyRepository>();
            services.AddScoped<IContentRepository, LegacyContentRepository>();
            services.AddScoped<IContentTagRepository, LegacyContentTagRepository>();
            services.AddScoped<IContentTypeRepository, LegacyContentTypeRepository>();
            services.AddScoped<IDomainRepository, LegacyDomainRepository>();
            services.AddScoped<ILanguageRepository, LegacyLanguageRepository>();
            services.AddScoped<IPropertyRepository, LegacyPropertyRepository>();
            services.AddScoped<IPropertyTypeRepository, LegacyPropertyTypeRepository>();
            services.AddScoped<IRedirectRepository, LegacyRedirectRepository>();
            services.AddScoped<ISystemInformationRepository, LegacySystemInformationRepository>();
            services.AddScoped<ITagContextRepository, LegacyTagContextRepository>();
            services.AddScoped<ITagRepository, LegacyTagRepository>();
            services.AddScoped<CmsContext, InMemoryCmsContext>();  // Using an empty in memory database
        }
    }
}