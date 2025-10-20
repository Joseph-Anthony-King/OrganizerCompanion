namespace OrganizerCompanion.Core.Interfaces.Domain
{
    internal interface ISubAccount : IDomainEntity
    {
        int? AccountId { get; set; }
        IAccount? Account { get; set; }
        int LinkedEntityId { get; set; }
        string? LinkedEntityType { get; }
        IDomainEntity? LinkedEntity { get; set; }
    }
}
