namespace KalikoCMS.PropertyType {
    using Attributes;

    [PropertyType("7ad491a1-48c7-40de-9151-9255aa61967d", "Image", "Image property", "%AdminPath%Content/PropertyType/ImagePropertyEditor.ascx")]
    public class ImageProperty {
        public string ImageUrl { get; set; }

        public int? Width { get; set; }

        public int? Height { get; set; }

        public string Description { get; set; }

        public string OriginalImageUrl { get; set; }

        public int? CropX { get; set; }

        public int? CropY { get; set; }

        public int? CropW { get; set; }

        public int? CropH { get; set; }
    }
}