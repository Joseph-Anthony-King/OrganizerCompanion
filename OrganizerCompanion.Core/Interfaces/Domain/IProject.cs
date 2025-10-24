namespace OrganizerCompanion.Core.Interfaces.Domain
{
    internal interface IProject : IDomainEntity
    {
        string ProjectName { get; set; }
        string? Description { get; set; }
        List<IGroup>? Groups { get; set; }
        List<IProjectTask>? Tasks { get; set; }
        bool IsCompleted { get; set; }
        DateTime? DueDate { get; set; }
        DateTime? CompletedDate { get; }
    }
}
