namespace TestSiteCore.Controllers {
    using KalikoCMS.Services.Content.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    public class RouteController : Controller {
        private readonly IUrlResolver _urlResolver;

        public RouteController(IUrlResolver urlResolver) {
            _urlResolver = urlResolver;
        }

        public ActionResult Index() {
            var content = _urlResolver.GetContent("/test/another-page/");
            if (content == null) {
                return Content("Not found");
            }

            return Content(content.ContentName);

            //return Content("I was routed! My url " + Url.Action("OtherAction"));
        }

        public ActionResult OtherAction() {
            return Content("Other action");
        }
    }
}
 