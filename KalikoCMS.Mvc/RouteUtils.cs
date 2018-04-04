namespace KalikoCMS.Mvc {
    using System.Collections.Generic;
    using Core;

    public class RouteUtils
    {
        public static void RedirectToController(CmsPage page, string actionName = "index", Dictionary<string, object> additionalRouteData = null)
        {
            //RequestModule.RedirectToController(page, actionName, additionalRouteData);
        }
    }
}