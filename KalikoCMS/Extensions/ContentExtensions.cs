namespace KalikoCMS.Extensions {
    using System;
    using Core;

    public static class ContentExtensions {

        public static bool IsAvailable(this Content content) {
            if (content.Status != ContentStatus.Published) {
                return false;
            }

            var dateTime = DateTime.Now.ToUniversalTime();
            return content.StartPublish != null && content.StartPublish <= dateTime &&
                   (content.StopPublish == null || content.StopPublish > dateTime);
        }

    }
}
