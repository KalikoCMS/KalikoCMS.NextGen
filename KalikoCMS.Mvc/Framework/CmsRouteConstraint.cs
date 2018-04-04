namespace KalikoCMS.Mvc.Framework {
#if NETCORE
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
#else
    using System.Web;
    using System.Web.Routing;
#endif

    public class CmsRouteConstraint : IRouteConstraint {
#if NETCORE
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values, RouteDirection routeDirection) {
            // Prevent usage of route if not initialized from custom request handler
            return httpContext.Items.ContainsKey("cmsRouting");
        }
#else
        public bool Match(HttpContextBase httpContext, Route route, string parameterName, RouteValueDictionary values, RouteDirection routeDirection) {
            // Prevent usage of route if not initialized from custom request handler
            return httpContext.Items.Contains("cmsRouting");
        }
#endif
    }
}