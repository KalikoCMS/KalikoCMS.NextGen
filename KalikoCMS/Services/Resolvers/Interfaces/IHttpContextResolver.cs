namespace KalikoCMS.Services.Resolvers.Interfaces {
#if NETCORE
    using Microsoft.AspNetCore.Http;
#else
    using System.Web;
#endif

    public interface IHttpContextResolver {
        HttpContext Current { get; }
    }
}