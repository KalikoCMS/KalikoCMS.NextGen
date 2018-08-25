namespace KalikoCMS.Services.Initialization {
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using AssemblyHelpers;
    using Configuration.Interfaces;
    using Content.Interfaces;
    using Core.Interfaces;
    using Interfaces;
    using Logging;

    public class InitializationService : IInitializationService {
        private static readonly ILog Logger = LogProvider.For<InitializationService>();
        private static bool _isInitialized;

        private readonly IContentTypeResolver _contentTypeResolver;
        private readonly IPropertyTypeResolver _propertyTypeResolver;
        private readonly IContentIndexService _contentIndexService;
        private readonly IDomainResolver _domainResolver;
        private readonly IPropertyResolver _propertyResolver;
        private readonly ICmsConfigurataion _configurataion;

        public InitializationService(IContentTypeResolver contentTypeResolver, IPropertyTypeResolver propertyTypeResolver, IContentIndexService contentIndexService, IDomainResolver domainResolver, IPropertyResolver propertyResolver, ICmsConfigurataion configurataion) {
            _contentTypeResolver = contentTypeResolver;
            _propertyTypeResolver = propertyTypeResolver;
            _contentIndexService = contentIndexService;
            _domainResolver = domainResolver;
            _propertyResolver = propertyResolver;
            _configurataion = configurataion;
        }

        public void Initialize() {
            Logger.Info("Start initialization");
            if (_isInitialized) {
                Logger.Error("CMS already initialized");
                return;
            }

            try {
                var startupSequence = GetStartupSequence();
                ExecutePreStartupSequence(startupSequence);

                _propertyTypeResolver.Initialize();
                _contentTypeResolver.Initialize();
                _contentIndexService.Initialize();
                _domainResolver.Initialize();

                ApplyConditionalInitializations();

                ExecutePostStartupSequence(startupSequence);

                _isInitialized = true;
                Logger.Info("Finished initialization");
            }
            catch (Exception exception) {
                Logger.FatalException("Failed to initialize the CMS", exception);
                throw;
            }
        }

        private void ApplyConditionalInitializations() {
            if (_configurataion.WarmupProperties) {
                _propertyResolver.Preload();
            }
        }

        #region Startup sequence

        private static List<IStartupSequence> GetStartupSequence() {
            var types = AssemblyLocator.GetTypesWithInterface<IStartupSequence>();
            var sequences = new List<IStartupSequence>();

            foreach (var type in types) {
                if (type.IsInterface) {
                    continue;
                }

                if (Activator.CreateInstance(type) is IStartupSequence startupSequence) {
                    sequences.Add(startupSequence);
                }
            }

            return sequences;
        }

        private static void ExecutePreStartupSequence(IEnumerable<IStartupSequence> sequences) {
            foreach (var startupSequence in sequences.Where(s => s.StartupOrder < 0).OrderBy(s => s.StartupOrder)) {
                startupSequence.Startup();
            }
        }

        private static void ExecutePostStartupSequence(IEnumerable<IStartupSequence> sequences) {
            foreach (var startupSequence in sequences.Where(s => s.StartupOrder >= 0).OrderBy(s => s.StartupOrder)) {
                startupSequence.Startup();
            }
        }

        #endregion Startup sequence
    }
}