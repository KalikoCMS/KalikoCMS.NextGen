namespace KalikoCMS.Services.Content.Interfaces {
    using System;
    using System.Collections.Generic;
    using Core;

    public interface IContentTypeResolver {
        ContentType GetContentType(Guid contentTypeId);
        ContentType GetContentType<T>() where T : class;
        ContentType GetContentType(Type type);
        void Initialize();
        List<PropertyDefinition> GetPropertyDefinitions(Guid contentTypeId);
    }
}