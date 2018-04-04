
namespace KalikoCMS.Mvc.Framework.Interfaces {
    using Core;

    public interface IPageController {
        CmsPage GetTypedPage(CmsPage page);
    }
}