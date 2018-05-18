using Castle.DynamicProxy;
using KalikoCMS.Core.Interfaces;

namespace KalikoCMS.Core.Interceptors {
    using System;

    public class PropertyInterceptor : IInterceptor {
        public void Intercept(IInvocation invocation) {
            if (!invocation.Method.IsVirtual) {
                invocation.Proceed();
            }
            else {
                HandleVirtualMethods(invocation);
            }
        }

        private static void HandleVirtualMethods(IInvocation invocation) {
            var methodName = invocation.Method.Name;

            // Handle properties if they are CMS properties
            if (methodName.StartsWith("get_")) {
                var currentPage = invocation.InvocationTarget as IContent;
                var propertyName = methodName.Substring(4);
                var propertyData = currentPage.Property.GetItem(propertyName);

                if (propertyData != null) {
                    invocation.ReturnValue = propertyData.Value;
                    return;
                }
            }
            else if (methodName.StartsWith("set_")) {
                var currentPage = (IContent)invocation.InvocationTarget;
                var propertyName = methodName.Substring(4);
                var propertyData = currentPage.Property.GetItem(propertyName);

                if (propertyData != null) {
                    if (!currentPage.IsEditable) {
                        throw new Exception("Content isn't editable, create an editable version before making property changes.");
                    }

                    propertyData.Value = invocation.Arguments[0];
                    return;
                }
            }

            invocation.Proceed();
        }
    }
}