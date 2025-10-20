namespace OrganizerCompanion.Core.Interfaces.Domain
{
    internal interface IProjectTask : IDomainEntity
    {
        string Name { get; set; }
        string? Description { get; set; }
        List<IProjectAssignment>? Assignments { get; set; }
        bool IsCompleted { get; set; }
        DateTime? DateDue { get; set; }
        DateTime? DateCompleted { get; }
    }
}
