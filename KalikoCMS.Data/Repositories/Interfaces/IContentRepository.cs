namespace KalikoCMS.Data.Repositories.Interfaces {
    using System;
    using System.Collections.Generic;
    using Entities;
    using Infrastructure;

    public interface IContentRepository : IRepository<ContentEntity, Guid> {
        IEnumerable<ContentNode> GetContentNodes(Guid contentProviderId);
    }
}