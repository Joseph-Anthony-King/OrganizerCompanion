using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Interfaces.DataTransferObject
{
    internal interface IAnnonymousUserDTO : IDomainEntity
    {
        int AccountId { get; set; }
    }
}
