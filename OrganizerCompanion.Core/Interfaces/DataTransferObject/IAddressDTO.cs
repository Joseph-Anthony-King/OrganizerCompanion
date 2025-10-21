using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Interfaces.DataTransferObject
{
    internal interface IAddressDTO : IDomainEntity, Type.IAddress
    {
        int? LinkedEntityId { get; }
        IDomainEntity? LinkedEntity { get; set; }
        string? LinkedEntityType { get; }
    }
}
