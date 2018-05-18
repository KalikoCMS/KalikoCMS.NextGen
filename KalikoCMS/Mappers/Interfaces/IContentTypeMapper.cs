namespace KalikoCMS.Mappers.Interfaces {
    using Core;
    using Data.Entities;

    public interface IContentTypeMapper : IMapper<ContentTypeEntity, ContentType>, IMapper<ContentType, ContentTypeEntity> { }
}
