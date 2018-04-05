using KalikoCMS.Core;
using KalikoCMS.Mvc.Framework;
using Microsoft.AspNetCore.Mvc;

namespace TestSiteCore.Controllers
{
    public class TestPageController : PageController<CmsPage>
    {
        public override ActionResult Index(CmsPage currentPage) {
            return Ok("Routed controller " + (currentPage != null));
        }
    }
}
