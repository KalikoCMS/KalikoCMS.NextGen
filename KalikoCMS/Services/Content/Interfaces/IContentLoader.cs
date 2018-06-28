namespace KalikoCMS.Services.Content.Interfaces {
    using System;
    using System.Collections.Generic;
    using Core;
    using Core.Interfaces;

    public interface IContentLoader {
        T Get<T>(Guid contentId, int languageId, bool bypassAccessCheck = false) where T : Content;

        T Get<T>(ContentReference contentReference, bool bypassAccessCheck = false) where T : Content;

        IEnumerable<IContent> GetAncestors(ContentReference contentReference, bool bypassAccessCheck = false);

        T GetClosest<T>(ContentReference contentReference, bool bypassAccessCheck = false) where T : Content;

        IEnumerable<IContent> GetChildren(ContentReference contentReference, bool bypassAccessCheck = false);

        IEnumerable<T> GetChildren<T>(ContentReference contentReference, bool bypassAccessCheck = false) where T : Content;

        IEnumerable<IContent> GetDescendents(ContentReference contentReference, bool bypassAccessCheck = false);

        IEnumerable<T> GetDescendents<T>(ContentReference contentReference, bool bypassAccessCheck = false) where T : Content;
    }
}