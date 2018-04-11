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

namespace KalikoCMS.AssemblyHelpers {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;
#if NETFULL
    using System.Web.Compilation;
#endif

    public class AssemblyLocator {
        private static IEnumerable<Assembly> _assemblies;


        public static IEnumerable<Assembly> GetAssemblies() {
            if (_assemblies != null) {
                return _assemblies;
            }
#if NETFULL
            _assemblies = BuildManager.GetReferencedAssemblies().Cast<Assembly>();
#else
            _assemblies = AppDomain.CurrentDomain.GetAssemblies();
#endif
            return _assemblies;
        }

        public static IEnumerable<Type> GetTypesWithInterface<T>() where T : class {
            var type = typeof(T);
            if (!type.IsInterface) {
                throw new ArgumentException($"'{type.Name}' is not an interface.");
            }

            var assemblies = AssemblyLocator.GetAssemblies();

            var types = assemblies
                .SelectMany(s => s.GetTypes())
                .Where(t => !t.IsInterface)
                .Where(type.IsAssignableFrom)
                .ToList();

            return types;
        }

        public static IEnumerable<Type> GetTypes<T>() where T : class {
            var assemblies = AssemblyLocator.GetAssemblies();
            var types = assemblies
                .SelectMany(s => s.GetTypes())
                .Where(typeof(T).IsAssignableFrom)
                .ToList();

            return types;
        }
    }
}
