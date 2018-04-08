namespace KalikoCMS.Services.Initialization {
    using System;
    using Interfaces;

    public class InitializationService : IInitializationService {
        private static bool _isInitialized;

        public InitializationService() { }

        public void Initialize() {
            if (_isInitialized) {
                throw new Exception("CMS already initialized");
            }

            _isInitialized = true;
        }

    }
}