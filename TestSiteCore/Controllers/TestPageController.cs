using KalikoCMS.Core;
using KalikoCMS.Mvc.Framework;
using Microsoft.AspNetCore.Mvc;

namespace TestSiteCore.Controllers
{
    using Models;

    public class TestPageController : PageController<MyPage>
    {
        public override ActionResult Index(MyPage currentPage) {
            return Ok("Routed controller " + currentPage.ContentName);
        }

        public ActionResult Test(MyPage currentPage) {
            return Ok("Test @ " + currentPage.ContentUrl + " " + Url.Action(null, null) + " | " + Url.Action("Hello", null, new { SomeValue = true }));
        }

        public ActionResult Hello(MyPage currentPage, bool someValue) {
            return Ok("Hello worlD");
        }
    }
}
