namespace KalikoCMS.ContentProviders {
    using System;

    public interface IPageExtender {
        bool HandleRequest(Guid pageId, string[] remainingSegments);
    }
}
