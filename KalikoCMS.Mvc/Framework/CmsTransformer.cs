#if NETCORE
using KalikoCMS.Configuration.Interfaces;
using KalikoCMS.Core;
using KalikoCMS.Extensions;
using KalikoCMS.Services.Content.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Routing;
using System.Threading.Tasks;

namespace KalikoCMS.Mvc.Framework
{
    public class CmsTransformer : DynamicRouteValueTransformer
    {
        private readonly IUrlResolver _urlResolver;
        private readonly IContentLoader _contentLoader;
        private readonly ICmsConfiguration _configuration;

        public CmsTransformer(ICmsConfiguration configuration, IContentLoader contentLoader, IUrlResolver urlResolver)
        {
            _configuration = configuration;
            _contentLoader = contentLoader;
            _urlResolver = urlResolver;
        }

        public override async ValueTask<RouteValueDictionary> TransformAsync(HttpContext httpContext, RouteValueDictionary values)
        {
            var path = values["path"] as string;

            if(string.IsNullOrEmpty(path)) // TODO: Add start page routing
            {
                return null;
            }

            if (!Bootstrapper.Initialize())
            {
                return new RouteValueDictionary()
                {
                    { "controller", "CmsMessage" },
                    { "action", "Startup" }
                };
            }

            var content = _urlResolver.GetContent(path, true);

            if (content == null || !content.IsAvailable())
            {
                return null;
            }

            var site = _contentLoader.GetClosest<CmsSite>(content.ContentReference);

            httpContext.Items["cmsCurrentPage"] = content;
            httpContext.Items["cmsCurrentSite"] = site;
            httpContext.Items["cmsLanguageId"] = content.LanguageId;

            var action = "Index";
            var contentUrlLength = content.ContentUrl.Length + (_configuration.SkipEndingSlash ? 1 : 0);
            if (path.Length > contentUrlLength)
            {
                var remains = path.Substring(contentUrlLength).Trim('/');
                if (remains.IndexOf('/') > 0)
                {
                    // Not an action
                    return null;
                }
                else
                {
                    action = remains;
                }
            }

            var controllerType = RequestModule.GetControllerType(content);
            var controllerTypeName = controllerType.Name;
            if (controllerTypeName.EndsWith("Controller"))
            {
                controllerTypeName = controllerTypeName.Substring(0, controllerTypeName.Length - 10);
            }

            return new RouteValueDictionary()
            {
                { "controller", controllerTypeName },
                { "action", action },
                { "currentPage", content },
                { "currentSite", site }
            };
        }
    }
}
#endif