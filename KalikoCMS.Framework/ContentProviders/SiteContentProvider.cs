namespace KalikoCMS.ContentProviders {
    using System;
    using Core.Interfaces;

    public class SiteContentProvider : IContentProvider {
        public static readonly Guid UniqueId = new Guid("511bd642-aad2-4345-9198-c7b4fb40961d");

        public static readonly Guid SiteContentTypeId = new Guid("6f82b8e0-72d3-4cbc-b18d-b8ea56758748");

        public Guid ContentProviderId => UniqueId;
    }
}