namespace OrganizerCompanion.Core.Interfaces.Domain
{
    internal interface IProjectAssignment : IDomainEntity
    {
        string ProjectAssignmentName { get; set; }
        string? Description { get; set; }
        int? AssigneeId { get; }
        ISubAccount? Assignee { get; set; }
        int? LocationId { get; set; }
        string? LocationType { get; set; }
        IAddress? Location { get; set; }
        List<IGroup>? Groups { get; set; }
        int? TaskId { get; set; }
        IProjectTask? Task { get; set; }
        bool IsCompleted { get; set; }
        DateTime? DueDate { get; set; }
        DateTime? CompletedDate { get; }
    }
}
