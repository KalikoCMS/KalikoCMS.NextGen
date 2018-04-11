namespace TestSiteCore.Controllers {
    using KalikoCMS.Data.Entities;
    using KalikoCMS.Data.Repositories.Interfaces;
    using KalikoCMS.Services.Content.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Models;

    public class BuildSiteController : Controller {
        private readonly ILanguageRepository _languageRepository;
        private readonly IContentCreator _contentCreator;

        public BuildSiteController(ILanguageRepository languageRepository, IContentCreator contentCreator) {
            _languageRepository = languageRepository;
            _contentCreator = contentCreator;
        }

        public ActionResult Index() {
            var language = _languageRepository.FirstOrDefault(x => x.Culture == "en");
            if (language == null) {
                language = new LanguageEntity {
                    Culture = "en",
                    DisplayName = "English",
                    UrlSegment = "en"
                };
                _languageRepository.Create(language);
            }

            var page = _contentCreator.Create<MyPage>();
            

            return Content("Done" + language.LanguageId + "_" + page.Status);
        }
    }
}