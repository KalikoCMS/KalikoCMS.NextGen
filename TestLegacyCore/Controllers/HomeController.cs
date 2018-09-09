namespace TestLegacyCore.Controllers {
    using System.Linq;
    using KalikoCMS.Core;
    using KalikoCMS.Legacy;
    using KalikoCMS.ServiceLocation;
    using KalikoCMS.Services.Content.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Models.PageTypes;
    using TestSiteCore.Services;

    public class HomeController : Controller {
        private readonly ILocalService _localService;

        public HomeController(ILocalService localService) {
            _localService = localService;
        }

        public ActionResult Index() {
            return Content(_localService.TestMethod());
        }

        public ActionResult Latest() {
            var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
            var articlePages = contentLoader.GetDescendants<ArticlePage>(new ContentReference(LegacySettings.SiteId, LegacySettings.LanguageId));

            return Content(string.Join(" | ", articlePages.Select(x => x.ContentName)));
        }

        [Route("routed")]
        public ActionResult Test()
        {
            return Content("Routed" + Url.Action("Test") + "|" + Url.Action("Index"));
        }
    }
}