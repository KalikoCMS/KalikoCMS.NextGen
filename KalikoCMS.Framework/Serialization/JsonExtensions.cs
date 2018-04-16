namespace KalikoCMS.Serialization {
    using System.Collections.Generic;
    using Newtonsoft.Json.Linq;

    public static class JsonExtensions {
        public static List<JToken> FindTokens(this JToken containerToken, string name) {
            var matches = new List<JToken>();
            FindTokens(containerToken, name, matches);
            return matches;
        }

        private static void FindTokens(JToken containerToken, string name, ICollection<JToken> matches) {
            switch (containerToken.Type) {
                case JTokenType.Object:
                    foreach (var child in containerToken.Children<JProperty>()) {
                        if (child.Name == name) {
                            matches.Add(child);
                        }

                        FindTokens(child.Value, name, matches);
                    }
                    break;
                case JTokenType.Array:
                    foreach (var child in containerToken.Children()) {
                        FindTokens(child, name, matches);
                    }
                    break;
            }
        }
    }
}