namespace TestSiteCore.Controllers {
    using Microsoft.AspNetCore.Mvc;

    public class RouteController : Controller {
        public ActionResult Index() {
            return Content("I was routed! My url " + Url.Action("OtherAction"));
        }

        public ActionResult OtherAction() {
            return Content("Other action");
        }
    }
}
 