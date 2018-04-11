namespace KalikoCMS.Mvc {
#if NETCORE
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Core;
    using Extensions;
    using Microsoft.AspNetCore.Http;
    using ServiceLocation;
    using Services;
    using Services.Initialization.Interfaces;

    public class CmsMiddleware {
        private static bool _isInitialized;
        private static bool _isRunning;
        private static readonly object Padlock = new object();

        private readonly RequestDelegate _next;

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

            if (httpContext.Request.Path == "/test/") {
                var httpResponse = httpContext.Response;
                httpResponse.StatusCode = 200;
                await httpResponse.WriteAsync("Triggered from middleware");
                return;
            }

            if (httpContext.Request.Path == "/pagecontroller/") {
                RouteUtils.RedirectToController(httpContext, new CmsPage());
                //return;
            }

            //var init = new InitModule();
            //init.Init(null);

            //var request = new RequestModule();
            //request.Init(httpContext);

            await _next(httpContext);
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