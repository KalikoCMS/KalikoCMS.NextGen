namespace TestLegacyCore.Controllers {
    using KalikoCMS.Mvc.Framework;
    using Microsoft.AspNetCore.Mvc;
    using Models.PageTypes;

    public class ArticleLandingPageController : PageController<ArticleLandingPage> {
        public override ActionResult Index(ArticleLandingPage currentPage) {
            return View("~/Views/Pages/ArticleLandingPage/Index.cshtml", currentPage);
        }
    }
}