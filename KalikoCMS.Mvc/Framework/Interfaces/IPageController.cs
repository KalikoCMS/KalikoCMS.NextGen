
namespace KalikoCMS.Mvc.Framework.Interfaces {
    using Core;

    internal interface IPageController {
        CmsPage GetTypedPage(CmsPage page);
    }
}