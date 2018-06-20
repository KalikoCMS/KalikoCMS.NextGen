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
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using Core.Collections;

    public class LegacySiteEntity {
        [Key]
        public Guid SiteId { get; set; }

        public string Name { get; set; }
        public string Author { get; set; }
        public SortDirection DefaultChildSortDirection { get; set; }
        public SortOrder DefaultChildSortOrder { get; set; }
        public DateTime UpdateDate { get; set; }
        
        public virtual ICollection<LegacySitePropertyEntity> SiteProperties { get; private set; }

        public LegacySiteEntity() {
            SiteProperties = new HashSet<LegacySitePropertyEntity>();
        }
    }
}
