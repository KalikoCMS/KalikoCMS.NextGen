namespace KalikoCMS.Mvc {
#if NETCORE
    using System;
    using System.Threading.Tasks;
    using Extensions;
    using Framework;
    using Microsoft.AspNetCore.Http;
    using ServiceLocation;
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
                        //var requestModule = new RequestModule();
                        //requestModule.Startup();

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