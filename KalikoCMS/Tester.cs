using KalikoCMS.Logging;

namespace KalikoCMS {
    public class Tester {
        private static readonly ILog Logger = LogProvider.For<Tester>();

        public string WhoAmI {
            get {
                Logger.Info("Test");

#if NETFULL
                return "Net 4.61";
#else
                return "Core";
#endif
            }
        }
    }
}