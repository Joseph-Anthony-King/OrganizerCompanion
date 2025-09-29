namespace OrganizerCompanion.Core.Interfaces.Domain
{
    internal interface IPhoneNumber : IDomainEntity, OrganizerCompanion.Core.Interfaces.Type.IPhoneNumber
    {
        int LinkedEntityId { get; set; }
        IDomainEntity? LinkedEntity { get; set; }
        string? LinkedEntityType { get; }
    }
}
