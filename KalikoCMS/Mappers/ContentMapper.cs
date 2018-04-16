namespace KalikoCMS.Mappers {
    using Core;
    using Infrastructure;
    using Interfaces;
    using Services.Content.Interfaces;

    public class ContentMapper : IContentMapper {
        private readonly IContentTypeResolver _contentTypeResolver;

        public ContentMapper(IContentTypeResolver contentTypeResolver) {
            _contentTypeResolver = contentTypeResolver;
        }

        public Content MapToContent(ContentNode node, LanguageNode languageNode)
        {
            var contentType = _contentTypeResolver.GetContentType(node.ContentTypeId);

            var content = ContentProxy.CreateProxy(contentType.Type);
            content.Author = languageNode.Author;
            content.ChildSortDirection = languageNode.ChildSortDirection;
            content.ChildSortOrder = languageNode.ChildSortOrder;
            content.ContentId = node.ContentId;
            content.ContentLanguageId = languageNode.ContentLanguageId;
            content.ContentName = languageNode.ContentName;
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

            return content;
        }

    }
}