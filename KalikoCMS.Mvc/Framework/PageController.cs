namespace KalikoCMS.Mvc.Framework {
    using System;
    using Core;
    using Interfaces;
#if NETCORE
    using Microsoft.AspNetCore.Mvc;
#else
    using System.Web.Mvc;
#endif

    public abstract class PageController<T> : Controller, IPageController where T : CmsPage {
        public abstract ActionResult Index(T currentPage);
    }
}