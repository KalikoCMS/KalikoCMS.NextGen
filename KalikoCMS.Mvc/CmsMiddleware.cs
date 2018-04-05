namespace KalikoCMS.Mvc {
#if NETCORE
    using System.Threading.Tasks;
    using Core;
    using Microsoft.AspNetCore.Http;

    public class CmsMiddleware {
        private readonly RequestDelegate _next;
        private bool _isInitialized;

        public CmsMiddleware(RequestDelegate next) {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext) {
            if (!_isInitialized) {
                // Temporary scan for page controllers
                var requestModule = new RequestModule();
                requestModule.Startup();
                _isInitialized = true;
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
    }
#endif
}