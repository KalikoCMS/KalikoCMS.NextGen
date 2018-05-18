namespace TestSiteCore.Controllers {
    using System;
    using KalikoCMS.Core;
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
        private readonly IDomainResolver _domainResolver;

        public BuildSiteController(ILanguageRepository languageRepository, IContentCreator contentCreator, IDomainResolver domainResolver) {
            _languageRepository = languageRepository;
            _contentCreator = contentCreator;
            _domainResolver = domainResolver;
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

            var site = _contentCreator.CreateNew<MySite>(new ContentReference(Guid.Empty, language.LanguageId));
            site.ContentName = "My website";

            _contentCreator.Save(site);
            _contentCreator.Publish(site);

            var domain = new DomainInformation {
                ContentId = site.ContentId,
                LanguageId = site.LanguageId,
                DomainName = "localhost",
                Port = 50176,
                EnforceHttps = false,
                IsPrimary = true,
                UseLanguagePrefix = false
            };
            _domainResolver.AddDomain(domain);

            //var parentReference = new ContentReference(new Guid("0f0a33cd-0db6-48be-9484-d3b51e62e921"), language.LanguageId);
            var parentReference = site.ContentReference;

            var page = _contentCreator.CreateNew<MyPage>(parentReference);
            page.ContentName = "Another subpage " + DateTime.Now.Ticks;
            page.TestHtmlString = new HtmlString("<h1>HTML STRING</h1>");
            page.TestString = "Lorem ipsum";
            _contentCreator.Save(page);
            _contentCreator.Publish(page);

            //var contentId = page.ContentId;

            //page = _contentCreator.CreateNew<MyPage>();
            //page.ContentName = "Sub page";
            //page.TestHtmlString = new HtmlString("<h1>HTML STRING</h1>");
            //page.TestString = "Lorem ipsum";
            //page.ParentId = contentId; //new Guid("1705099c-cea5-41e4-9f4e-7ada79d09995");
            //page.LanguageId = language.LanguageId;
            //_contentCreator.Save(page);


            //var contentIndexService = ServiceLocator.Current.GetInstance<IContentIndexService>();
            //var site = contentIndexService.GetContent(new Guid("4B619F02-CC0F-4B9A-85D5-08D5A0B0E806"));

            return Content("Done");
        }
    }
}