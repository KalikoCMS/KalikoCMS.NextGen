namespace KalikoCMS.Mappers.Interfaces {
    using Core;
    using Infrastructure;

    public interface IContentMapper {
        Content MapToContent(ContentNode node, LanguageNode languageNode);
    }
}