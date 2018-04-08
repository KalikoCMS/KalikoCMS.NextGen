namespace KalikoCMS.Services.Content.Interfaces {
    using System;
    using System.Collections.Generic;
    using Core;

    public interface IContentTypeResolver {
        ContentType GetContentType(int contentTypeId);
        ContentType GetContentType<T>() where T : class;
        ContentType GetContentType(Type type);
        void Initialize();
        List<PropertyDefinition> GetPropertyDefinitions(int contentTypeId);
    }
}