using Castle.DynamicProxy;
using KalikoCMS.Core.Interfaces;

namespace KalikoCMS.Core.Interceptors {
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
                var propertyExists = false;
                var currentPage = (IContent) invocation.InvocationTarget;
                var propertyName = methodName.Substring(4);
                //var propertyData = currentPage.Property.GetPropertyValue(propertyName, out propertyExists);

                if (propertyExists) {
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