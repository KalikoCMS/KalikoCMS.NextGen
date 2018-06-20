namespace TestSiteCore.Services {
    using KalikoCMS.Data.Repositories.Interfaces;
    using KalikoCMS.Legacy.Data;
    using KalikoCMS.Legacy.Data.Repositories;
    using KalikoCMS.ServiceLocation.Interfaces;
    using SimpleInjector;

    public class ServiceRegistrator : ISimpleInjectorRegistrator {
        public void Register(Container container) {
            container.Register<ILocalService, LocalServices>();

            // Override with legacy repositories
            container.Register<LegacyDataContext, LegacyDataContext>(Lifestyle.Scoped);
            container.Register<IContentAccessRightsRepository, LegacyContentAccessRightsRepository>();
            container.Register<IContentLanguageRepository, LegacyContentLanguageRepository>();
            container.Register<IContentPropertyRepository, LegacyContentPropertyRepository>();
            container.Register<IContentRepository, LegacyContentRepository>();
            container.Register<IContentTagRepository, LegacyContentTagRepository>();
            container.Register<IContentTypeRepository, LegacyContentTypeRepository>();
            container.Register<IDomainRepository, LegacyDomainRepository>();
            container.Register<ILanguageRepository, LegacyLanguageRepository>();
            container.Register<IPropertyRepository, LegacyPropertyRepository>();
            container.Register<IPropertyTypeRepository, LegacyPropertyTypeRepository>();
            container.Register<IRedirectRepository, LegacyRedirectRepository>();
            container.Register<ISystemInformationRepository, LegacySystemInformationRepository>();
            container.Register<ITagContextRepository, LegacyTagContextRepository>();
            container.Register<ITagRepository, LegacyTagRepository>();
        }
    }
}