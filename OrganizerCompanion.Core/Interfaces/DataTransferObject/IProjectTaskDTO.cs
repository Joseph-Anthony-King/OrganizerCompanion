using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Interfaces.DataTransferObject
{
    internal interface IProjectTaskDTO : IDomainEntity
    {
        string ProjectTaskName { get; set; }
        string? Description { get; set; }
        List<IProjectAssignmentDTO>? Assignments { get; set; }
        bool IsCompleted { get; set; }
        DateTime? DueDate { get; set; }
        DateTime? CompletedDate { get; }
    }
}
