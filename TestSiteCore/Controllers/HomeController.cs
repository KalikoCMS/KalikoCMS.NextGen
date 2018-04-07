using KalikoCMS.Core;
using Microsoft.AspNetCore.Mvc;

namespace TestSiteCore.Controllers {
    public class HomeController : Controller {

        public HomeController() {
        }

        public IActionResult Index() {
            return View(new ContentReference());
        }
    }
}
