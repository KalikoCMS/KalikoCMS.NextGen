#region License and copyright notice
/* 
 * Kaliko Content Management System
 * 
 * Copyright (c) Fredrik Schultz
 * 
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General Public
 * License as published by the Free Software Foundation; either
 * version 3.0 of the License, or (at your option) any later version.
 * 
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
 * Lesser General Public License for more details.
 * http://www.gnu.org/licenses/lgpl-3.0.html
 */
#endregion

namespace KalikoCMS.Core {
    using System;
    using Castle.DynamicProxy;
    using KalikoCMS.Core.Interceptors;

    public class ContentProxy {
        public static Content CreateProxy(Type type, bool isEditable = false) {
            if (type == null) {
                throw new ArgumentException("Type argument can't be empty");
            }

            var proxyGenerator = new ProxyGenerator();
            var proxy = proxyGenerator.CreateClassProxy(type, new PropertyInterceptor()) as Content;
            if (proxy == null) {
                throw new Exception($"Could not create proxy for type '{type.Name}'");
            }

            proxy.IsEditable = isEditable;

            return proxy;
        }
    }
}