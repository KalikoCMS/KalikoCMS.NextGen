namespace KalikoCMS.ContentProviders {
    using System;
    using Core.Interfaces;

    public class MediaContentProvider : IContentProvider {
        private static readonly Guid UniqueId = new Guid("8f3c081b-47a3-46e7-9f1d-09c2341ae407");

        public Guid ContentProviderId => UniqueId;
    }
}