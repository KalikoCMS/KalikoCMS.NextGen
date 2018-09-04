namespace TestLegacyCore.Controllers {
    using Microsoft.AspNetCore.Mvc;
    using TestSiteCore.Services;

    public class HomeController : Controller {
        private readonly ILocalService _localService;

        public HomeController(ILocalService localService) {
            _localService = localService;
        }

        public ActionResult Index() {
            return Content(_localService.TestMethod());
        }

        [Route("routed")]
        public ActionResult Test()
        {
            return Content("Routed" + Url.Action("Test") + "|" + Url.Action("Index"));
        }
    }
}