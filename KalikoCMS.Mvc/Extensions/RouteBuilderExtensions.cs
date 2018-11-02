namespace KalikoCMS.Mvc.Extensions {
#if NETCORE
    using Framework;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Routing;
    using ServiceLocation;
    using Services.Content.Interfaces;

    public static class RouteBuilderExtensions {
        public static void MapCms(this IRouteBuilder routeBuilder, bool insertAsFirst = true) {
            var actionInvokerFactory = routeBuilder.ServiceProvider.GetService(typeof(IActionInvokerFactory)) as IActionInvokerFactory;
            var actionSelector = routeBuilder.ServiceProvider.GetService(typeof(IActionSelector)) as IActionSelector;
            var actionContextAccessor = routeBuilder.ServiceProvider.GetService(typeof(IActionContextAccessor)) as IActionContextAccessor;
            var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
            var urlResolver = ServiceLocator.Current.GetInstance<IUrlResolver>();

            if (insertAsFirst) {
                routeBuilder.Routes.Insert(0, new CmsRoute(actionSelector, actionInvokerFactory, actionContextAccessor, urlResolver, contentLoader));
            }
            else {
                routeBuilder.Routes.Add(new CmsRoute(actionSelector, actionInvokerFactory, actionContextAccessor, urlResolver, contentLoader));
            }
        }
    }
#endif
}