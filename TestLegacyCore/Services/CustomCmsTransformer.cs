namespace TestLegacyCore.Services {
    using System.Threading.Tasks;
    using KalikoCMS.Configuration.Interfaces;
    using KalikoCMS.Mvc.Framework;
    using KalikoCMS.Services.Content.Interfaces;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;

    public class CustomCmsTransformer : CmsTransformer {
        public CustomCmsTransformer(ICmsConfiguration configuration, IContentLoader contentLoader, IUrlResolver urlResolver) : base(configuration, contentLoader, urlResolver) { }

        public override ValueTask<RouteValueDictionary> TransformAsync(HttpContext httpContext, RouteValueDictionary values) {
            PrefixPath(values);

            return base.TransformAsync(httpContext, values);
        }

        private static void PrefixPath(RouteValueDictionary values) {
            if (values["path"] is string path) {
                path = $"articles/{path}";
                values["path"] = path;
            }
        }
    }
}