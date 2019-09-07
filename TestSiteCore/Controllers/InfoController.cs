namespace TestSiteCore.Controllers {
    using System;
    using System.Linq;
    using System.Text;
    using KalikoCMS.Extensions;
    using KalikoCMS.Infrastructure;
    using KalikoCMS.Services.Content.Interfaces;
    using Microsoft.AspNetCore.Mvc;

    public class InfoController : Controller {
        private readonly IContentIndexService _contentIndexService;
        public InfoController(IContentIndexService contentIndexService) {
            _contentIndexService = contentIndexService;
        }

        public ActionResult Index() {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<html><body>");
            stringBuilder.AppendLine("<ul>");
            var node = _contentIndexService.GetNode(new Guid("5D1E8F73-F5CF-4338-BA6E-B1713C4F90B0"));
            AppendNodes(node, stringBuilder);
            stringBuilder.AppendLine("</ul>");
            stringBuilder.AppendLine("</body></html>");

            HttpContext.Response.ContentType = "text/html";

            return Content(stringBuilder.ToString());
        }

        private void AppendNodes(ContentNode node, StringBuilder stringBuilder) {
            stringBuilder.AppendLine($"<li>{node.ContentId} [{node.ContentTypeId}]<br />");
            foreach (var nodeLanguage in node.Languages) {
                var content = _contentIndexService.GetContentFromNode(node);
                stringBuilder.AppendLine($" - {nodeLanguage.ContentName} \"{nodeLanguage.ContentUrl}\" [{nodeLanguage.LanguageId}] {content.IsAvailable()}<br>");   
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