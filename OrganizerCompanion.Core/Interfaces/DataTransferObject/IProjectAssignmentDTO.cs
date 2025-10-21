using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Interfaces.DataTransferObject
{
    internal interface IProjectAssignmentDTO : IDomainEntity
    {
        string Name { get; set; }
        string? Description { get; set; }
        int? AssigneeId { get; set; }
        ISubAccountDTO? Assignee { get; set; }
        int? LocationId { get; set; }
        string? LocationType { get; set; }
        IAddressDTO? Location { get; set; }
        List<IGroupDTO>? Groups { get; set; }
        int? TaskId { get; set; }
        IProjectTaskDTO? Task { get; set; }
        bool IsCompleted { get; set; }
        DateTime? DateDue { get; set; }
        DateTime? DateCompleted { get; }
    }
}
