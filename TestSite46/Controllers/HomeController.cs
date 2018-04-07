using System.Web.Mvc;
using KalikoCMS.Core;
using TestSite46.Models;

namespace TestSite46.Controllers
{
    public class HomeController : Controller
    {
        public HomeController()
        {
        }

        public ActionResult Index()
        {
            var pageProxy = (MyPage)PageProxy.CreatePageProxy(typeof(MyPage));
            var test = pageProxy.Test;
            if (test != null)
            {
                return Content(test);
            }

            return View(new ContentReference());
        }
    }
}
