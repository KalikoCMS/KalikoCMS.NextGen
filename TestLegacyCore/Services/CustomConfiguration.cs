namespace TestLegacyCore.Services {
    using KalikoCMS.Configuration.Interfaces;

    public class CustomConfiguration : ICmsConfiguration
    {
        public string ConnectionString {
            get {
                // logic to get configuration here
                return @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=LegacyCMS;Data Source=(LocalDb)\MSSQLLocalDB";
            }
        }

        public bool IgnoreStartPage => false;

        // Remove trailing slash from URLs when set to true
        public bool SkipEndingSlash => true;

        // NOTE: Warming property repository by pre-loading at first request
        public bool WarmupProperties => true;
    }
}