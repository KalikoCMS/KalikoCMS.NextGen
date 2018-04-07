namespace KalikoCMS.Infrastructure {
    using System;
    using System.Collections.Generic;

    public class ContentTree {
        public Guid ContentId { get; }
        private Dictionary<Guid, ContentNode> LookupTable { get; }
        internal ICollection<ContentNode> Children { get; }

        public ContentTree(Guid contentId, Dictionary<Guid, ContentNode> lookupTable) {
            ContentId = contentId;
            LookupTable = lookupTable;
            Children = new List<ContentNode>();
        }
    }
}