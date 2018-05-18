namespace KalikoCMS.Services.Content.Interfaces {
    using Core;

    public interface IContentCreator {
        T CreateNew<T>(ContentReference parent, bool bypassAccessCheck = false) where T : Content;
        void Save(Content content, bool bypassAccessCheck = false);
        void Publish(Content content, bool bypassAccessCheck = false);
    }
}