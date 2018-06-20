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

namespace KalikoCMS.Legacy.Data.Entities {
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Core.Collections;

    public class LegacyPageTypeEntity {
        [Key]
        public int PageTypeId { get; set; }

        public string Name { get; set; }
        public SortDirection DefaultChildSortDirection { get; set; }
        public SortOrder DefaultChildSortOrder { get; set; }
        public string DisplayName { get; set; }
        public string PageTypeDescription { get; set; }
        public string PageTemplate { get; set; }
        public bool? ShowPublishDates { get; set; }
        public bool? ShowSortOrder { get; set; }
        public bool? ShowVisibleInMenu { get; set; }

        public virtual ICollection<LegacyPropertyEntity> Properties { get; set; }
        public virtual ICollection<LegacyPageEntity> Pages { get; set; }

        public LegacyPageTypeEntity() {
            Pages = new HashSet<LegacyPageEntity>();
            Properties = new HashSet<LegacyPropertyEntity>();
        }
    }
}
