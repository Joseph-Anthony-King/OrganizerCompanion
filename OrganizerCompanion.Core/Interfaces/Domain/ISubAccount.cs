namespace OrganizerCompanion.Core.Interfaces.Domain
{
    internal interface ISubAccount : IDomainEntity
    {
        int? AccountId { get; }
        IAccount? Account { get; set; }
        IDomainEntity? LinkedEntity { get; set; }
        int LinkedEntityId { get; }
        string? LinkedEntityType { get; }
    }
}
