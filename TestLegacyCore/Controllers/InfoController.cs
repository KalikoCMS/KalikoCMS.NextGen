namespace TestLegacyCore.Controllers {
    using System;
    using System.Linq;
    using System.Text;
    using KalikoCMS.Extensions;
    using KalikoCMS.Infrastructure;
    using KalikoCMS.Services.Content.Interfaces;
    using Microsoft.AspNetCore.Mvc;
    using Models.PageTypes;

    public class InfoController : Controller {
        private readonly IContentIndexService _contentIndexService;

        public InfoController(IContentIndexService contentIndexService) {
            _contentIndexService = contentIndexService;
        }

        public ActionResult Index() {
            var stringBuilder = new StringBuilder();
            stringBuilder.AppendLine("<html><body>");
            stringBuilder.AppendLine("<ul>");
            var node = _contentIndexService.GetNode(new Guid("be3673e2-128b-42a5-b8cb-8f345d9a58f2"));
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
                stringBuilder.AppendLine($" - {nodeLanguage.ContentName} \"{nodeLanguage.ContentUrl}\" [{nodeLanguage.LanguageId}] {content.IsAvailable()} Type: {content.GetType()} {content is ArticlePage}<br>");   
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