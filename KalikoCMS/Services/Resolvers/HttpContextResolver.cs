namespace KalikoCMS.Services.Resolvers {
    using Interfaces;
#if NETCORE
    using Microsoft.AspNetCore.Http;
#else
    using System.Web;
#endif

    public class HttpContextResolver : IHttpContextResolver {
#if NETCORE
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HttpContextResolver(IHttpContextAccessor httpContextAccessor) {
            _httpContextAccessor = httpContextAccessor;
        }
#endif

        public HttpContext Current {
            get {
#if NETCORE
                return _httpContextAccessor.HttpContext;
#else
                return HttpContext.Current;
#endif
            }
        }
    }
}
