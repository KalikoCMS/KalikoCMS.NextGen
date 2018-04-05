namespace KalikoCMS.Mvc.Extensions {
#if NETCORE
    using Framework;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Routing;

    public static class RouteBuilderExtensions {
        public static void MapCms(this IRouteBuilder routeBuilder) {
            var actionInvokerFactory = routeBuilder.ServiceProvider.GetService(typeof(IActionInvokerFactory)) as IActionInvokerFactory;
            var actionSelector = routeBuilder.ServiceProvider.GetService(typeof(IActionSelector)) as IActionSelector;
            var actionContextAccessor = routeBuilder.ServiceProvider.GetService(typeof(IActionContextAccessor)) as IActionContextAccessor;

            routeBuilder.Routes.Add(new CmsRoute(actionSelector, actionInvokerFactory, actionContextAccessor));
        }
    }
#endif
}