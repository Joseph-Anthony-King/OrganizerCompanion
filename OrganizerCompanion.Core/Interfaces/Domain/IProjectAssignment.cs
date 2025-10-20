namespace OrganizerCompanion.Core.Interfaces.Domain
{
    internal interface IProjectAssignment : IDomainEntity
    {
        string Name { get; set; }
        string? Description { get; set; }
        List<IGroup>? Groups { get; set; }
        int? TaskId { get; set; }
        IProjectTask? Task { get; set; }
        bool IsCompleted { get; set; }
        DateTime? DateDue { get; set; }
        DateTime? DateCompleted { get; }
    }
}
