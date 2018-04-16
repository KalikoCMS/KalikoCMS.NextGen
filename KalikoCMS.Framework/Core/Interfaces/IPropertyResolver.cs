namespace KalikoCMS.Core.Interfaces {
    using System;
    using System.Collections.Generic;
    using Collections;

    public interface IPropertyResolver {
        PropertyCollection GetProperties(Guid contentId, int languageId, Guid contentTypeId, int version, bool useCache);
    }
}