namespace KalikoCMS.Mappers {
    using Core;
    using Data.Entities;
    using Interfaces;

    public class DomainMapper : IDomainMapper {
        public DomainEntity Map(DomainInformation source) {
            return new DomainEntity() {
                DomainId = source.DomainId,
                ContentId = source.ContentId,
                LanguageId = source.LanguageId,
                DomainName = source.DomainName,
                Port = source.Port,
                ApplicationName = source.ApplicationName,
                EnforceHttps = source.EnforceHttps,
                IsPrimary = source.IsPrimary,
                UseLanguagePrefix = source.UseLanguagePrefix
            };
        }

        public DomainInformation Map(DomainEntity source) {
            return new DomainInformation {
                DomainId = source.DomainId,
                ContentId = source.ContentId,
                LanguageId = source.LanguageId,
                DomainName = source.DomainName,
                Port = source.Port,
                ApplicationName = source.ApplicationName,
                EnforceHttps = source.EnforceHttps,
                IsPrimary = source.IsPrimary,
                UseLanguagePrefix = source.UseLanguagePrefix
            };
        }
    }
}