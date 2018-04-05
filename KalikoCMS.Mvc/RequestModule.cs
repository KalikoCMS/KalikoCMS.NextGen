using System.Collections.Generic;

namespace KalikoCMS.Mvc {
    using System;
    using System.Linq;
    using System.Reflection;
    using System.Web;
    using Core;
    using Core.Interfaces;
    using Framework;
    using Framework.Interfaces;
    using Logging;
#if NETCORE
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Routing;
#else
    using System.Web.Routing;
    using System.Web.Mvc;
#endif

    internal class RequestModule : IStartupSequence {
        private static Dictionary<int, Type> _controllerList;

        public RequestModule() {
            //RequestManager = new RequestManager();
        }

        protected void RedirectToStartPage() {
            //var startPageId = Configuration.SiteSettings.Instance.StartPageId;

            //if (startPageId == Guid.Empty)
            //{
            //    Utils.RenderSimplePage(HttpContext.Current.Response, "Set a start page", "Start page hasn't yet been configured in web.config.");
            //    return;
            //}

            //var page = PageFactory.GetPage(startPageId);

            //if (page == null)
            //{
            //    Utils.RenderSimplePage(HttpContext.Current.Response, "Can't find start page", "Please check your siteSettings configuration in web.config.");
            //    return;
            //}

            //RedirectToController(page);
        }

        public static void RedirectToController(HttpContext httpContext, CmsPage page, string actionName = "index", Dictionary<string, object> additionalRouteData = null, bool isPreview = false) {
            //if (!page.IsAvailable && !isPreview)
            //{
            //    PageHasExpired();
            //    return;
            //}

            //var type = GetControllerType(page);

            var type = _controllerList.FirstOrDefault().Value;

#if NETCORE
            var controller = httpContext.RequestServices.GetService(type);
#else
            var controller = DependencyResolver.Current.GetService(type);
#endif

            var controllerName = StripEnd(type.Name.ToLowerInvariant(), "controller");

            httpContext.Items["cmsRouting"] = true;

            var routeData = new RouteData();
            routeData.Values["controller"] = controllerName;
            routeData.Values["action"] = actionName;
            routeData.Values["currentPage"] = page; //((IPageController)controller).GetTypedPage(page);
            routeData.Values["cmsPageUrl"] = "/pagecontroller"; //page.PageUrl.ToString().Trim('/');

            if (additionalRouteData != null)
            {
                foreach (var data in additionalRouteData)
                {
                    routeData.Values.Add(data.Key, data.Value);
                }
            }

            httpContext.Response.Clear();
#if NETCORE
            //var requestContext = new RequestContext(new HttpContextWrapper(httpContext), routeData);
            //((ControllerBase)controller).Execute(requestContext);
            //httpContext.ApplicationInstance.CompleteRequest();
#else
            var requestContext = new RequestContext(new HttpContextWrapper(httpContext), routeData);
            ((IController)controller).Execute(requestContext);
            httpContext.ApplicationInstance.CompleteRequest();
#endif
        }

        private static Type GetControllerType(CmsPage page) {
            throw new NotImplementedException();
            //if (_controllerList.All(c => c.Key != page.PageTypeId))
            //{
            //    var exception = new Exception(string.Format("No controller is registered for pagetype of page '{0}'", page.PageName));
            //    Logger.Write(exception, Logger.Severity.Critical);
            //    throw exception;
            //}

            //var type = _controllerList[page.PageTypeId];
            //return type;
        }

        private static string StripEnd(string text, string ending) {
            if (text.EndsWith(ending)) {
                return text.Substring(0, text.Length - ending.Length);
            }

            return text;
        }

        public static void RedirectToControllerAction(CmsPage page, string[] parameters) {
            //if (!page.IsAvailable)
            //{
            //    PageHasExpired();
            //    return;
            //}

            //var type = GetControllerType(page);
            //var controller = DependencyResolver.Current.GetService(type);
            //var controllerName = StripEnd(type.Name.ToLowerInvariant(), "controller");
            //var httpContext = new HttpContextWrapper(HttpContext.Current);
            //var route = new CmsRoute(page.PageUrl.ToString().TrimStart('/') + "{action}", new MvcRouteHandler());
            //var routeData = route.GetRouteData(httpContext);

            //if (routeData == null)
            //{
            //    var message = string.Format("Not an action /{0}/{1}/", controllerName, string.Join("/", parameters));
            //    throw new Exception(message);
            //}

            //routeData.Values["controller"] = controllerName;
            //routeData.Values["currentPage"] = ((IPageController)controller).GetTypedPage(page);

            //HttpContext.Current.Response.Clear();
            //var requestContext = new RequestContext(new HttpContextWrapper(HttpContext.Current), routeData);
            //((IController)controller).Execute(requestContext);
            //HttpContext.Current.ApplicationInstance.CompleteRequest();
        }

#region Startup sequence

        public int StartupOrder => 10;

        public void Startup() {
            _controllerList = BuildControllerList();
            InjectMvcRoute();
        }

        private void InjectMvcRoute() {
#if NETCORE
            //throw new NotImplementedException();
            //var route = new CmsRoute(new MvcRouteHandler(), "{cmsPageUrl}/{action}", new DefaultInlineConstraintResolver());
            //route.Constraints.Add("cmsPageUrl", new CmsRouteConstraint());
            //route.Defaults.Add("action", "Index");
#else
            var route = new CmsRoute("{cmsPageUrl}/{action}", new MvcRouteHandler()) {
                Constraints = new RouteValueDictionary { { "cmsPageUrl", new CmsRouteConstraint() } },
                Defaults = new RouteValueDictionary { { "action", "Index" } }
            };

            RouteTable.Routes.Insert(0, route);
#endif
        }

        private static Dictionary<int, Type> BuildControllerList() {
            var controllerList = new Dictionary<int, Type>();

            var assemblies = AssemblyReaders.AssemblyReader.GetAssemblies();

            foreach (var assembly in assemblies) {
                if (IsGenericAssembly(assembly)) {
                    continue;
                }

                foreach (var definedType in assembly.DefinedTypes) {
                    if (definedType.ImplementedInterfaces.All(i => i != typeof(IPageController))) {
                        continue;
                    }

                    if (definedType.BaseType == null) {
                        continue;
                    }

                    if (definedType.IsAbstract) {
                        continue;
                    }

                    // TODO: Add pagetype check
                    //var pageType = PageType.GetPageType(definedType.BaseType.GenericTypeArguments.FirstOrDefault());

                    //if (pageType == null)
                    //{
                    //    continue;
                    //}

                    //controllerList.Add(pageType.PageTypeId, definedType.AsType());
                    controllerList.Add(1, definedType.AsType());
                }
            }

            return controllerList;
        }

        private static bool IsGenericAssembly(Assembly assembly) {
            var knownAssemblyNames = new[] {"System.", "Microsoft.", "KalikoCMS.", "Lucene."};
            var isGenericAssembly = knownAssemblyNames.Any(knownAssemblyName => assembly.FullName.StartsWith(knownAssemblyName));

            return isGenericAssembly;
        }

#endregion

    }
}