namespace KalikoCMS.Mvc {
#if NETCORE
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Extensions;
    using Framework;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Routing;
    using ServiceLocation;
    using Services;
    using Services.Content.Interfaces;
    using Services.Initialization.Interfaces;

    public class CmsMiddleware {
        private static bool _isInitialized;
        private static bool _isRunning;
        private static readonly object Padlock = new object();

        private readonly RequestDelegate _next;
        private CmsRoute _route;

        public CmsMiddleware(RequestDelegate next) {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext) {
            if (BreakIfAlreadyRunning(httpContext)) {
                return;
            }

            lock(Padlock) {
                if (!_isInitialized) {
                    _isRunning = true;

                    try {
                        // Temporary scan for page controllers
                        var requestModule = new RequestModule();
                        requestModule.Startup();

                        var initializationService = ServiceLocator.Current.GetInstance<IInitializationService>();
                        initializationService.Initialize();

                        _isInitialized = true;
                    }
                    catch (Exception exception) {
                        // TODO: Log
                    }

                    _isRunning = false;
                }
            }

            if (_route == null) {
                var actionInvokerFactory = ServiceLocator.Current.GetInstance<IActionInvokerFactory>();
                var actionSelector = ServiceLocator.Current.GetInstance<IActionSelector>();
                var actionContextAccessor = ServiceLocator.Current.GetInstance<IActionContextAccessor>();
                var urlResolver = ServiceLocator.Current.GetInstance<IUrlResolver>();

                _route = new CmsRoute(actionSelector, actionInvokerFactory, actionContextAccessor, urlResolver);
            }


            var context = new RouteContext(httpContext);
            await _route.RouteAsync(context);
            if (context.Handler == null) {
                await _next(httpContext);
            }
            else {
                context.RouteData.Routers.Insert(0, _route);
                httpContext.Features[typeof(IRoutingFeature)] = new RoutingFeature()
                {
                    RouteData = context.RouteData,
                };

                await context.Handler(context.HttpContext);
            }


            //if (httpContext.Request.Path == "/test/") {
            //    var httpResponse = httpContext.Response;
            //    httpResponse.StatusCode = 200;
            //    await httpResponse.WriteAsync("Triggered from middleware");
            //    return;
            //}

            //if (httpContext.Request.Path == "/pagecontroller/") {
            //    RouteUtils.RedirectToController(httpContext, new CmsPage());
            //    //return;
            //}

            //var init = new InitModule();
            //init.Init(null);

            //var request = new RequestModule();
            //request.Init(httpContext);

            //await _next(httpContext);
        }

        private static bool BreakIfAlreadyRunning(HttpContext httpContext) {
            if (!_isRunning) {
                return false;
            }

            httpContext.Response.RenderMessage("System is starting up..", "Please check back in a few seconds.", 503);
            return true;
        }
    }
#endif
}