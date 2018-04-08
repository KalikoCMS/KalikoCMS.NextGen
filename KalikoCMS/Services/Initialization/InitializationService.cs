namespace KalikoCMS.Services.Initialization {
    using System;
    using Content.Interfaces;
    using Interfaces;
    using Logging;

    public class InitializationService : IInitializationService {
        private static readonly ILog Logger = LogProvider.For<InitializationService>();
        private static bool _isInitialized;

        private readonly IContentTypeResolver _contentTypeResolver;

        public InitializationService(IContentTypeResolver contentTypeResolver) {
            _contentTypeResolver = contentTypeResolver;
        }

        public void Initialize() {
            Logger.Info("InitializationService.Initialize");
            if (_isInitialized) {
                Logger.Error("CMS already initialized");
                return;
            }

            try {
                _contentTypeResolver.Initialize();

                _isInitialized = true;
            }
            catch (Exception exception) {
                Logger.FatalException("Failed to initialize the CMS", exception);
                throw;
            }
        }

    }
}