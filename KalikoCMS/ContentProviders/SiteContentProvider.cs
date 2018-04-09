namespace KalikoCMS.ContentProviders {
    using System;
    using Core.Interfaces;

    public class SiteContentProvider : IContentProvider {
        public static readonly Guid UniqueId = new Guid("511bd642-aad2-4345-9198-c7b4fb40961d");

        public Guid ContentProviderId => UniqueId;
    }
}