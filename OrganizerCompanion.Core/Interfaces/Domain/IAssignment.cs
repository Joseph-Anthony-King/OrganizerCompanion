namespace OrganizerCompanion.Core.Interfaces.Domain
{
    internal interface IAssignment : IDomainEntity
    {
        string Name { get; set; }
        string? Description { get; set; }
        List<IGroup>? Groups { get; set; }
        bool IsCompleted { get; set; }
        DateTime? DateDue { get; set; }
        DateTime? DateCompleted { get; }
    }
}
