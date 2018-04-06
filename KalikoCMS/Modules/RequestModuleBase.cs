using System;

namespace KalikoCMS.Modules {
#if NETCORE
#else
    using System.Web;
#endif

#if NETCORE
    public abstract class RequestModuleBase {
#else
    public abstract class RequestModuleBase : IHttpModule {
#endif

        private static readonly string PreviewPath;
        //protected static IRequestManager RequestManager { get; set; }

        static RequestModuleBase() {
            //PreviewPath = SiteSettings.Instance.PreviewPath;
        }

#if NETFULL
        public void Init(HttpApplication context) {
            context.PostAuthorizeRequest += PostAuthorizeRequest;
            context.PreRequestHandlerExecute += PreRequestHandlerExecute;
        }

        public static string PageHasExpired() {
            //Utils.RenderSimplePage(HttpContext.Current.Response, "Page is not available", "The requested page has expired or is not yet published.", 404);
            return string.Empty;
        }

        private void PreRequestHandlerExecute(object sender, EventArgs e) {
            var session = HttpContext.Current.Session;

            if (session != null && session.IsNewSession) {
                //Language.CurrentLanguage = Language.ReadLanguageFromHostAddress();
                session["__session_is_set"] = "true";
            }
        }

        private void PostAuthorizeRequest(object sender, EventArgs e) {
            HandleRequest();
        }

        private void HandleRequest() {
            var relativeUrl = RelativeUrl;

            if (IsUrlToPreview(FullUrl)) {
                PreviewPage();
            }
            else if (IsUrlToStartPage(relativeUrl)) {
                RedirectToStartPage();
            }
            else {
                //PageFactory.FindPage(relativeUrl, RequestManager);
            }
        }

        private static void PreviewPage() {
            var pageId = Guid.Parse(HttpContext.Current.Request.QueryString["id"]);
            var version = int.Parse(HttpContext.Current.Request.QueryString["version"]);
            //var page = PageFactory.GetSpecificVersion(pageId, version);

            //RequestManager.PreviewPage(page);
        }

        private static string RelativeUrl {
            get {
                var url = HttpContext.Current.Request.Path.ToLowerInvariant();
                //int rootPathLength = Utils.ApplicationPath.Length;
                //url = url.Length > rootPathLength ? url.Substring(rootPathLength) : string.Empty;
                return url;
            }
        }

        private static string FullUrl {
            get { return HttpContext.Current.Request.Path.ToLowerInvariant(); }
        }

        private bool IsUrlToPreview(string url) {
            if (!url.StartsWith("/")) {
                url = string.Format("/{0}", url);
            }
            return url.ToLowerInvariant().StartsWith(PreviewPath);
        }

        private static bool IsUrlToStartPage(string url) {
            return (url.Length == 0 || url == "default.aspx");
        }

        public void Dispose() { }

        protected abstract void RedirectToStartPage();
#endif
    }
}