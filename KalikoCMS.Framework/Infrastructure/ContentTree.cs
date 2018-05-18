namespace KalikoCMS.Infrastructure {
    using System;
    using System.Collections.Generic;

    public class ContentTree {
        public Guid ContentId { get; }
        private Dictionary<Guid, ContentNode> LookupTable { get; }
        internal ICollection<ContentNode> Children { get; }
        public Guid ContentTypeId { get; }
        public Guid ContentProviderId { get; }

        public void AddChild(ContentNode contentNode) {
            LookupTable.Add(contentNode.ContentId, contentNode);
            Children.Add(contentNode);
        }

        public ContentTree(Guid contentId, Guid contentTypeId, Guid contentProviderId, Dictionary<Guid, ContentNode> lookupTable) {
            ContentId = contentId;
            ContentTypeId = contentTypeId;
            ContentProviderId = contentProviderId;
            LookupTable = lookupTable;
            Children = new List<ContentNode>();
        }
    }
}