namespace KalikoCMS.Core {
    using System;
    using System.Collections.Generic;
    using Collections;

    public class ContentType {
        public static List<ContentType> ContentTypes { get; internal set; }

        public int ContentTypeId { get; set; }
        public string Name { get; set; }
        public SortDirection DefaultChildSortDirection { get; set; }
        public SortOrder DefaultChildSortOrder { get; set; }
        public string DisplayName { get; set; }
        public string ContentTypeDescription { get; set; }
        public string PageTemplate { get; set; }
        public string PreviewImage { get; set; }
        public bool ShowPublishDates { get; set; }
        public bool ShowSortOrder { get; set; }
        public bool ShowVisibleInMenu { get; set; }
        public Type[] AllowedTypes { get; set; }
        public Type Type { get; set; }

        internal CmsPage Instance { get; set; }
        internal List<PropertyDefinition> Properties { get; set; }

        public ContentType() {
            Properties = new List<PropertyDefinition>();
        }

        public static ContentType GetContentType(int ContentTypeId) {
            return ContentTypes.Find(pt => pt.ContentTypeId == ContentTypeId);
        }

        public static ContentType GetContentType(Type type) {
            return ContentTypes.Find(pt => pt.Type == type);
        }

        internal static void LoadContentTypes() {
            Synchronizer.SynchronizeContentTypes();
        }

        public static List<PropertyDefinition> GetPropertyDefinitions(int ContentTypeId) {
            var ContentType = GetContentType(ContentTypeId);

            if (ContentType == null) {
                throw new Exception("ContentType " + ContentTypeId + " was not found!");
            }

            return ContentType.Properties;
        }
    }
}
