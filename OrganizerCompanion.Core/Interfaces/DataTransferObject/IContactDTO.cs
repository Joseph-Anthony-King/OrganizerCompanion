using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Interfaces.DataTransferObject
{
    internal interface IContactDTO : IDomainEntity, Type.IPerson
    {
        string? UserName { get; set; }
        bool? IsActive { get; set; }
        bool? IsDeceased { get; set; }
        bool? IsAdmin { get; set; }
        bool? IsSuperUser { get; set; }
    }
}
