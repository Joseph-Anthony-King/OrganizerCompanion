namespace OrganizerCompanion.Core.Interfaces.Domain
{
    internal interface IEmail : IDomainEntity, Type.IEmail
    {
        IDomainEntity? LinkedEntity { get; set; }
        int? LinkedEntityId { get; }
        string? LinkedEntityType { get; }
        bool IsConfirmed { get; set; }
    }
}
