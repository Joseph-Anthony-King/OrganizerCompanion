namespace OrganizerCompanion.Core.Interfaces.Domain
{
    internal interface IContact : IPerson
    {
        int LinkedEntityId { get; set; }
        IDomainEntity? LinkedEntity { get; set; }
        string? LinkedEntityType { get; }
    }
}
