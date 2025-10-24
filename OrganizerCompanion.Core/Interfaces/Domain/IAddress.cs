namespace OrganizerCompanion.Core.Interfaces.Domain
{
    public interface IAddress : IDomainEntity, Interfaces.Type.IAddress
    {
        IDomainEntity? LinkedEntity { get; set; }
        int? LinkedEntityId { get; }
        string? LinkedEntityType { get; }
    }
}
