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

        private ContentTree AddContentTree(Guid contentId) {
            var contentTree = new ContentTree(contentId, LookupTable);
            ContentTrees.Add(contentId, contentTree);

            return contentTree;
        }

        public void AddChild(ContentNode node) {
            // TODO: Error handling
            if (node.ParentId == Guid.Empty) {
                var tree = AddContentTree(node.ContentId);
                tree.AddChild(node);
            }
            else {
                var parentNode = LookupTable[node.ParentId];
                node.Parent = parentNode;
                parentNode.Children.Add(node);
                LookupTable.Add(node.ContentId, node);
            }
        }
    }
}