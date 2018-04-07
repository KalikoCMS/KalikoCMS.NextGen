using KalikoCMS.Core;
using KalikoCMS.Data.Entities;
using KalikoCMS.Data.Repositories.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace TestSiteCore.Controllers {
    using System;

    public class HomeController : Controller {
        private IContentRepository _contentRepository;

        public HomeController(IContentRepository contentRepository) {
            _contentRepository = contentRepository;
        }

        public IActionResult Index() {

            _contentRepository.Create(new ContentEntity() {ContentId = Guid.NewGuid()});
            var content = _contentRepository.GetAll().ToList();
            return Content(content.Count().ToString());

            return View(new ContentReference());
        }
    }
}
