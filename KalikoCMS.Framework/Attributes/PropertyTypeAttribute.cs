namespace KalikoCMS.Attributes {
    using System;

    [AttributeUsage(AttributeTargets.Class)]
    public sealed class PropertyTypeAttribute : Attribute {

        public PropertyTypeAttribute(string propertyTypeId, string name, string description, string editorControl) {
            PropertyTypeId = propertyTypeId;
            Name = name;
            Description = description;
            EditorControl = editorControl;
        }

        public string PropertyTypeId { get; }
        public string Name { get; }
        public string Description { get; }
        public string EditorControl { get; }

    }
}