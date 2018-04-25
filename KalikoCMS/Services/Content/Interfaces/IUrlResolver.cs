namespace KalikoCMS.Services.Content.Interfaces {
    using Core;

    public interface IUrlResolver {
        Content GetContent(string path);
        Content GetContent(string path, bool returnPartialMatches);
    }
}