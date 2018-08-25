namespace KalikoCMS.Core.Interfaces {
    using System;
    using Collections;

    public interface IPropertyResolver {
        PropertyCollection GetProperties(Guid contentId, int languageId, Guid contentTypeId, int version, bool useCache);
        void Preload();
    }
}