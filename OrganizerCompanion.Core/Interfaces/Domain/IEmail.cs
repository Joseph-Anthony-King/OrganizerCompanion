namespace OrganizerCompanion.Core.Interfaces.Domain
{
    internal interface IEmail : IDomainEntity, Type.IEmail
    {
        int LinkedEntityId { get; set; }
        IDomainEntity? LinkedEntity { get; set; }
        string? LinkedEntityType { get; }
    }
}
