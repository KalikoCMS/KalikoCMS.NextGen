namespace TestLegacyCore.Controllers {
    using KalikoCMS.Mvc.Framework;
    using Microsoft.AspNetCore.Mvc;
    using Models.PageTypes;

    public class ArticlePageController : PageController<ArticlePage> {
        public override ActionResult Index(ArticlePage currentPage) {
            return View("~/Views/Pages/ArticlePage/Index.cshtml", currentPage);
        }

        public ActionResult Test(ArticlePage currentPage) {
            return Content("Action Test on " + currentPage.ContentName);
        }
    }
}