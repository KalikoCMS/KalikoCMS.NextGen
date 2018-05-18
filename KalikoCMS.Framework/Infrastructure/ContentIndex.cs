namespace KalikoCMS.Infrastructure {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using ContentProviders;

    public class ContentIndex {
        internal Dictionary<Guid, ContentTree> ContentTrees { get; }
        internal Dictionary<Guid, ContentNode> LookupTable { get; }

        public ContentIndex() {
            ContentTrees = new Dictionary<Guid, ContentTree>();
            LookupTable = new Dictionary<Guid, ContentNode>();
        }

        private ContentTree AddContentTree(Guid contentId, Guid contentTypeId, Guid contentProviderId) {
            var contentTree = new ContentTree(contentId, contentTypeId, contentProviderId, LookupTable);
            ContentTrees.Add(contentId, contentTree);

            return contentTree;
        }

        public void AddChild(ContentNode node) {
            // TODO: Error handling
            if (node.ParentId == Guid.Empty) {
                var tree = AddContentTree(node.ContentId, node.ContentTypeId, node.ContentProviderId);
                tree.AddChild(node);
            }
            else {
                var parentNode = LookupTable[node.ParentId];
                node.Parent = parentNode;
                parentNode.Children.Add(node);
                LookupTable.Add(node.ContentId, node);
            }

            GenerateContentUrl(node);
        }

        internal void GenerateContentUrl(ContentNode node) {
            if (node.ContentProviderId == SiteContentProvider.UniqueId) {
                foreach (var languageNode in node.Languages) {
                    languageNode.ContentUrl = "/";
                }
                return;
            }

            if (node.ContentProviderId == PageContentProvider.UniqueId) {
                foreach (var languageNode in node.Languages) {
                    var parent = node.Parent.Languages.FirstOrDefault(x => x.LanguageId == languageNode.LanguageId);
                    // TODO: Add error handling
                    languageNode.ContentUrl = $"{parent.ContentUrl}{languageNode.UrlSegment}/";
                }
                return;
            }

            foreach (var languageNode in node.Languages) {
                languageNode.ContentUrl = $"~link:{node.ContentId}";
            }
        }
    }
}