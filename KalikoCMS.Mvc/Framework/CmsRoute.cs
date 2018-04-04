namespace KalikoCMS.Mvc.Framework {
#if NETCORE
    using System.Collections.Generic;
    using Microsoft.AspNetCore.Routing;

#else
    using System.Web.Routing;
#endif

    public class CmsRoute : Route {
        #region Constructors

#if NETCORE
        public CmsRoute(IRouter target, string routeTemplate, IInlineConstraintResolver inlineConstraintResolver) : base(target, routeTemplate, inlineConstraintResolver) { }

        public CmsRoute(IRouter target, string routeTemplate, RouteValueDictionary defaults, IDictionary<string, object> constraints, RouteValueDictionary dataTokens, IInlineConstraintResolver inlineConstraintResolver) : base(target, routeTemplate, defaults, constraints, dataTokens, inlineConstraintResolver) { }

        public CmsRoute(IRouter target, string routeName, string routeTemplate, RouteValueDictionary defaults, IDictionary<string, object> constraints, RouteValueDictionary dataTokens, IInlineConstraintResolver inlineConstraintResolver) : base(target, routeName, routeTemplate, defaults, constraints, dataTokens, inlineConstraintResolver) { }
#else
        public CmsRoute(string url, IRouteHandler routeHandler) : base(url, routeHandler) { }

        public CmsRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler) : base(url, defaults, routeHandler) { }

        public CmsRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler) : base(url, defaults, constraints, routeHandler) { }

        public CmsRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler) : base(url, defaults, constraints, dataTokens, routeHandler) { }
#endif

        #endregion

#if NETCORE
        public override VirtualPathData GetVirtualPath(VirtualPathContext context) {
            object controller = null;

            if (context.HttpContext.Items.Keys.Contains("cmsRouting") && context.Values.ContainsKey("controller")) {
                controller = context.Values["controller"];
                context.Values.Remove("controller");
            }

            var virtualPath = base.GetVirtualPath(context);

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