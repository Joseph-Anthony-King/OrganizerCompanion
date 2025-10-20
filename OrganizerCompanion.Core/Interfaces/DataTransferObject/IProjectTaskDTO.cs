using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Interfaces.DataTransferObject
{
    internal interface IProjectTaskDTO : IDomainEntity
    {
        string Name { get; set; }
        string? Description { get; set; }
        List<IAssignmentDTO>? Assignments { get; set; }
        bool IsCompleted { get; set; }
        DateTime? DateDue { get; set; }
        DateTime? DateCompleted { get; }
    }
}
