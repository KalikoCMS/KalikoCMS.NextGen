namespace KalikoCMS.Mvc {
    using System;
    using Logging;
    using ServiceLocation;
    using Services.Initialization.Interfaces;

    public class Bootstrapper {
        private static bool _isInitialized;
        private static bool _isRunning;
        private static readonly object Padlock = new object();
        private static readonly ILog Logger = LogProvider.For<Bootstrapper>();

        internal static bool Initialize() {
            if (_isRunning) {
                return false;
            }

            if (_isInitialized) {
                return true;
            }

            lock(Padlock) {
                _isRunning = true;

                try {
                    var initializationService = ServiceLocator.Current.GetInstance<IInitializationService>();
                    initializationService.Initialize();

                    _isInitialized = true;
                    return true;
                }
                catch (Exception exception) {
                    Logger.FatalException("Failed to initialize CMS", exception);
                    return false;
                }
                finally {
                    _isRunning = false;
                }
            }
        }
    }
}