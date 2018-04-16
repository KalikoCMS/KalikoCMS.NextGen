namespace KalikoCMS.Services.Content {
    using System;
    using System.Linq;
    using Interfaces;
    using Mappers.Interfaces;

    public class ContentLoader : IContentLoader {
        private readonly IContentIndexService _contentIndexService;
        private readonly IContentMapper _contentMapper;

        public ContentLoader(IContentIndexService contentIndexService, IContentMapper contentMapper) {
            _contentIndexService = contentIndexService;
            _contentMapper = contentMapper;
        }

        public T Get<T>(Guid contentId, int languageId, bool bypassAccessCheck = false) where T : class {
            var contentNode = _contentIndexService.GetNode(contentId);
            var languageNode = contentNode.Languages.FirstOrDefault(x => x.LanguageId == languageId);

            // TODO: null checks + access checks

            return _contentMapper.MapToContent(contentNode, languageNode) as T;
        }
    }
}