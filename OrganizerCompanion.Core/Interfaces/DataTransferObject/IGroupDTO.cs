using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Interfaces.DataTransferObject
{
    internal interface IGroupDTO : IDomainEntity
    {
        string? GroupName { get; set; }
        string? Description { get; set; }
        List<IContactDTO> Members { get; set; }
        int AccountId { get; set; }
        IAccountDTO? Account { get; set; }
    }
}