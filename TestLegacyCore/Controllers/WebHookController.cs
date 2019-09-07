namespace TestLegacyCore.Controllers {
    using KalikoCMS.Services.Content.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    public class WebHookController : Controller {
        private const string ApiKey = "97d8637d-2579-49a8-821e-6920f44e3c6c";
        private readonly IContentIndexService _contentIndexService;

        public WebHookController(IContentIndexService contentIndexService) {
            _contentIndexService = contentIndexService;
        }

        public ActionResult RefreshContent(string apiKey) {
            if (apiKey != ApiKey) {
                return BadRequest();
            }

            try {
                _contentIndexService.Initialize();
                return Ok();
            }
            catch {
                return StatusCode(500);
            }
        }
    }
}