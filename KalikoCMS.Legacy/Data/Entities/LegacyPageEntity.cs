#region License and copyright notice
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
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class LegacyPageEntity {
        [Key]
        public virtual Guid PageId { get; set; }

        public virtual int PageTypeId { get; set; }
        public virtual Guid ParentId { get; set; }
        public virtual Guid RootId { get; set; }

        public virtual int TreeLevel { get; set; }
        public virtual int SortOrder { get; set; }

        public virtual LegacyPageTypeEntity PageType { get; set; }
        public virtual ICollection<LegacyPageTagEntity> PageTags { get; private set; }
        public virtual ICollection<LegacyPageInstanceEntity> PageInstances { get; set; }
        public virtual ICollection<LegacyPagePropertyEntity> PageProperties { get; set; }

        public LegacyPageEntity() {
            PageInstances = new HashSet<LegacyPageInstanceEntity>();
            PageProperties = new HashSet<LegacyPagePropertyEntity>();
            PageTags = new HashSet<LegacyPageTagEntity>();
        }
    }
}