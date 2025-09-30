using OrganizerCompanion.Core.Enums;
using OrganizerCompanion.Core.Interfaces.Domain;

namespace OrganizerCompanion.Core.Interfaces.DataTransferObject
{
    internal interface ICAAddressDTO : IDomainEntity, Type.ICAAddress
    {
        Types? Type { get; set; }
    }
}
