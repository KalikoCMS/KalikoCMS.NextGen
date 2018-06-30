namespace TestLegacyCore.Services {
    using KalikoCMS.Data.Repositories.Interfaces;
    using KalikoCMS.Legacy.Data;
    using KalikoCMS.Legacy.Data.Repositories;
    using KalikoCMS.ServiceLocation.Interfaces;
    using SimpleInjector;
    using TestSiteCore.Services;

    public class ServiceRegistrator : ISimpleInjectorRegistrator {
        public void Register(Container container) {
            container.Register<ILocalService, LocalServices>();

            // Override with legacy repositories
            container.Register<LegacyDataContext, LegacyDataContext>(Lifestyle.Scoped);
            container.Register<IContentAccessRightsRepository, LegacyContentAccessRightsRepository>(Lifestyle.Scoped);
            container.Register<IContentLanguageRepository, LegacyContentLanguageRepository>(Lifestyle.Scoped);
            container.Register<IContentPropertyRepository, LegacyContentPropertyRepository>(Lifestyle.Scoped);
            container.Register<IContentRepository, LegacyContentRepository>(Lifestyle.Scoped);
            container.Register<IContentTagRepository, LegacyContentTagRepository>(Lifestyle.Scoped);
            container.Register<IContentTypeRepository, LegacyContentTypeRepository>(Lifestyle.Scoped);
            container.Register<IDomainRepository, LegacyDomainRepository>(Lifestyle.Scoped);
            container.Register<ILanguageRepository, LegacyLanguageRepository>(Lifestyle.Scoped);
            container.Register<IPropertyRepository, LegacyPropertyRepository>(Lifestyle.Scoped);
            container.Register<IPropertyTypeRepository, LegacyPropertyTypeRepository>(Lifestyle.Scoped);
            container.Register<IRedirectRepository, LegacyRedirectRepository>(Lifestyle.Scoped);
            container.Register<ISystemInformationRepository, LegacySystemInformationRepository>(Lifestyle.Scoped);
            container.Register<ITagContextRepository, LegacyTagContextRepository>(Lifestyle.Scoped);
            container.Register<ITagRepository, LegacyTagRepository>(Lifestyle.Scoped);
        }
    }
}