namespace KalikoCMS.ContentProviders {
    using System;
    using Core.Interfaces;

    public class PageContentProvider : IContentProvider {
        private static readonly Guid UniqueId = new Guid("017db147-1d7d-40a0-958a-6982b6d5668d");

        public Guid ContentProviderId => UniqueId;
    }
}