namespace OrganizerCompanion.Core.Interfaces.Domain
{
    internal interface IAddress : IDomainEntity, Interfaces.Type.IAddress
    {
        int LinkedEntityId { get; set; }
        IDomainEntity? LinkedEntity { get; set; }
        string? LinkedEntityType { get; }
    }
}
