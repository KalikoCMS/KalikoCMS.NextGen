namespace KalikoCMS.Services.Content {
    using System;
    using System.Linq;
    using Configuration.Interfaces;
    using Core;
    using Interfaces;

    public class UrlResolver : IUrlResolver {
        private readonly IContentIndexService _contentIndexService;
        private readonly IDomainResolver _domainResolver;
        private readonly ICmsConfiguration _configuration;

        public UrlResolver(IContentIndexService contentIndexService, IDomainResolver domainResolver, ICmsConfiguration configuration) {
            _contentIndexService = contentIndexService;
            _domainResolver = domainResolver;
            _configuration = configuration;
        }

        public Content GetContent(string path) {
            return GetContent(path, false);
        }

        public Content GetContent(string path, bool returnPartialMatches) {
            var domain = _domainResolver.GetCurrentDomain();
            if (domain == null) {
                // TODO: Allow wildcard?
                return null;
            }

            // TODO: Add persinstance and support multiple sites
            var site = _contentIndexService.GetNode(domain.ContentId);
            if (site == null) {
                return null;
            }

            // TODO: Actually get the start page from config, not the first child of root
            if (path == "/") {
                if (_configuration.IgnoreStartPage) {
                    return null;
                }

                var startContent = site.Children.FirstOrDefault();
                if (startContent == null) {
                    return null;
                }

                return _contentIndexService.GetContentFromNode(startContent);
            }
            
            // TODO: Add domain and language resolver

            var segments = path.Trim('/').Split('/');
            var node = site;
            foreach (var segment in segments) {
                var parent = node;
                node = node.Children.FirstOrDefault(x => x.Languages.Any(l => l.UrlSegment == segment));
                if (node != null) {
                    continue;
                }

                if (!returnPartialMatches) {
                    return null;
                }

                if (parent == site) {
                    return null;
                }

                return _contentIndexService.GetContentFromNode(parent);
            }

            // TODO: Access rights check
            return _contentIndexService.GetContentFromNode(node);
        }
    }
}