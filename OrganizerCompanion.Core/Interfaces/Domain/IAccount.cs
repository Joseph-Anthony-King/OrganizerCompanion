namespace OrganizerCompanion.Core.Interfaces.Domain
{
    internal interface IAccount : IDomainEntity
    {
        string? UserName { get; set; }
        string? AccountNumber { get; set; }
        int LinkedEntityId { get; set; }
        string? LinkedEntityType { get; set; }
        IDomainEntity? LinkedEntity { get; set; }
        bool AllowAnnonymousUsers { get; set; }
    }
}
