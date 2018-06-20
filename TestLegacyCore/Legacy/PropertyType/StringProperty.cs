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

namespace KalikoCMS.PropertyType {
    using System;
    using Attributes;

    [Obsolete("Use the string data type instead in your model.")]
    [PropertyType("296f2f4a-99a5-4b54-96bc-8148830a8fc5", "String", "String", "%AdminPath%Content/PropertyType/StringPropertyEditor.ascx")]
    public class StringProperty {
        public string Value { get; set; }
    }
}