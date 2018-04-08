using Microsoft.AspNetCore.Mvc;

namespace TestSiteCore.Controllers {
    using Services;

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
