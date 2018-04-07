namespace KalikoCMS.Infrastructure {
    using System;
    using System.Collections.Generic;

    public class ContentIndex {
        private Dictionary<Guid, ContentTree> ContentTrees { get; }
        internal Dictionary<Guid, ContentNode> LookupTable { get; }

        public ContentIndex() {
            ContentTrees = new Dictionary<Guid, ContentTree>();
            LookupTable = new Dictionary<Guid, ContentNode>();
        }

        public ContentTree AddContentTree(Guid contentId) {
            var contentTree = new ContentTree(contentId, LookupTable);
            ContentTrees.Add(contentId, contentTree);

            return contentTree;
        }
    }
}