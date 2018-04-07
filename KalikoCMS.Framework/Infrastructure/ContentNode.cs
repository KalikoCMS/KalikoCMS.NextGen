namespace KalikoCMS.Infrastructure {
    using System;
    using System.Collections.Generic;

    public class ContentNode {
        public Guid ContentId { get; set; }

        public Guid ContentTypeId { get; set; }
        public int SortOrder { get; set; }

        public ContentNode Parent { get; set; }
        public ICollection<ContentNode> Children { get; }
        public ICollection<LanguageNode> Languages { get; }
        public ICollection<AccessRightsNode> AccessRights { get; }

        public ContentNode() {
            Children = new List<ContentNode>();
            Languages = new List<LanguageNode>();
            AccessRights = new List<AccessRightsNode>();
        }
    }
}