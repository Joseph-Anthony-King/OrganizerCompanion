using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Interfaces.DataTransferObject
{
    internal interface ISubAccountDTO : IDomainEntity
    {
        int? AccountId { get; set; }
        IAccountDTO? Account { get; set; }
        int LinkedEntityId { get; set; }
        string? LinkedEntityType { get; }
        IDomainEntity? LinkedEntity { get; set; }
    }
}
