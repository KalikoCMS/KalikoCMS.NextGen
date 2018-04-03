#if NETFULL
using System.Reflection;
using System.Web;
using System.Web.Hosting;

namespace KalikoCMS.UI {
    public class EmbeddedVirtualFile : VirtualFile {
        private readonly string _virtualPath;
        private readonly Assembly _assembly;

        public EmbeddedVirtualFile(string virtualPath) : base(virtualPath) {
            _assembly = this.GetType().Assembly;
            _virtualPath = VirtualPathUtility.ToAppRelative(virtualPath);
        }

        public override System.IO.Stream Open() {
            var resourceName = GetType().Namespace + "." + _virtualPath.Replace("~/", "").Replace("/", ".");
            return _assembly.GetManifestResourceStream(resourceName);

        }
    }
}
#endif