namespace TestSiteCore.Controllers {
    using System;
    using System.Linq;
    using System.Text;
    using KalikoCMS.Infrastructure;
    using KalikoCMS.Services.Content.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    public class InfoController : Controller {
        private readonly IContentIndexService _contentIndexService;
        private readonly IContentIndexService _unknown;
        public InfoController(IContentIndexService contentIndexService) {
            _contentIndexService = contentIndexService;
        }

        public ActionResult Index() {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<html><body>");
            stringBuilder.AppendLine("<ul>");
            var node = _contentIndexService.GetNode(new Guid("4B619F02-CC0F-4B9A-85D5-08D5A0B0E806"));
            AppendNodes(node, stringBuilder);
            stringBuilder.AppendLine("</ul>");
            stringBuilder.AppendLine("</body></html>");

            HttpContext.Response.ContentType = "text/html";

            return Content(stringBuilder.ToString());
        }

        private void AppendNodes(ContentNode node, StringBuilder stringBuilder) {
            stringBuilder.AppendLine($"<li>{node.ContentId} [{node.ContentTypeId}]<br />");
            foreach (var nodeLanguage in node.Languages) {
                stringBuilder.AppendLine($" - {nodeLanguage.ContentName} [{nodeLanguage.LanguageId}]<br>");
            }

            if(node.Children.Any()) {
                stringBuilder.AppendLine("<ul>");
                foreach (var nodeChild in node.Children) {
                    AppendNodes(nodeChild, stringBuilder);
                }
                stringBuilder.AppendLine("</ul>");
            }

            stringBuilder.AppendLine("</li>");
        }
    }
}