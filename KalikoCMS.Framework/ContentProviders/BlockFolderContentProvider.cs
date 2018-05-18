namespace KalikoCMS.ContentProviders {
    using System;
    using Core.Interfaces;

    public class BlockFolderContentProvider : IContentProvider {
        private static readonly Guid UniqueId = new Guid("861d9f7d-20d4-40eb-98fb-100549bf2f08");

        public Guid ContentProviderId => UniqueId;
    }
}