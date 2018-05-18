namespace KalikoCMS.Mappers.Interfaces {
    using Core;
    using Data.Entities;

    public interface IDomainMapper : IMapper<DomainInformation, DomainEntity>, IMapper<DomainEntity, DomainInformation> { }
}