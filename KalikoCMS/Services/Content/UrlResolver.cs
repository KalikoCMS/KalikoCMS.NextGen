namespace KalikoCMS.Services.Content {
    using System;
    using System.Linq;
    using Core;
    using Interfaces;

    public class UrlResolver : IUrlResolver {
        private readonly IContentIndexService _contentIndexService;
        private readonly IDomainResolver _domainResolver;

        public UrlResolver(IContentIndexService contentIndexService, IDomainResolver domainResolver) {
            _contentIndexService = contentIndexService;
            _domainResolver = domainResolver;
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

            if (path == "/") {
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