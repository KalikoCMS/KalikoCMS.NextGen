namespace KalikoCMS.Serialization {
    using System;
    using Newtonsoft.Json;
#if NETCORE
    using Microsoft.AspNetCore.Html;
#else
    using System.Web;
    using System.Web.Mvc;
#endif

    public class HtmlStringConverter : JsonConverter {
        public override bool CanConvert(Type objectType) {
            return typeof(HtmlString).IsAssignableFrom(objectType);
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer) {
            if (!(value is HtmlString source)) {
                return;
            }

            writer.WriteValue(source.ToString());
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer) {
            return !(reader.Value is string html) ? null : new HtmlString(html);
        }
    }
}