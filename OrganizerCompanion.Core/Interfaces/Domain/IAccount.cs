namespace OrganizerCompanion.Core.Interfaces.Domain
{
    internal interface IAccount : IDomainEntity
    {
        string? AccountName { get; set; }
        string? AccountNumber { get; set; }
        int LinkedEntityId { get; set; }
        string? LinkedEntityType { get; }
        IDomainEntity? LinkedEntity { get; set; }
        List<IAccountFeature> Features { get; set; }
    }
}
