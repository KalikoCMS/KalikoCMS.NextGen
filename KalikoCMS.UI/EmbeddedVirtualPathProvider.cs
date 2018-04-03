#if NETFULL
using System;
using System.Collections;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Caching;
using System.Web.Hosting;

namespace KalikoCMS.UI {
    public class EmbeddedVirtualPathProvider : VirtualPathProvider {
        private readonly Assembly _assembly = typeof(EmbeddedVirtualPathProvider).Assembly;
        private readonly string[] _resourceNames;

        public EmbeddedVirtualPathProvider() {
            _resourceNames = _assembly.GetManifestResourceNames();
        }

        private bool IsEmbeddedResourcePath(string virtualPath) {
            var checkPath = VirtualPathUtility.ToAppRelative(virtualPath);
            var resourceName = this.GetType().Namespace + "." + checkPath.Replace("~/", "").Replace("/", ".");
            return this._resourceNames.Contains(resourceName);
        }

        public override bool FileExists(string virtualPath) {
            return IsEmbeddedResourcePath(virtualPath) || base.FileExists(virtualPath);
        }

        public override VirtualFile GetFile(string virtualPath) {
            if (IsEmbeddedResourcePath(virtualPath)) {
                return new EmbeddedVirtualFile(virtualPath);
            }
            return base.GetFile(virtualPath);
        }

        public override CacheDependency GetCacheDependency(string virtualPath, IEnumerable virtualPathDependencies, DateTime utcStart) {
            if (IsEmbeddedResourcePath(virtualPath)) {
                return null;
            }
            return base.GetCacheDependency(virtualPath, virtualPathDependencies, utcStart);
        }
    }
}
#endif