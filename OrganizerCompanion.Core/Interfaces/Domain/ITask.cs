namespace OrganizerCompanion.Core.Interfaces.Domain
{
    internal interface ITask : IDomainEntity
    {
        string Name { get; set; }
        string? Description { get; set; }
        List<IAssignment>? Assignments { get; set; }
        bool IsCompleted { get; set; }
        DateTime? DateDue { get; set; }
        DateTime? DateCompleted { get; }
    }
}
