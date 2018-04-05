namespace KalikoCMS.Mvc.Framework {
#if NETCORE
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Routing;
#else
    using System.Web.Routing;
#endif

#if NETCORE
    public class CmsRoute : IRouter {
        private readonly IRouter _defaultRouter;
#else
    public class CmsRoute : Route {
#endif

        #region Constructors

#if NETCORE
        public CmsRoute(IRouter defaultRouter) {
            _defaultRouter = defaultRouter;
        }
#else
        public CmsRoute(string url, IRouteHandler routeHandler) : base(url, routeHandler) { }

        public CmsRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler) : base(url, defaults, routeHandler) { }

        public CmsRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler) : base(url, defaults, constraints, routeHandler) { }

        public CmsRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler) : base(url, defaults, constraints, dataTokens, routeHandler) { }
#endif

        #endregion

#if NETCORE
        public async Task RouteAsync(RouteContext context) {
            await _defaultRouter.RouteAsync(context);
        }

        public VirtualPathData GetVirtualPath(VirtualPathContext context) {
            object controller = null;

            if (context.HttpContext.Items.Keys.Contains("cmsRouting") && context.Values.ContainsKey("controller")) {
                controller = context.Values["controller"];
                context.Values.Remove("controller");
            }

            var virtualPath = _defaultRouter.GetVirtualPath(context);

            if (controller != null) {
                context.Values.Add("controller", controller);
            }

            return virtualPath;
        }
#else
        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values) {
            object controller = null;

            if (requestContext.HttpContext.Items.Contains("cmsRouting") && values.ContainsKey("controller")) {
                controller = values["controller"];
                values.Remove("controller");
            }

            var virtualPath = base.GetVirtualPath(requestContext, values);

            if (controller != null) {
                values.Add("controller", controller);
            }

            return virtualPath;
        }
#endif
    }
}