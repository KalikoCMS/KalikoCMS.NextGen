namespace KalikoCMS.Mvc.Framework {
#if NETCORE
    using System;
    using System.Threading.Tasks;
    using Core;
    using Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Infrastructure;
    using Microsoft.AspNetCore.Routing;
    using Services.Content.Interfaces;

#else
    using System.Web.Routing;
    using Interfaces;
#endif

#if NETCORE
    public class CmsRoute : IRouter {
        private readonly IActionSelector _actionSelector;
        private readonly IActionInvokerFactory _actionInvokerFactory;
        private readonly IActionContextAccessor _actionContextAccessor;
        private readonly IUrlResolver _urlResolver;
#else
    public class CmsRoute : Route {
#endif

        #region Constructors

#if NETCORE
        public CmsRoute(IActionSelector actionSelector, IActionInvokerFactory actionInvokerFactory, IActionContextAccessor actionContextAccessor, IUrlResolver urlResolver) {
            _actionSelector = actionSelector;
            _actionInvokerFactory = actionInvokerFactory;
            _actionContextAccessor = actionContextAccessor;
            _urlResolver = urlResolver;
        }
#else
        public CmsRoute(string url, IRouteHandler routeHandler) : base(url, routeHandler) { }

        public CmsRoute(string url, RouteValueDictionary defaults, IRouteHandler routeHandler) : base(url, defaults, routeHandler) { }

        public CmsRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, IRouteHandler routeHandler) : base(url, defaults, constraints, routeHandler) { }

        public CmsRoute(string url, RouteValueDictionary defaults, RouteValueDictionary constraints, RouteValueDictionary dataTokens, IRouteHandler routeHandler) : base(url, defaults, constraints, dataTokens, routeHandler) { }
#endif

        #endregion

#if NETCORE
        public Task RouteAsync(RouteContext context) {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }

            var path = context.HttpContext.Request.Path;
            var content = _urlResolver.GetContent(path, true);
            var action = "Index";

            if (path.Value.Length > content.ContentUrl.Length) {
                var remains = path.Value.Substring(content.ContentUrl.Length).Trim('/');
                if (remains.IndexOf('/') > 0) {
                    // Not an action
                    content = null;
                }
                else {
                    action = remains;
                }
            }

            if (content != null) {
                context.RouteData.Values["controller"] = "TestPage";
                context.RouteData.Values["action"] = action;
                context.RouteData.Values["currentPage"] = content;
            }
            else {
                return Task.CompletedTask;
            }

            var candidates = _actionSelector.SelectCandidates(context);
            if (candidates == null || candidates.Count == 0) {
                //_logger.NoActionsMatched(context.RouteData.Values);
                return Task.CompletedTask;
            }

            var actionDescriptor = _actionSelector.SelectBestCandidate(context, candidates);
            if (actionDescriptor == null) {
                //_logger.NoActionsMatched(context.RouteData.Values);
                return Task.CompletedTask;
            }

            context.Handler = c => {
                var routeData = c.GetRouteData();

                var actionContext = new ActionContext(context.HttpContext, routeData, actionDescriptor);
                if (_actionContextAccessor != null) {
                    _actionContextAccessor.ActionContext = actionContext;
                }

                var invoker = _actionInvokerFactory.CreateInvoker(actionContext);
                if (invoker == null) {
                    throw new InvalidOperationException("Resources.FormatActionInvokerFactory_CouldNotCreateInvoker(actionDescriptor.DisplayName)");
                }

                return invoker.InvokeAsync();
            };

            return Task.CompletedTask;
        }

        public VirtualPathData GetVirtualPath(VirtualPathContext context) {
            var page = context.AmbientValues.ContainsKey("currentPage") ? context.AmbientValues["currentPage"] as CmsPage : null;
            var action = context.Values.ContainsKey("action") ? context.AmbientValues["action"] : null;


            return new VirtualPathData(this, $"{page?.ContentUrl}{action}", context.Values);

            return null;
        }
#else
        public override VirtualPathData GetVirtualPath(RequestContext requestContext, RouteValueDictionary values) {
            object controller = null;

            if (requestContext.HttpContext.Items.Contains("cmsRouting") && values.ContainsKey("controller")) {
                controller = values["controller"];
                values.Remove("controller");
            }

            var virtualPath = base.GetVirtualPath(requestContext, values);

            if (controller != null) {
                values.Add("controller", controller);
            }

            return virtualPath;
        }
#endif
    }
}