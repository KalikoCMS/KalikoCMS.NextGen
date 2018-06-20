namespace TestSiteCore.Controllers {
    using KalikoCMS.Mvc.Framework;
    using Microsoft.AspNetCore.Mvc;
    using TestLegacyCore.Models;

    public class ArticlePageController : PageController<ArticlePage> {
        public override ActionResult Index(ArticlePage currentPage) {
            return View("~/Views/Pages/ArticlePage/Index.cshtml", currentPage);
        }
    }
}