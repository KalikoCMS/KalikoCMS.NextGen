namespace KalikoCMS.Services.Content.Interfaces {
    using Core;

    public interface IContentCreator {
        T CreateNew<T>() where T : Content;
        void Save(Content content);
    }
}