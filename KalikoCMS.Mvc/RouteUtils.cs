namespace KalikoCMS.Mvc {
    using System.Collections.Generic;
    using Core;
#if NETCORE
    using Microsoft.AspNetCore.Http;
#else
    using System.Web;
#endif

    public class RouteUtils
    {
        public static void RedirectToController(HttpContext httpContext, CmsPage page, string actionName = "index", Dictionary<string, object> additionalRouteData = null)
        {
            RequestModule.RedirectToController(httpContext, page, actionName, additionalRouteData);
        }
    }
}