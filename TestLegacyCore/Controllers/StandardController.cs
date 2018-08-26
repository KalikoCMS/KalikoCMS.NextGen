namespace TestLegacyCore.Controllers {
    using Microsoft.AspNetCore.Mvc;

    public class StandardController : Controller {
        public ActionResult Index() {
            return View();
        }

        public ActionResult Test()
        {
            return Content("Test");
        }

    }
}