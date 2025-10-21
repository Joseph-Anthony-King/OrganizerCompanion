namespace OrganizerCompanion.Core.Interfaces.Domain
{
    internal interface IProject : IDomainEntity
    {
        string Name { get; set; }
        string? Description { get; set; }
        List<IGroup>? Groups { get; set; }
        List<IProjectTask>? Tasks { get; set; }
        bool IsCompleted { get; set; }
        DateTime? DateDue { get; set; }
        DateTime? DateCompleted { get; }
    }
}
