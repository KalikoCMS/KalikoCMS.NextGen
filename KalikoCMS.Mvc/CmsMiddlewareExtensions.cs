namespace KalikoCMS.Mvc {
#if NETCORE
    using Microsoft.AspNetCore.Builder;

    public static class CmsMiddlewareExtensions {
        public static IApplicationBuilder UseCmsMiddleware(this IApplicationBuilder builder) {
            return builder.UseMiddleware<CmsMiddleware>();
        }
    }
#endif
}