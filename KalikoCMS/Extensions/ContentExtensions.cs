namespace KalikoCMS.Extensions {
    using System;
    using System.Collections.Generic;
    using Core;
    using Core.Interfaces;
    using ServiceLocation;
    using Services.Content.Interfaces;

    public static class ContentExtensions {

        public static IEnumerable<IContent> GetChildren(this IContent content) {
            var contentLoader = ServiceLocator.Current.GetInstance<IContentLoader>();
            return contentLoader.GetChildren(content.ContentReference);
        }

        public static bool IsAvailable(this IContent content) {
            if (content?.Status != ContentStatus.Published) {
                return false;
            }

            var dateTime = DateTime.UtcNow;
            return content.StartPublish != null && content.StartPublish <= dateTime &&
                   (content.StopPublish == null || content.StopPublish > dateTime);
        }

        public static void MakeEditable(this Content content) {
            if (content == null) {
                return;
            }

            content.IsEditable = true;
            content.Status = ContentStatus.WorkingCopy;
        }
    }
}
