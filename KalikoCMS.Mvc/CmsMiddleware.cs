namespace KalikoCMS.Mvc {
#if NETCORE
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Http;

    public class CmsMiddleware {
        private readonly RequestDelegate _next;

        public CmsMiddleware(RequestDelegate next) {
            _next = next;
        }

        public async Task Invoke(HttpContext httpContext) {
            if (httpContext.Request.Path == "/test/") {
                var httpResponse = httpContext.Response;
                httpResponse.StatusCode = 200;
                await httpResponse.WriteAsync("Triggered from middleware");
                return;
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