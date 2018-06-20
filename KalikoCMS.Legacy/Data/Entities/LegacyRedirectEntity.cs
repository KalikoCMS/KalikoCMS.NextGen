﻿#region License and copyright notice
/* 
 * Kaliko Content Management System
 * 
 * Copyright (c) Fredrik Schultz
 * 
 * This library is free software; you can redistribute it and/or
 * modify it under the terms of the GNU Lesser General internal 
 * License as published by the Free Software Foundation; either
 * version 3.0 of the License, or (at your option) any later version.
 * 
 * This library is distributed in the hope that it will be useful,
 * but WITHOUT ANY WARRANTY; without even the implied warranty of
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the GNU
 * Lesser General internal  License for more details.
 * http://www.gnu.org/licenses/lgpl-3.0.html
 */
#endregion

namespace KalikoCMS.Legacy.Data.Entities {
    using System;
    using System.ComponentModel.DataAnnotations;
    using Core;

    public class LegacyRedirectEntity
    {
        public LegacyRedirectEntity() {}

        public LegacyRedirectEntity(CmsPage page) {
            PageId = page.ContentId;
            LanguageId = page.LanguageId;
            Url = page.ContentUrl.TrimStart('/').ToLowerInvariant();
            UrlHash = Url.GetHashCode();
        }

        [Key]
        public int RedirectId { get; set; }

        public int UrlHash { get; set; }
        public string Url { get; set; }
        public Guid PageId { get; set; }
        public int LanguageId { get; set; }
    }
}
