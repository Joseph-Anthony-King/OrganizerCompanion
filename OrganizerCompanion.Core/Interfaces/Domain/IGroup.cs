namespace OrganizerCompanion.Core.Interfaces.Domain
{
    internal interface IGroup : IDomainEntity
    {
        string? GroupName { get; set; }
        string? Description { get; set; }
        List<IContact>? Members { get; set; }
        int AccountId { get; set; }
        IAccount? Account { get; set; }
    }
}