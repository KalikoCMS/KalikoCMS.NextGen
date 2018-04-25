#if NETCORE
namespace KalikoCMS.Mvc {
    using System;
    using System.Threading.Tasks;
    using Core;
    using Microsoft.AspNetCore.Mvc.ModelBinding;
    using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

    public class CmsPageModelBinder : IModelBinder {
        public Task BindModelAsync(ModelBindingContext bindingContext) {
            if (bindingContext == null) {
                throw new ArgumentNullException(nameof(bindingContext));
            }

            var valueProviderResult = bindingContext.ValueProvider.GetValue("currentPage");

            if (valueProviderResult == ValueProviderResult.None) {
                return Task.CompletedTask;
            }

            var currentPage = bindingContext.ActionContext.RouteData.Values["currentPage"];
            if (currentPage == null) {
                return Task.CompletedTask;
            }

            bindingContext.ModelState.SetModelValue("currentPage", valueProviderResult);

            bindingContext.Result = ModelBindingResult.Success(currentPage);
            return Task.CompletedTask;
        }
    }

    public class CmsPageBinderProvider : IModelBinderProvider {
        public IModelBinder GetBinder(ModelBinderProviderContext context) {
            if (context == null) {
                throw new ArgumentNullException(nameof(context));
            }

            if (typeof(CmsPage).IsAssignableFrom(context.Metadata.ModelType)) {
                return new BinderTypeModelBinder(typeof(CmsPageModelBinder));
            }

            return null;
        }
    }
}
#endif