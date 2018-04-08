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

    public static class AttributeReader {
        public static IEnumerable<Type> GetTypesWithAttribute(Type attributeType) {
            var typesWithAttribute = new List<Type>();
            var attributeAssemblyName = attributeType.Assembly.GetName().Name;
            var assemblies = AssemblyLocator.GetAssemblies();

            typesWithAttribute.AddRange(GetTypesWithAttributeInAssembly(attributeType.Assembly, attributeType));

            var typesWithAttributeInAssembly = assemblies
                .Select(assembly => new {assembly, referencedAssemblies = assembly.GetReferencedAssemblies()})
                .Where(t => t.referencedAssemblies.Any(a => a.Name == attributeAssemblyName))
                .Select(t => GetTypesWithAttributeInAssembly(t.assembly, attributeType));

            foreach (var types in typesWithAttributeInAssembly) {
                typesWithAttribute.AddRange(types);
            }

            return typesWithAttribute;
        }


        private static List<Type> GetTypesWithAttributeInAssembly(Assembly assembly, Type attributeType) {
            var types = assembly.GetTypes();
            return types.Where(type => TypeHasAttribute(type, attributeType)).ToList();
        }


        private static bool TypeHasAttribute(Type type, Type attributeType) {
            var attributes = type.GetCustomAttributes(true);
            return attributes.Any(attributeType.IsInstanceOfType);
        }


        internal static Attribute GetAttribute(Type type, Type attributeType) {
            var attributes = type.GetCustomAttributes(true);
            return attributes.Where(attributeType.IsInstanceOfType).Cast<Attribute>().FirstOrDefault();
        }


        internal static T GetAttribute<T>(Type type) where T : Attribute {
            var attributes = type.GetCustomAttributes(true);
            return (T)attributes.Where(attributeInType => attributeInType is T).Cast<Attribute>().FirstOrDefault();
        }
    }
}
