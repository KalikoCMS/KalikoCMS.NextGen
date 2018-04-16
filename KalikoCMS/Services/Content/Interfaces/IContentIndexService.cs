namespace KalikoCMS.Services.Content.Interfaces {
    using System;
    using Core;
    using Infrastructure;

    public interface IContentIndexService {
        void Initialize();
        bool ContentExist(Guid contentId);
        Content GetContent(Guid contentId);
        ContentNode GetNode(Guid contentId);
    }
}