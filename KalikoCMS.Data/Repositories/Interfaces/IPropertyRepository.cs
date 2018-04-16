namespace KalikoCMS.Data.Repositories.Interfaces {
    using System;
    using System.Collections.Generic;
    using Core;
    using Entities;

    public interface IPropertyRepository : IRepository<PropertyEntity, int> {
        List<PropertyData> LoadProperties(Guid contentId, int languageId, Guid contentTypeId, int version, Func<Guid, string, object> creator);
    }
}