﻿#region License and copyright notice
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

namespace KalikoCMS.Attributes {
    using System;
    using KalikoCMS.Core.Collections;

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class PageTypeAttribute : Attribute {
        internal const int DefaultSortOrder = 100;

        public PageTypeAttribute(string uniqueId, string displayName) {
            // TODO: Add GUID validator
            UniqueId = new Guid(uniqueId);
            DisplayName = displayName;
        }

        public Type[] AllowedTypes { get; set; }
        public SortDirection DefaultChildSortDirection { get; set; }
        public SortOrder DefaultChildSortOrder { get; set; }
        public string DisplayName { get; }
        public Guid UniqueId { get; }
        public string PageTypeDescription { get; set; }
        public string PreviewImage { get; set; }
    }
}