namespace KalikoCMS.Services.Content.Interfaces {
    using Core;

    public interface IDomainResolver {
        void AddDomain(DomainInformation domain);
        void Initialize();
        DomainInformation GetCurrentDomain();
    }
}