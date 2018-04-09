namespace KalikoCMS.Services.Initialization {
    using System;
    using Content.Interfaces;
    using Interfaces;
    using Logging;

    public class InitializationService : IInitializationService {
        private static readonly ILog Logger = LogProvider.For<InitializationService>();
        private static bool _isInitialized;

        private readonly IContentTypeResolver _contentTypeResolver;
        private readonly IPropertyTypeResolver _propertyTypeResolver;
        private readonly IContentIndexService _contentIndexService;

        public InitializationService(IContentTypeResolver contentTypeResolver, IPropertyTypeResolver propertyTypeResolver, IContentIndexService contentIndexService) {
            _contentTypeResolver = contentTypeResolver;
            _propertyTypeResolver = propertyTypeResolver;
            _contentIndexService = contentIndexService;
        }

        public void Initialize() {
            Logger.Info("Start initialization");
            if (_isInitialized) {
                Logger.Error("CMS already initialized");
                return;
            }

            try {
                _propertyTypeResolver.Initialize();
                _contentTypeResolver.Initialize();
                _contentIndexService.Initialize();
                
                _isInitialized = true;
                Logger.Info("Finished initialization");
            }
            catch (Exception exception) {
                Logger.FatalException("Failed to initialize the CMS", exception);
                throw;
            }
        }

    }
}