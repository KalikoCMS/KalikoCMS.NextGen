namespace KalikoCMS.Services.Content.Interfaces {
    using System;
    using System.Collections.Generic;
    using Core;
    using Infrastructure;

    public interface IContentIndexService {
        void Initialize();
        bool ContentExist(Guid contentId);
        Content GetContent(Guid contentId);
        ContentNode GetNode(Guid contentId);
        IEnumerable<ContentNode> GetRootNodes(Guid contentTypeId);
        Content GetContentFromNode(ContentNode node);
    }
}