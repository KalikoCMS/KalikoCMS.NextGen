namespace TestLegacyCore.Controllers {
    using Microsoft.AspNetCore.Mvc;
    using TestSiteCore.Services;

    public class HomeController : Controller {
        private readonly ILocalService _localService;

        public HomeController(ILocalService localService) {
            _localService = localService;
        }

        public IActionResult Index() {
            return Content(_localService.TestMethod());
        }
    }
}