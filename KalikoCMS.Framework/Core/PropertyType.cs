using System;
using System.Collections.Generic;

namespace KalikoCMS.Core
{

    public class PropertyType
    {
        internal static readonly List<PropertyType> PropertyTypes = Data.PropertyTypeData.GetPropertyTypes();
        private PropertyData _class;

        public Guid PropertyTypeId { get; set; }
        public string Name { get; set; }
        public string Class { get; set; }
        public string EditControl { get; set; }
        public Type Type { get; set; }

        public PropertyData ClassInstance {
            get {
                return _class ?? (_class = CreateInstance());
            }
        }

        private PropertyData CreateInstance()
        {
            // TODO: Experimental code to handle typed properties
            if (Type.IsGenericType)
            {
                // TODO: Bad code, possible candidate for errors down the road. Fix!
                var dynamicClosedGenericClass = Type.MakeGenericType(typeof(PropertyData));
                return (PropertyData)Activator.CreateInstance(dynamicClosedGenericClass, null);
            }
            else
            {
                return (PropertyData)Activator.CreateInstance(Type);
            }
        }

        public static PropertyType GetPropertyType(Guid propertyTypeId)
        {
            return PropertyTypes.Find(pt => pt.PropertyTypeId == propertyTypeId);
        }

        public static PropertyType GetPropertyType(Type propertyType)
        {
            return PropertyTypes.Find(pt => pt.Type == propertyType);
        }

        public static PropertyType GetPropertyTypeByClassName(string className)
        {
            return PropertyTypes.Find(pt => pt.Class == className);
        }

        public static Guid GetPropertyTypeId(Type type)
        {
            // TODO: Experimental code to handle typed properties
            string typeName;

            if (type.IsGenericType)
            {
                typeName = type.FullName.Substring(0, type.FullName.IndexOf('['));
            }
            else
            {
                typeName = type.FullName;
            }

            var propertyType = PropertyTypes.Find(pt => pt.Class == typeName);

            if (propertyType != null)
            {
                return propertyType.PropertyTypeId;
            }

            var exception = new Exception("Cannot find propertytype for type " + type.Name);
            Logger.Write(exception, Logger.Severity.Major);
            throw exception;
        }

        public PropertyData CreateNewClassInstance()
        {
            return CreateInstance();
        }
    }
}
