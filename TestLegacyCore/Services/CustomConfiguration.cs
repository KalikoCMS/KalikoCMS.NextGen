namespace TestLegacyCore.Services {
    using System;
    using KalikoCMS.Configuration.Interfaces;

    public class CustomConfiguration : ICmsConfiguration
    {
        public string ConnectionString {
            get {
                // logic to get configuration here
                return @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=LegacyCMS;Data Source=(localdb)\v11.0";
            }
        }

        // NOTE: Set to content id for the start page
        public Guid StartPageId => new Guid("1358ddc7-c7d5-4b65-9c94-c9567dd2485b");

        // NOTE: Prevent CMS router answering on root requests when set to true
        public bool IgnoreStartPage => false;

        // NOTE: Remove trailing slash from URLs when set to true
        public bool SkipEndingSlash => true;

        // NOTE: Warming property repository by pre-loading at first request
        public bool WarmupProperties => true;
    }
}