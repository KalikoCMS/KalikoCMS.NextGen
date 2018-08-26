namespace TestLegacyCore.Services {
    using KalikoCMS.Configuration.Interfaces;

    public class CustomConfiguration : ICmsConfigurataion
    {
        public string ConnectionString {
            get {
                // logic to get configuration here
                return @"Integrated Security=SSPI;Persist Security Info=False;Initial Catalog=LegacyCMS;Data Source=(localdb)\v11.0";
            }
        }

        public bool IgnoreStartPage => true;

        // NOTE: Warming property repository by preloading at first request
        public bool WarmupProperties => true;
    }
}