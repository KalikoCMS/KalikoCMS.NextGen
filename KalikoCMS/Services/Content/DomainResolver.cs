namespace KalikoCMS.Services.Content {
    using System.Collections.Generic;
    using Core;
    using Data.Repositories.Interfaces;
    using Interfaces;
    using Mappers.Interfaces;
    using Resolvers;
    using Resolvers.Interfaces;
    using ServiceLocation;

    public class DomainResolver : IDomainResolver {
        private readonly IDomainMapper _domainMapper;
        private static Dictionary<string, DomainInformation> _domains;

        public DomainResolver(IDomainMapper domainMapper) {
            _domainMapper = domainMapper;
        }

        public void AddDomain(DomainInformation domain) {
            // TODO: Add validation

            var domainRepository = ServiceLocator.Current.GetInstance<IDomainRepository>();
            var domainEntity = _domainMapper.Map(domain);
            domainRepository.Create(domainEntity);
            domain.DomainId = domainEntity.DomainId;

            _domains.Add(domain.DomainName, domain);
        }

        public void Initialize() {
            if (_domains != null) {
                return;
            }

            _domains = new Dictionary<string, DomainInformation>();

            var domainRepository = ServiceLocator.Current.GetInstance<IDomainRepository>();
            var domains = domainRepository.GetAll();
            foreach (var domainEntity in domains) {
                var domain = _domainMapper.Map(domainEntity);
                _domains.Add(domain.DomainName, domain);
            }
        }

        public DomainInformation GetCurrentDomain() {
            var httpContext = ServiceLocator.Current.GetInstance<IHttpContextResolver>();
            if (httpContext.Current.Items["cmsDomain"] is DomainInformation domain) {
                return domain;
            }

#if NETCORE
            var host = httpContext.Current.Request.Host.Host;
#else
            var host = httpContext.Current.Request.Url.Host;
#endif

            if (_domains.TryGetValue(host, out domain)) {
                httpContext.Current.Items["cmsDomain"] = domain;
                return domain;
            }

            if (_domains.TryGetValue("*", out domain)) {
                httpContext.Current.Items["cmsDomain"] = domain;
                return domain;
            }

            return null;
        }
    }
}
