using KalikoCMS;
using KalikoCMS.AssemblyReaders;
using KalikoCMS.Core;
using KalikoCMS.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using TestSite46.Models;

namespace TestSiteCore.Controllers {
    public class HomeController : Controller {
        private readonly IDemoService _demoService;

        public HomeController(IDemoService demoService) {
            _demoService = demoService;
        }

        public IActionResult Index() {
            //var pageProxy = (MyPage) PageProxy.CreatePageProxy(typeof(MyPage));
            //var test = pageProxy.Test;
            //if (test != null) {
            //    return Content(test);
            //}

            //return Content(_demoService.HelloWorld());

            //var types = InterfaceReader.GetTypesWithInterface(typeof(IDemoService));

            //var s = "";
            //foreach (var type in types) {
            //    s += type.AssemblyQualifiedName;
            //}
            //return Content(s);

            return View(new ContentReference());

            var tester = new Tester();
            return Ok("Hello World from a controller " + tester.WhoAmI);
        }
    }
}
