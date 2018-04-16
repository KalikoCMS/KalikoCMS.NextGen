namespace KalikoCMS.Infrastructure {
    using System;
    using System.Collections.Generic;

    public class ContentNode {
        public Guid ContentId { get; set; }

        public Guid ParentId { get; set; }
        public Guid ContentTypeId { get; set; }
        public int SortOrder { get; set; }
        public int TreeLevel { get; set; }

        public ContentNode Parent { get; set; }
        public IList<ContentNode> Children { get; }
        public IEnumerable<LanguageNode> Languages { get; internal set; }
        public IEnumerable<AccessRightsNode> AccessRights { get; }

        public ContentNode() {
            Children = new List<ContentNode>();
            Languages = new List<LanguageNode>();
            AccessRights = new List<AccessRightsNode>();
        }
    }
}