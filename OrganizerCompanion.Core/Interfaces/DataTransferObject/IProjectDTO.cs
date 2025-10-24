using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Interfaces.DataTransferObject
{
    internal interface IProjectDTO : IDomainEntity
    {
        string ProjectName { get; set; }
        string? Description { get; set; }
        List<IGroupDTO>? Groups { get; set; }
        List<IProjectTaskDTO>? Tasks { get; set; }
        bool IsCompleted { get; set; }
        DateTime? DueDate { get; set; }
        DateTime? CompletedDate { get; }
    }
}
