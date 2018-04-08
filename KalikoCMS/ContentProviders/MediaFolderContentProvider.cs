namespace KalikoCMS.ContentProviders {
    using System;
    using Core.Interfaces;

    public class MediaFolderContentProvider : IContentProvider {
        private static readonly Guid UniqueId = new Guid("2588945e-6961-43fe-9e95-c379740ff32a");

        public Guid ContentProviderId => UniqueId;
    }
}