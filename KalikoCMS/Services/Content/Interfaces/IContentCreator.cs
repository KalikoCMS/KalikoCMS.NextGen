namespace KalikoCMS.Services.Content.Interfaces {
    using Core;

    public interface IContentCreator {
        T Create<T>() where T : Content;
    }
}