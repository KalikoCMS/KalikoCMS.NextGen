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
                var currentPage = (IContent) invocation.InvocationTarget;
                var propertyName = methodName.Substring(4);
                //var propertyData = currentPage.Property.GetPropertyValue(propertyName, out propertyExists);
                var propertyData = currentPage.Property.GetItem(propertyName);

                if (propertyData != null) {
                    invocation.ReturnValue = propertyData.Value;
                    //    invocation.ReturnValue = GetPropertyValue(method, propertyData);
                    return;
                }
            }
            else if (methodName.StartsWith("set_"))
            {
                var currentPage = (IContent)invocation.InvocationTarget;
                var propertyName = methodName.Substring(4);
                //var propertyData = currentPage.Property.GetPropertyValue(propertyName, out propertyExists);
                var propertyData = currentPage.Property.GetItem(propertyName);

                if (propertyData != null) {
                    if (!currentPage.IsEditable) {
                        throw new Exception("Content isn't editable, create an editable version before making property changes.");
                    }

                    propertyData.Value = invocation.Arguments[0];
                    //    invocation.ReturnValue = GetPropertyValue(method, propertyData);
                    return;
                }
            }

            invocation.Proceed();
        }

        //private object GetPropertyValue(MethodBase method, PropertyData propertyData)
        //{
        //    if (propertyData == null)
        //    {
        //        return Activator.CreateInstance(((MethodInfo)method).ReturnType);
        //    }

        //    return propertyData;
        //}
    }
}