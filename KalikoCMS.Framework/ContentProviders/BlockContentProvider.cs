namespace KalikoCMS.ContentProviders {
    using System;
    using Core.Interfaces;

    public class BlockContentProvider : IContentProvider {
        private static readonly Guid UniqueId = new Guid("c40dcf2b-9593-40ff-a575-62ca939a4a70");

        public Guid ContentProviderId => UniqueId;
    }
}