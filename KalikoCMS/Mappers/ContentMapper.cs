namespace KalikoCMS.Mappers {
    using Configuration.Interfaces;
    using Core;
    using Infrastructure;
    using Interfaces;
    using Services.Content.Interfaces;

    public class ContentMapper : IContentMapper {
        private readonly IContentTypeResolver _contentTypeResolver;
        private readonly ICmsConfiguration _configuration;

        public ContentMapper(IContentTypeResolver contentTypeResolver, ICmsConfiguration configuration) {
            _contentTypeResolver = contentTypeResolver;
            _configuration = configuration;
        }

        public Content MapToContent(ContentNode node, LanguageNode languageNode) {
            if (node == null || languageNode == null) {
                return null;
            }

            var contentType = _contentTypeResolver.GetContentType(node.ContentTypeId);

            var content = ContentProxy.CreateProxy(contentType.Type);
            content.Author = languageNode.Author;
            content.ChildSortDirection = languageNode.ChildSortDirection;
            content.ChildSortOrder = languageNode.ChildSortOrder;
            content.ContentId = node.ContentId;
            content.ContentLanguageId = languageNode.ContentLanguageId;
            content.ContentName = languageNode.ContentName;
            content.ContentProviderId = node.ContentProviderId;
            content.ContentTypeId = node.ContentTypeId;
            content.ContentUrl = languageNode.ContentUrl;
            content.CreatedDate = languageNode.CreatedDate;
            content.CurrentVersion = languageNode.CurrentVersion;
            content.LanguageId = languageNode.LanguageId;
            content.ParentId = node.ParentId;
            content.SortOrder = node.SortOrder;
            content.StartPublish = languageNode.StartPublish;
            content.Status = languageNode.Status;
            content.StopPublish = languageNode.StopPublish;
            content.TreeLevel = node.TreeLevel;
            content.UpdateDate = languageNode.UpdateDate;
            content.UrlSegment = languageNode.UrlSegment;
            content.VisibleInMenu = languageNode.VisibleInMenu;
            content.VisibleInSitemap = languageNode.VisibleInSitemap;

            if (_configuration.SkipEndingSlash) {
                content.ContentUrl = content.ContentUrl.TrimEnd('/');
            }

            return content;
        }

        public ContentNode MapFromContent(Content content) {
            var node = new ContentNode {
                ContentId = content.ContentId,
                ContentProviderId = content.ContentProviderId,
                ContentTypeId = content.ContentTypeId,
                ParentId = content.ParentId,
                SortOrder = content.SortOrder,
                TreeLevel = content.TreeLevel
            };

            var languageNode = new LanguageNode {
                Author = content.Author,
                ChildSortDirection = content.ChildSortDirection,
                ChildSortOrder = content.ChildSortOrder,
                ContentLanguageId = content.ContentLanguageId,
                ContentName = content.ContentName,
                ContentUrl = content.ContentUrl,
                CreatedDate = content.CreatedDate,
                CurrentVersion = content.CurrentVersion,
                LanguageId = content.LanguageId,
                StartPublish = content.StartPublish,
                Status = content.Status,
                StopPublish = content.StopPublish,
                UpdateDate = content.UpdateDate,
                UrlSegment = content.UrlSegment,
                VisibleInMenu = content.VisibleInMenu,
                VisibleInSitemap = content.VisibleInSitemap
            };

            if (_configuration.SkipEndingSlash && !languageNode.ContentUrl.EndsWith("/")) {
                languageNode.ContentUrl += '/';
            }

            node.Languages.Add(languageNode);

            return node;
        }
    }
}