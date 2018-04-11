namespace KalikoCMS.Core.Collections {
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class PropertyCollection : IEnumerable<PropertyData> {
        internal List<PropertyData> Properties { get; set; }

        public object this[string propertyName] {
            get {
                var property = GetItem(propertyName);

                return property?.Value;
            }
            internal set {
                var property = GetItem(propertyName);

                if (property == null) {
                    throw new Exception($"Property '{propertyName}' not found!");
                }

                property.Value = value;
            }
        }

        internal PropertyData GetItem(string propertyName) {
            return Properties.Find(p => p.PropertyName == propertyName.ToLowerInvariant());
        }

        public IEnumerator<PropertyData> GetEnumerator() {
            return Properties.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator() {
            return GetEnumerator();
        }

        public object GetPropertyValue(string propertyName, out bool propertyExists) {
            var property = GetItem(propertyName);

            if (property == null) {
                propertyExists = false;
                return null;
            }

            propertyExists = true;
            return property.Value;
        }
    }
}