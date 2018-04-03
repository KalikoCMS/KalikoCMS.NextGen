#if NETCORE
using System.Reflection;
using Microsoft.Extensions.FileProviders;

namespace KalikoCMS.UI {
    public class CmsEmbeddedFileProvider : EmbeddedFileProvider {
        public CmsEmbeddedFileProvider() : base(Assembly.GetExecutingAssembly()) {
        }

        public CmsEmbeddedFileProvider(string baseNamespace) : base(Assembly.GetExecutingAssembly(), baseNamespace) {
        }
    }
}
#endif