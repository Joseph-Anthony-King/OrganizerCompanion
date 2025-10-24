namespace OrganizerCompanion.Core.Interfaces.Domain
{
    internal interface IProjectTask : IDomainEntity
    {
        string ProjectTaskName { get; set; }
        string? Description { get; set; }
        List<IProjectAssignment>? Assignments { get; set; }
        bool IsCompleted { get; set; }
        DateTime? DueDate { get; set; }
        DateTime? CompletedDate { get; }
    }
}
