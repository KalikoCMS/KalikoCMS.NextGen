namespace KalikoCMS.Data.Repositories.Interfaces {
    using System;
    using Entities;

    public interface IContentRepository : IRepository<ContentEntity, Guid> { }
}