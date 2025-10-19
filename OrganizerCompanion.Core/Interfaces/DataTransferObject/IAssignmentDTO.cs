using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Interfaces.DataTransferObject
{
    internal interface IAssignmentDTO : IDomainEntity
    {
        string Name { get; set; }
        string? Description { get; set; }
        List<IGroupDTO>? Groups { get; set; }
        bool IsCompleted { get; set; }
        DateTime? DateDue { get; set; }
        DateTime? DateCompleted { get; }
    }
}
