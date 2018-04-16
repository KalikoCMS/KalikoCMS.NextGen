namespace TestSiteCore.Controllers {
    using System;
    using KalikoCMS.Data.Entities;
    using KalikoCMS.Data.Repositories.Interfaces;
    using KalikoCMS.Serialization;
    using KalikoCMS.ServiceLocation;
    using KalikoCMS.Services.Content.Interfaces;
    using Microsoft.AspNetCore.Html;
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


            var page = _contentCreator.CreateNew<MyPage>();
            page.ContentName = "Test page 3";
            page.TestHtmlString = new HtmlString("<h1>HTML STRING</h1>");
            page.TestString = "Lorem ipsum";
            page.ParentId = new Guid("4B619F02-CC0F-4B9A-85D5-08D5A0B0E806");
            page.LanguageId = language.LanguageId;
            _contentCreator.Save(page);


            return Content(JsonSerialization.SerializeJson(new HtmlString("<b>HEJ</b>")));

            //var site = _contentCreator.CreateNew<MySite>();
            //site.ContentName = "My website";
            //site.LanguageId = language.LanguageId;

            //_contentCreator.Save(site);


            //var page = _contentCreator.CreateNew<MyPage>();
            //page.ContentName = "Test page";
            //page.TestHtmlString = new HtmlString("<h1>HTML STRING</h1>");
            //page.TestString = "Lorem ipsum";
            //page.ParentId = new Guid("4B619F02-CC0F-4B9A-85D5-08D5A0B0E806");
            //page.LanguageId = language.LanguageId;
            //_contentCreator.Save(page);

            var contentIndexService = ServiceLocator.Current.GetInstance<IContentIndexService>();
            var site = contentIndexService.GetContent(new Guid("4B619F02-CC0F-4B9A-85D5-08D5A0B0E806"));

            return Content("Done");
        }
    }
}