using System.Web.Mvc;
using KalikoCMS;
using KalikoCMS.Attributes;
using KalikoCMS.Core;
using KalikoCMS.AssemblyReaders;
using KalikoCMS.Services.Interfaces;
using TestSite46.Models;

namespace TestSite46.Controllers
{
    public class HomeController : Controller
    {
        private readonly IDemoService _demoService;

        public HomeController(IDemoService demoService)
        {
            _demoService = demoService;
        }

        public ActionResult Index()
        {
            var pageProxy = (MyPage)PageProxy.CreatePageProxy(typeof(MyPage));
            var test = pageProxy.Test;
            if (test != null)
            {
                return Content(test);
            }

            var tester2 = new Tester().WhoAmI;
            return View(new ContentReference());

            //            return Content(_demoService.HelloWorld());
            //var types = AttributeReader.GetTypesWithAttribute(typeof(SiteSettingsAttribute));
            var types = InterfaceReader.GetTypesWithInterface(typeof(IDemoService));

            var s = "";
            foreach (var type in types)
            {
                s += type.AssemblyQualifiedName;
            }
            return Content(s);

            return View(new ContentReference());

            var tester = new Tester();
            return Content(tester.WhoAmI);
        }
    }
}
